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
        rb.isKinematic = false;
        grabbedAt.transform.SetParent(grabbableParentParent, true);
        grabbableParent.SetParent(grabbedAt.transform, true);
    }

    public void Release()
    {
        Debug.Log("Check");
        grabbableParent.SetParent(grabbableParentParent, true);
        grabbed = false;
        grabbedBy = null;
        grabbedAt = null;
        rb.isKinematic = isKinematicFlag;
        rb = null;
    }
}
