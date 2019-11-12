using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ManualControls
{
    public class Throttle : MonoBehaviour
    {
        [Header("Throttle Visualizer")]
        [SerializeField] private Transform ThrottleVis;

        [Header("Anchors")]
        [SerializeField] private Transform BaseAnchor;
        [SerializeField] private Transform GripAnchor;
        [SerializeField] private Transform TopAnchor;
        [SerializeField] private Transform HandleAnchor;


        [Header("Grabbable Object")]
        [SerializeField] private AltVRGrabbable GripObject;

        private float BaseToTopDist;

        [Header("Test Indicators")]
        [SerializeField] Vector3 BaseToGripNorm;
        Vector3 OriginalTopPosition;
        [SerializeField] Vector3 NearestY;
        [SerializeField] float DistToSnap = 0.0f;
        [SerializeField] float DistToOriginalTopPosition;
        [SerializeField] float height;

        float offset = 0.1466741f;

        [Header("Snap At Y")]
        public float[] ThrottleYSnap;
        public Transform[] SnapPositions;

        [Header("Output")]
        [SerializeField] private float ThrottleYOut;

        float DistCovered = 0.0f;
        float speed = 1.5f;
        float startTime = 0.0f;
        bool GrabbedFlag = false;

        OVRInput.Controller LastGrabbedBy = OVRInput.Controller.None;

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
            OriginalTopPosition = GripAnchor.localPosition;
            BaseToTopDist = (GripAnchor.localPosition - BaseAnchor.localPosition).magnitude;

            DistToOriginalTopPosition = 0f;
            offset = HandleAnchor.localPosition.z - BaseAnchor.localPosition.z;
        }

        // Update is called once per frame
        void Update()
        {
            UpdateValues();
            RotateThrottle();
            if (!GripObject.isGrabbed && GrabbedFlag)
            {
                GrabbedFlag = false;
                Debug.Log("Let go");
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
            NewBaseToGripNorm.x = 0;
            Vector3 CompToOrigPos = (GripAnchor.localPosition - OriginalTopPosition);

            if (NewBaseToGripNorm != BaseToGripNorm)
            {
                BaseToGripNorm = NewBaseToGripNorm;
                TopAnchor.localPosition = BaseToGripNorm;// * BaseToTopDist;
            }
            DistToOriginalTopPosition = (GripAnchor.localPosition - OriginalTopPosition).magnitude;

            DistToSnap = (HandleAnchor.localPosition - NearestY).magnitude;

            if (GripObject.isGrabbed)
            {
                LastGrabbedBy = GrabbedBy;
                OVRInput.SetControllerVibration(1, 1, GrabbedBy);
                ThrottleYOut = (HandleAnchor.localPosition.z - BaseAnchor.localPosition.z - offset) * 10;
            }
            else
            {
                if(LastGrabbedBy != OVRInput.Controller.None)
                {
                    OVRInput.SetControllerVibration(0, 0, LastGrabbedBy);
                    LastGrabbedBy = OVRInput.Controller.None;
                }
            }
        }

        private void RotateThrottle()
        {
            ThrottleVis.LookAt(TopAnchor.position, -1 * BaseAnchor.forward);
        }

        // Change so it clips onto a given position and vibrates controller
        private IEnumerator OnLetGo()
        {
            Vector3 StartPos = GripAnchor.localPosition;
            NearestY = FindNearestY();
            float journeyLength = Vector3.Distance(StartPos, NearestY);
            startTime = Time.time;
            float NearestYVal = FindNearestYVal();
            Debug.LogFormat("Nearest Y : ({0}, {1}, {2})", NearestY.x, NearestY.y, NearestY.z);
            while (!GripObject.isGrabbed && (Mathf.Abs(ThrottleYOut - NearestYVal) > 0.02f))
            {
                float distCovered = (Time.time - startTime) * speed;
                // Fraction of journey completed equals current distance divided by total distance.
                float fractionOfJourney = distCovered / journeyLength;
                GripAnchor.localPosition = Vector3.Lerp(StartPos, NearestY, fractionOfJourney);
                yield return null;
            }

            GripAnchor.localRotation = new Quaternion();
            Debug.Log("Should reset Rotation");
            GripAnchor.localPosition = NearestY;

            Debug.Log("Complete let go");
            yield return null;
        }

        public float GetThrottleOut()
        {
            return ThrottleYOut;
        }

        public Vector3 GetReticleOut()
        {
            if (GrabbedFlag)
            {
                return OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, GrabbedBy);
            }
            else return new Vector3();
        }

        private Vector3 FindNearestY()
        {
            if (ThrottleYOut > 0.6)
                return SnapPositions[2].localPosition;
            else if (ThrottleYOut > -0.6)
                return SnapPositions[1].localPosition;
            else
                return SnapPositions[0].localPosition;
        }

        private float FindNearestYVal()
        {
            if (ThrottleYOut > 0.6)
                ThrottleYOut = ThrottleYSnap[2];
            else if (ThrottleYOut > -0.6)
                ThrottleYOut = ThrottleYSnap[1];
            else
                ThrottleYOut = ThrottleYSnap[0];
            return ThrottleYOut;
            /*
            float LastFound = 100;
            foreach (float SnapTo in ThrottleYSnap)
            {
                if (Mathf.Abs(ThrottleYOut - SnapTo) < Mathf.Abs(ThrottleYOut - LastFound))
                {
                    LastFound = SnapTo;
                }
            }
            return LastFound;*/
        }
    }
}