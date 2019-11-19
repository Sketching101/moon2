using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechGripAnchor : MonoBehaviour
{
    [Header("Grab Scripts")]
    public MechGrabbable grabObject;
    public MechGrabber grabbedBy;
    public MechGrabbableParent grabParent;

    [Header("Misc")]
    public MechActiveGrip activeAnchorParent;
    public Transform gripParent;
    public Rigidbody rb;

    // Update is called once per frame
    void Awake()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody>();
        if (gripParent == null)
            gripParent = transform.parent;
    }

    public void Grab(MechGrabber grabber)
    {
        if (grabObject.grabbedBy == null)
        {
            grabbedBy = grabber;
            grabObject.rb = rb;
            grabObject.Grab(this);
            activeAnchorParent.SetActiveGrip(this);
        }
    }

    public void Release()
    {
        if (grabObject.grabbedBy != null)
        {
            grabbedBy = null;
            grabObject.Release();
            activeAnchorParent.ClearActiveGrip(gripParent);
        }
    }
}
