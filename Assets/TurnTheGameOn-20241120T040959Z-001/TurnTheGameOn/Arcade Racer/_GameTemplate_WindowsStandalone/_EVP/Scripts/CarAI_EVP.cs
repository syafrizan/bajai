namespace TurnTheGameOn.ArcadeRacer
{
    using UnityEngine;
    using EVP;

    public class CarAI_EVP : CarAI
    {
        public VehicleController vehicleController;
        public float minDrag = 0.02f;
        public float minAngularDrag = 0.5f;
        public float topSpeed = 140;
        public float cautionSlowSpeed = 1f;
        public float driveTargetAngleDifference;
        public float angularDistance;
        public SpeedType speedType = SpeedType.MPH;
        public float speedMultiplier;

        public override void Awake()
        {
            transformCached = transform;
            rigidbodyCached = GetComponent<Rigidbody>();
            currentRoutePointIndex = startingRoutePointIndex;
            for (int i = 0; i < detectionData.Length; i++)
            {
                detectionData[i].sensor.updateInterval = m_AISettings.updateInterval;
                detectionData[i].sensor.layerMask = m_AISettings.detectionLayers;
            }
            speedMultiplier = speedType == SpeedType.MPH ? 2.23693629f : speedType == SpeedType.KPH ? 3.6f : 0f;
        }

        public override void CheckAutoCautionDistanceThreshold()
        {
            Vector3 targetDir = driveTarget.position - transform.position;
            driveTargetAngleDifference = Mathf.Abs(Vector3.Angle(targetDir, transform.forward));
            if (waypointRoute.stopForTrafficLight && routeDistanceTraveled > 0 && currentRoutePointIndex >= waypointRoute.waypointDataList.Count - 1)
            {
                if (cautionAmount != 1)
                {
                    minCaution = Mathf.Clamp(minCaution += cautionSlowSpeed * Time.deltaTime, minCaution, 1.0f);
                    cautionAmount = minCaution;
                    vehicleController.brakeInput = minCaution;
                    vehicleController.throttleInput = 0;
                    rigidbodyCached.drag = 1.5f;
                    rigidbodyCached.angularDrag = 1;
                }
                else if (vehicleController.speed == 0)
                {
                    rigidbodyCached.drag = minDrag;
                    rigidbodyCached.angularDrag = minAngularDrag;
                }
            }
            else if ((
                detectionData[3].hit == true ||
                detectionData[4].hit == true ||
                detectionData[5].hit == true
                ) && (
                detectionData[3].distance < m_AISettings.autoCautionDistanceThreshold ||
                detectionData[4].distance < m_AISettings.autoCautionDistanceThreshold ||
                detectionData[5].distance < m_AISettings.autoCautionDistanceThreshold))
            {
                if (cautionAmount != 1)
                {
                    minCaution = Mathf.Clamp(minCaution += cautionSlowSpeed * Time.deltaTime, minCaution, 1.0f);
                    cautionAmount = minCaution;
                    vehicleController.brakeInput = minCaution;
                    vehicleController.throttleInput = 0;
                    rigidbodyCached.drag = 1.5f;
                    rigidbodyCached.angularDrag = 1;
                }
                else if (vehicleController.speed == 0)
                {
                    rigidbodyCached.drag = minDrag;
                    rigidbodyCached.angularDrag = minAngularDrag;
                }
            }
            else //if (cautionAmount != minCaution)
            {
                angularDistance = Mathf.InverseLerp(0, 90, driveTargetAngleDifference);
                minCaution = Mathf.Clamp(angularDistance, 0, 0.9f);
                cautionAmount = minCaution;
                vehicleController.brakeInput = minCaution;
                vehicleController.throttleInput = 0;
                rigidbodyCached.drag = minDrag + cautionAmount;
                rigidbodyCached.angularDrag = minAngularDrag + (cautionAmount * 10);

                //// The step size is equal to speed times frame time.
                //float singleStep = 1 * Time.deltaTime;

                //// Rotate the forward vector towards the target direction by one step
                //Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDir, singleStep, 0.0f);

                //// Draw a ray pointing at our target in
                //Debug.DrawRay(transform.position, newDirection, Color.red);

                //// Calculate a rotation a step closer to the target and applies rotation to this object
                //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(newDirection), 1);
            }
        }

        public override void Move()
        {
            offsetTargetPos = driveTarget.position; // our target position starts off as the 'real' target position
            if (isDriving)
            {
                desiredSpeed = Mathf.Lerp(0.0f, topSpeed * (1 - cautionAmount), 1);
                offsetTargetPos += driveTarget.right * (Mathf.PerlinNoise(Time.time * 0.2f, 0.2f) * 2 - 1) * 0.1f; // no need for evasive action, we can just wander across the path-to-target in a random way, // // which can help prevent AI from seeming too uniform and robotic in their driving
                if (useSpeedLimit && desiredSpeed > speedLimit) desiredSpeed = speedLimit;
                accelBrakeSensitivity = (desiredSpeed < vehicleController.speed * speedMultiplier) ? m_AISettings.brakeSensitivity : m_AISettings.accelerationSensitivity; // use different sensitivity depending on whether accelerating or braking:                
                accel = Mathf.Clamp((desiredSpeed - vehicleController.speed * speedMultiplier) * accelBrakeSensitivity, -1, 1); // decide the actual amount of accel/brake input to achieve desired speed.
                localTarget = transform.InverseTransformPoint(offsetTargetPos); // calculate the local-relative position of the target, to steer towards                
                targetAngle = Mathf.Atan2(localTarget.x, localTarget.z) * Mathf.Rad2Deg; // work out the local angle towards the target
                steer = Mathf.Clamp(targetAngle * m_AISettings.steeringSensitivity, -1, 1) * Mathf.Sign(vehicleController.speed * speedMultiplier);  // get the amount of steering needed to aim the car towards the target                
                if (useBrakeTrigger)
                {
                    if (releaseBrakeWhenStopped && vehicleController.speed < 1)
                    {
                        brakeAmount = 0;
                    }
                    vehicleController.steerInput = steer;
                    vehicleController.brakeInput = brakeAmount;
                    vehicleController.throttleInput = accel;
                    vehicleController.handbrakeInput = 0;
                }
                else
                {
                    vehicleController.steerInput = steer;
                    vehicleController.brakeInput = brakeAmount;
                    vehicleController.throttleInput = accel;
                    vehicleController.handbrakeInput = 0;
                }
            }
            else
            {
                if (vehicleController.speed > 2)
                {
                    offsetTargetPos += driveTarget.right; // no need for evasive action, drive toward target //else offsetTargetPos += driveTarget.right* (Mathf.PerlinNoise(Time.time*m_LateralWanderSpeed, m_RandomPerlin)*2 - 1)* m_LateralWanderDistance; // no need for evasive action, we can just wander across the path-to-target in a random way, which can help prevent AI from seeming too uniform and robotic in their driving
                    localTarget = transform.InverseTransformPoint(offsetTargetPos); // calculate the local-relative position of the target, to steer towards                    
                    targetAngle = Mathf.Atan2(localTarget.x, localTarget.z) * Mathf.Rad2Deg; // work out the local angle towards the target                    
                    steer = Mathf.Clamp(targetAngle * m_AISettings.steeringSensitivity, -1, 1) * Mathf.Sign(vehicleController.speed * speedMultiplier); // get the amount of steering needed to aim the car towards the target                    
                    vehicleController.steerInput = steer;
                    vehicleController.throttleInput = 0;
                    vehicleController.brakeInput = -1;
                    vehicleController.handbrakeInput = 1;

                }
                else
                {
                    vehicleController.steerInput = steer;
                    vehicleController.throttleInput = 0;
                    vehicleController.brakeInput = -1;
                    vehicleController.handbrakeInput = 1;
                }
            }
        }

        public override void CheckIfLost()
        {
            if (isDriving)
            {
                waypointTimer += Time.deltaTime * 1;
                if (m_AISettings.useWaypointReset && waypointTimer >= m_AISettings.waypointReset) MoveVehicleToTarget();
                if (m_AISettings.useStuckReset && stuckTimer >= m_AISettings.stuckReset) MoveVehicleToTarget();
                stuckTimer = vehicleController.speed < 5.0f ? stuckTimer + (Time.deltaTime * 1) : 0;
            }
            else waypointTimer = stuckTimer = 0.0f;
        }

    }
}