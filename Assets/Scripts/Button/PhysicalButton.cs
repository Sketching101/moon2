using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ManualControls
{
    public class PhysicalButton : MonoBehaviour
    {
        [Header("Restrictions")]
        public float y_min = -0.03f;
        public float y_max = 0;

        private Rigidbody rb;
        private int inCollision = 0;

        [Header("Debug")]
        [SerializeField]
        bool ButtonPressed;

        Vector3 ClampedPos;
        

        public bool GetButton
        {
            get
            {
                return button_val;
            }
        }

        public bool GetButtonDown
        {
            get
            {
                return (button_val && !last_button_val);
            }
        }

        bool last_button_val;

        bool button_val;

        bool can_press = true;
        bool pressed_buf = false;

        void Awake()
        {
            if (rb == null)
                rb = gameObject.GetComponent<Rigidbody>();
        }

        void Update()
        {
            ButtonPressed = GetButton;
        /*
         *  if(GetButtonDown)
         *      Play.ClickSound
         */
        }

        // Update is called once per frame
        void LateUpdate()
        {
            last_button_val = button_val;

            float normalize = (transform.localPosition.y - y_min) / (y_max - y_min);
            if(normalize < .3f && can_press)
            {
                button_val = true;
                pressed_buf = true;
            } 
            else
            {
                if(pressed_buf)
                {
                    can_press = false;
                    if(normalize > .6f)
                    {
                        pressed_buf = false;
                        can_press = true;
                    }
                }
                button_val = false;
            }
        }
        

        void FixedUpdate()
        {
            ClampedPos = transform.localPosition;

            if (inCollision == 0)
            {
                ClampedPos.y += Time.fixedDeltaTime;
            }

            if(ClampedPos.y > y_max)
            {
                ClampedPos.y = y_max - .001f;
            } else if(ClampedPos.y < y_min)
            {
                ClampedPos.y = y_min + .001f;
            }
            transform.localPosition = ClampedPos;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if(collision.gameObject.tag == "Finger")
                inCollision++;
        }

        private void OnCollisionExit(Collision collision)
        {
            if (collision.gameObject.tag == "Finger")
                inCollision--;
        }
    }
}