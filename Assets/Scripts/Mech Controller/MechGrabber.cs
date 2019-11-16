using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechGrabber : MonoBehaviour
{
    [Header("Mech Hand Objects")]
    public HandController handController;
    public Transform gripAnchor;

    [Header("Player Object")]
    public Transform PlayerPos;

    [Header("Grabbed Object")]
    [SerializeField] private MechGrabbable Grabbed;
    [SerializeField] private List<MechGrabbable> GrabRange;
    public bool CanGrab = false;

    void Awake()
    {
        GrabRange = new List<MechGrabbable>();
        if(gripAnchor == null)
        {
            gripAnchor = transform;
        }
    }

    private void Update()
    {
        if(GrabRange.Count > 0 && Grabbed == null && handController.handState != HandState.Open)
        {
            GrabObject();
        } else if(Grabbed != null && handController.handState == HandState.Open)
        {
            ReleaseObject();
        }
    }

    private bool GrabObject()
    {
        Grabbed = GrabRange[0];
        Grabbed.Grab(this);
        GrabRange.Remove(Grabbed);
        return true;
    }

    private void ReleaseObject()
    {
        Grabbed.grabbedBy = null;
        Grabbed = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        MechGrabbable grab;
        if ((grab = other.GetComponent<MechGrabbable>()) != null && handController.handState == HandState.Open)
        {
            GrabRange.Add(grab);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        MechGrabbable grab;
        if ((grab = other.GetComponent<MechGrabbable>()) != null && handController.handState == HandState.Open && !GrabRange.Contains(grab))
        {
            GrabRange.Add(grab);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        MechGrabbable grab;
        if ((grab = other.GetComponent<MechGrabbable>()) != null && GrabRange.Contains(grab) && grab != Grabbed)
        {
            GrabRange.Remove(grab);
            if(GrabRange.Count == 0)
            {
                CanGrab = false;
            }
        }
    }

}
