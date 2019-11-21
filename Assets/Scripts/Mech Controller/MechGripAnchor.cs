using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechGripAnchor : MonoBehaviour
{
    [Header("Grab Scripts")]
    public MechGrabbable grabObject;
    public MechGrabber grabbedBy;

    [Header("Misc")]
    public Transform gripParent;
    public Rigidbody rb;

    public Vector3 OrigLocPos;
    public Quaternion OrigRotation;

    // Update is called once per frame
    void Awake()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody>();
        if (gripParent == null)
            gripParent = transform.parent;

        OrigLocPos = transform.localPosition;
        OrigRotation = transform.localRotation;
    }

    public void Grab(MechGrabber grabber)
    {
        if (grabObject.grabbedBy == null)
        {
            grabbedBy = grabber;
            grabObject.rb = rb;
            grabObject.Grab(this);
        }
    }

    public void Release()
    {
        if (grabObject.grabbedBy != null)
        {
            grabbedBy = null;
            grabObject.Release();
            transform.SetParent(gripParent, true);
            transform.localPosition = OrigLocPos;
            transform.localRotation = OrigRotation;
        }
    }
}
