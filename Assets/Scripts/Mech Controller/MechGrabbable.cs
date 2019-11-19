using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechGrabbable : MonoBehaviour
{
    public MechGrabber grabbedBy;
    public MechGripAnchor grabbedAt;
    public Rigidbody rb;

    bool grabbed = false;
    bool isKinematicFlag;

    // Start is called before the first frame update
    void Awake()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        /*if(grabbedBy != null && grabbedBy.gripAnchor != null)
        {
            grabbedAt.transform.position = grabbedBy.gripAnchor.position;
            grabbedAt.transform.rotation = grabbedBy.gripAnchor.rotation;
        }*/
    }

    public void Grab(MechGripAnchor grabbedAt_)
    {
        grabbed = true;
        grabbedAt = grabbedAt_;
        grabbedBy = grabbedAt.grabbedBy;
        rb = grabbedAt_.rb;
        isKinematicFlag = rb.isKinematic;
        rb.isKinematic = false;
    }

    public void Release()
    {
        grabbed = false;
        grabbedBy = null;
        grabbedAt = null;
        rb.isKinematic = isKinematicFlag;
        rb = null;
    }
}
