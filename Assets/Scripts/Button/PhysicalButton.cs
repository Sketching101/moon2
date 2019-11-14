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
        public int inCollision = 0;

        public List<Transform> PushedBy;

        public Transform BufferAnchor;

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
            PushedBy = new List<Transform>();
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
            } else if(PushedBy.Count > 0)
            {
                Debug.Log("Pushing");
                BufferAnchor.position = PushedBy[0].position;
                ClampedPos.y = BufferAnchor.localPosition.y;
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

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("Collided with " + other.gameObject.name + ":" + other.gameObject.tag);
            if(other.gameObject.tag == "Finger")
            {
                inCollision++;
                ButtonPusher Finger = other.gameObject.GetComponent<ButtonPusher>();
                Debug.Log(other.ClosestPoint(transform.position));
                Finger.FingerTipAnchor.position = other.ClosestPoint(transform.position);
                PushedBy.Add(Finger.FingerTipAnchor);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            Debug.Log("Collision ended with " + other.gameObject.name + ":" + other.gameObject.tag);
            if (other.gameObject.tag == "Finger")
            {
                ButtonPusher Finger = other.gameObject.GetComponent<ButtonPusher>();
                PushedBy.Remove(Finger.FingerTipAnchor);
                inCollision--;
            }
        }
    }
}