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
    [SerializeField] private MechGripAnchor Grabbed;
    [SerializeField] private List<MechGripAnchor> GrabRange;
    public bool CanGrab = false;

    void Awake()
    {
        GrabRange = new List<MechGripAnchor>();
        if(gripAnchor == null)
        {
            gripAnchor = transform;
        }
    }

    private void Update()
    {
        if(GrabRange.Count > 0 && Grabbed == null && handController.handState != HandState.Open)
        {
            Debug.Log("Grabbing");
            GrabObject();
        } else if(Grabbed != null && handController.handState == HandState.Open)
        {
            ReleaseObject();
        }

        if(Grabbed != null)
        {
            Grabbed.transform.position = gripAnchor.position;

            Grabbed.transform.rotation = gripAnchor.rotation;
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
        Grabbed.Release();
        Grabbed = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        MechGripAnchor grab;
        Debug.Log("Trigger Enter");
        if ((grab = other.GetComponent<MechGripAnchor>()) != null && handController.handState == HandState.Open)
        {
            GrabRange.Add(grab);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        MechGripAnchor grab;
        if ((grab = other.GetComponent<MechGripAnchor>()) != null && handController.handState == HandState.Open && !GrabRange.Contains(grab))
        {
            GrabRange.Add(grab);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        MechGripAnchor grab;
        if ((grab = other.GetComponent<MechGripAnchor>()) != null && GrabRange.Contains(grab) && grab != Grabbed)
        {
            GrabRange.Remove(grab);
            if(GrabRange.Count == 0)
            {
                CanGrab = false;
            }
        }
    }

}
