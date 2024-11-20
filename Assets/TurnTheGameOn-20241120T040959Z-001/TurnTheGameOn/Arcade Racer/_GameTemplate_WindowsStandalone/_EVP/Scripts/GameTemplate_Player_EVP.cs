namespace TurnTheGameOn.ArcadeRacer
{
    using EVP;
    using UnityEngine;

    public class GameTemplate_Player_EVP : GameTemplate_Player
    {
        public VehicleController vehicleController;
        public VehicleAudio vehicleAudio;
        public VehicleStandardInput vehicleStandardInput;

        public override void Start()
        {
            if (vehicleController == null) vehicleController = GetComponent<VehicleController>();
            if (vehicleAudio == null) vehicleAudio = GetComponent<VehicleAudio>();
            if (vehicleStandardInput == null) vehicleStandardInput = GetComponent<VehicleStandardInput>();
            if (!isInitialized) Initialize();
            string controller = GameData.GetControllerType();
            if (controller == "KEYBOARD")
            {
                vehicleStandardInput.throttleAndBrakeInput = VehicleStandardInput.ThrottleAndBrakeInput.SingleAxis;
            }
            if (controller == "XBOX_ONE_CONTROLLER")
            {
                vehicleStandardInput.throttleAndBrakeInput = VehicleStandardInput.ThrottleAndBrakeInput.SeparateAxes;
            }
            if (controller == "PLAYSTATION_4_CONTROLLER")
            {
                vehicleStandardInput.throttleAndBrakeInput = VehicleStandardInput.ThrottleAndBrakeInput.SeparateAxes;
            }
            
            vehicleStandardInput.steerAxis = playerInputSettings.inputAxes.steering;
            vehicleStandardInput.throttleAxis = playerInputSettings.inputAxes.throttle;
            vehicleStandardInput.brakeAxis = playerInputSettings.inputAxes.brake;
            vehicleStandardInput.handbrakeAxis = playerInputSettings.inputAxes.handBrake;
        }

        public override void Update()
        {
            if (useWaypointArrow)
            {
                if (arrowTarget != null)
                {
                    arrowTarget.localPosition = Vector3.Lerp(arrowTarget.localPosition, RaceManager.Instance.racerInformation[playerIndex].currentWaypoint.localPosition, arrowSmooth * Time.deltaTime);
                    arrowTarget.localRotation = Quaternion.Lerp(arrowTarget.localRotation, RaceManager.Instance.racerInformation[playerIndex].currentWaypoint.localRotation, arrowSmooth * Time.deltaTime);
                    waypointArrow.LookAt(arrowTarget);
                }
            }
            if (useWrongWayDetection)
            {
                playerVectorForward = RaceManager.Instance.racerInformation[playerIndex].racerObject.transform.TransformDirection(Vector3.forward);
                playerVectorToToOther = RaceManager.Instance.racerInformation[playerIndex].currentWaypoint.localPosition - RaceManager.Instance.racerInformation[playerIndex].racerObject.transform.localPosition;
                wrongWay = (Vector3.Dot(playerVectorForward, playerVectorToToOther) < 0) ? true : false;
            }
            if (vehicleCameraSystem.isLookingBack)
            {
                if (Input.GetAxisRaw(playerInputSettings.inputAxes.lookBack) == 0) vehicleCameraSystem.OnLookBackKeyUp();
            }
            else
            {
                if (Input.GetAxisRaw(playerInputSettings.inputAxes.lookBack) == 1) vehicleCameraSystem.LookBackCamera();
            }
            if (carNitro)
            {
                if (Input.GetAxisRaw(playerInputSettings.inputAxes.nitro) == 1) carNitro.NitroOn();
                if (Input.GetAxisRaw(playerInputSettings.inputAxes.nitro) == 0) carNitro.NitroOff();
            }
        }

        //public override float SteerInput()
        //{
        //    return vehicleController.steerInput;
        //}

        public override float SpeedFactor()
        {
            return vehicleController.speed / vehicleController.maxSpeedForward;
        }

        public override float CurrentSpeed()
        {
            if (speedType == SpeedType.MPH) return vehicleController.speed * 2.23693629f;
            else if (speedType == SpeedType.KPH) return vehicleController.speed * 3.6f;
            else return vehicleController.speed;
        }

        public override float GearFactor()
        {
            return 0;
        }

        public override float RPM_Minimum()
        {
            return 0.2f;
        }

        public override float RPM_Limit()
        {
            return 8f;
        }

        public override bool ManualTransmission()
        {
            return false;
        }

        public override int CurrentGear()
        {
            return vehicleAudio.simulatedGear;
        }

        public override float RPM()
        {
            return vehicleAudio.simulatedEngineRpm;
        }

        public override float AccelInput()
        {
            return vehicleController.throttleInput;
        }

        public override float BrakeInput()
        {
            return vehicleController.brakeInput;
        }

        public override bool IsReversing()
        {
            return vehicleController.throttleInput < 0 ? true : false;
        }

        public override float NitroAmount()
        {
            if (carNitro) return carNitro.nitroAmount;
            else return 0;
        }
    }
}