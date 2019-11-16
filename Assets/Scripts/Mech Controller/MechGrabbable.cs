using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechGrabbable : MonoBehaviour
{
    public MechGrabber grabbedBy;
    public Transform gripAnchor;
    public Rigidbody rb;

    bool grabbed = false;
    bool isKinematicFlag;

    // Start is called before the first frame update
    void Start()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(grabbedBy != null && grabbedBy.gripAnchor != null)
        {
            gripAnchor.position = grabbedBy.gripAnchor.position;
            gripAnchor.rotation = grabbedBy.gripAnchor.rotation;
        }
    }

    public void Grab(MechGrabber grabber)
    {
        grabbed = true;
        grabbedBy = grabber;
        isKinematicFlag = rb.isKinematic;
        rb.isKinematic = false;
    }

    public void Release()
    {
        grabbed = false;
        grabbedBy = null;
        rb.isKinematic = isKinematicFlag;
    }
}
