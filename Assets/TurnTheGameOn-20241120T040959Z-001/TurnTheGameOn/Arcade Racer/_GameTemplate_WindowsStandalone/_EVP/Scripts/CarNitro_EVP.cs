namespace TurnTheGameOn.ArcadeRacer
{
    using UnityEngine;

    public class CarNitro_EVP : CarNitro
    {
        Rigidbody m_rigidbody;
        public enum Mode { Acceleration, Impulse };
        public Mode mode = Mode.Acceleration;
        public float value = 10.0f;
        public float maxVelocity = 50.0f;

        [ContextMenu("NiroON")]
        public override void NitroOn()
        {
            if (enableNitro && !nitroOn && nitroAmount > 2.0f)
            {
                nitroEffectObject.SetActive(true);
                nitroOn = true;
            }
        }

        [ContextMenu("NiroOFF")]
        public override void NitroOff()
        {
            if (nitroOn && enableNitro)
            {
                nitroEffectObject.SetActive(false);
                nitroOn = false;
            }
        }

        public override void Start()
        {
            m_rigidbody = GetComponent<Rigidbody>();
            if (enableNitro)
            {
                nitroAmount = nitroDuration;
                StartCoroutine("UpdateNitroAmount");
            }
        }

        void Update()
        {
            if (mode == Mode.Impulse)
            {
                if (nitroOn && m_rigidbody.velocity.magnitude < maxVelocity)
                {
                    m_rigidbody.AddRelativeForce(Vector3.forward * value, ForceMode.VelocityChange);
                }
            }
        }


        void FixedUpdate()
        {
            if (mode == Mode.Acceleration)
            {
                if (nitroOn && m_rigidbody.velocity.magnitude < maxVelocity)
                {
                    m_rigidbody.AddRelativeForce(Vector3.forward * value, ForceMode.Acceleration);
                }
            }
        }

    }
}