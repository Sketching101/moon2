using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ManualControls;

namespace ShipControllerNS

{
    public class ShipController : MonoBehaviour
    {
        [Header("Manual Controls")]
        [SerializeField]
        private Joystick RotateJoystick;
        [SerializeField]
        private Throttle VelocityThrottle;

        [Header("Ship Objects")]
        [SerializeField]
        private Transform ShipTransform;


        [Header("Mobility Stats")]
        [SerializeField]
        public float MinVelocity;
        [SerializeField]
        public float Axelaration;
        [SerializeField]
        public float MaxVelocity;

        bool Collided = false;

        [Header("Dmg and Health Stats")]
        [SerializeField]
        private float HP;
        [SerializeField]
        private float FireRate;
        [SerializeField]
        private Transform[] Canons;

        float DamageRate = 1f;


        [Header("Debug Values")]
        [SerializeField]
        private float time_t = 0;
        [SerializeField]
        float roll = 0.0f;
        [SerializeField]
        float pitch = 0.0f;
        [SerializeField]
        float yaw = 0.0f;


        float velocity_t = 0;
        float acceleration_t = 0;

        public float velocity_display = 0;

        // Use this for initialization
        void Awake()
        {
            velocity_t = MinVelocity;
        }

        void Update()
        {
            if (PullUpMenu.Instance.Paused) return;
            if (Collided)
                HP -= Time.deltaTime * DamageRate;
            Vector3 JoystickInput = RotateJoystick.GetJoystickOut();
            float ThrottleInput = VelocityThrottle.GetThrottleOut();

            pitch = JoystickInput.y;
            roll = JoystickInput.x;
            yaw = RotateJoystick.GetYawOut() * 5;

            acceleration_t = Axelaration * ThrottleInput;

            
            if (velocity_t <= MinVelocity)
                velocity_t = MinVelocity;
            else if (velocity_t > MaxVelocity)
                velocity_t = MaxVelocity;

            velocity_display = velocity_t;

            if (transform.position.y < 36)
                MenuSelect.Instance.LoseGame();
            if (Mathf.Abs(transform.position.x) > 2000)
                MenuSelect.Instance.LoseGame();
            if (Mathf.Abs(transform.position.z) > 2000)
                MenuSelect.Instance.LoseGame();
            if (Mathf.Abs(transform.position.y) > 2000)
                MenuSelect.Instance.LoseGame();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (PullUpMenu.Instance.gameState == PullUpMenu.GameState.Paused || PullUpMenu.Instance.gameState == PullUpMenu.GameState.Dead) return;

            velocity_t += acceleration_t * Time.fixedDeltaTime;

            ShipTransform.position += velocity_t * ShipTransform.forward * Time.fixedDeltaTime;

            ShipTransform.Rotate(ShipTransform.right, pitch / 10, Space.World);
            ShipTransform.Rotate(ShipTransform.forward, roll * -1 / 10, Space.World);
            ShipTransform.Rotate(ShipTransform.up, yaw / 10, Space.World);
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "CrashCollision")
                MenuSelect.Instance.LoseGame();
        }

        void OnTriggerExit(Collider other)
        {
            Collided = false;
        }
    }
}
