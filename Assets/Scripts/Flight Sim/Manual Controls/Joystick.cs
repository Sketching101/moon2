using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ManualControls
{
    public class Joystick : MonoBehaviour
    {
        [Header("Joystick Visualizer")]
        [SerializeField] private Transform JoystickVis;

        [Header("Anchors")]
        [SerializeField] private Transform BaseAnchor;
        [SerializeField] private Transform GripAnchor;
        [SerializeField] private Transform TopAnchor;

        [Header("Grabbable Object")]
        [SerializeField] private AltVRGrabbable GripObject;

        private float BaseToTopDist;

        [Header("Test Indicators")]
        [SerializeField] Vector3 BaseToGripNorm;
        Vector3 OriginalTopPosition;
        [SerializeField] float DistToOriginalTopPosition;

        [Header("Output")]
        [SerializeField] private Vector3 JoystickXYOut;

        float DistCovered = 0.0f;
        float speed = 1.5f;
        float startTime = 0.0f;

        [SerializeField]
        bool GrabbedFlag = false;

        public OVRInput.Controller GrabbedBy
        {
            get
            {
                if (GripObject != null && GripObject.m_grabbedBy != null)
                {
                    return GripObject.m_grabbedBy.Controller;
                }
                else
                {
                    return OVRInput.Controller.None;
                }
            }
        }

        // Use this for initialization
        void Awake()
        {
            BaseToTopDist = (TopAnchor.localPosition - BaseAnchor.localPosition).magnitude;
            OriginalTopPosition = GripAnchor.localPosition;
            DistToOriginalTopPosition = 0f;
        }

        // Update is called once per frame
        void Update()
        {
            UpdateValues();
            RotateJoystick();
            if (!GripObject.isGrabbed && GrabbedFlag)
            {
                GrabbedFlag = false;
                StartCoroutine(OnLetGo());
            }
            else if (GripObject.isGrabbed && !GrabbedFlag)
            {
                GrabbedFlag = true;
            }

        }

        private void UpdateValues()
        {
            Vector3 NewBaseToGripNorm = (GripAnchor.localPosition - BaseAnchor.localPosition).normalized;
            Vector3 CompToOrigPos = (GripAnchor.localPosition - OriginalTopPosition);

            JoystickXYOut = new Vector3();

            if (CompToOrigPos.z > 0.01f)
                JoystickXYOut.y = CompToOrigPos.z - 0.01f;
            else if(CompToOrigPos.z < -0.01f)
                JoystickXYOut.y = CompToOrigPos.z + 0.01f;

            if (CompToOrigPos.x > 0.01f)
                JoystickXYOut.x = CompToOrigPos.x - 0.01f;
            else if (CompToOrigPos.x < -0.01f)
                JoystickXYOut.x = CompToOrigPos.x + 0.01f;

            if (NewBaseToGripNorm != BaseToGripNorm)
            {
                BaseToGripNorm = (GripAnchor.localPosition - BaseAnchor.localPosition).normalized;
                TopAnchor.localPosition = BaseToGripNorm * BaseToTopDist;
            }
            DistToOriginalTopPosition = (GripAnchor.localPosition - OriginalTopPosition).magnitude;
        }

        private void RotateJoystick()
        {
            JoystickVis.LookAt(GripAnchor.position, Vector3.forward);
        }

        private IEnumerator OnLetGo()
        {
            Vector3 StartPos = GripAnchor.localPosition;
            float journeyLength = Vector3.Distance(StartPos, OriginalTopPosition);
            startTime = Time.time;

            while (!GripObject.isGrabbed && DistToOriginalTopPosition > 0.01f)
            {
                float distCovered = (Time.time - startTime) * speed;
                // Fraction of journey completed equals current distance divided by total distance.
                float fractionOfJourney = distCovered / journeyLength;
                GripAnchor.localPosition = Vector3.Lerp(StartPos, OriginalTopPosition, fractionOfJourney);
                yield return null;
            }
            yield return null;
        }

        public Vector3 GetJoystickOut()
        {
            return JoystickXYOut * 200;
        }

        public float GetYawOut()
        {
            if (GrabbedFlag)
            {
                return OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, GrabbedBy).x;
            }
            else return 0;
        }
    }
}