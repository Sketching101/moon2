using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechGrabbable : MonoBehaviour
{
    public MechGrabber grabbedBy;
    public MechGripAnchor grabbedAt;
    public Rigidbody rb;
    public Transform grabbableParent;
    public Transform grabbableParentParent;
    public HeldItem item;
    public bool defaultKinematic;
    bool grabbed = false;
    bool isKinematicFlag;

    // Start is called before the first frame update
    void Awake()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody>();
    }

    public void Grab(MechGripAnchor grabbedAt_)
    {
        grabbed = true;
        grabbedAt = grabbedAt_;
        grabbedBy = grabbedAt.grabbedBy;
        rb = grabbedAt_.rb;
        isKinematicFlag = rb.isKinematic;
        rb.isKinematic = defaultKinematic;
        
        grabbedAt.transform.SetParent(grabbableParentParent, true);
        grabbableParent.SetParent(grabbedAt.transform, true);
    }

    public void Release()
    {
        grabbableParent.SetParent(grabbableParentParent, true);
        grabbed = false;
        grabbedBy = null;
        grabbedAt = null;
        rb.isKinematic = isKinematicFlag;
        rb = null;
    }
}
