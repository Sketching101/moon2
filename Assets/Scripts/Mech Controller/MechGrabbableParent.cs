using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechGrabbableParent : MonoBehaviour
{
    public MechActiveGrip ActiveAnchor;
    public Transform GrabObject;

    // Update is called once per frame
    void Update()
    {
        if(ActiveAnchor.activeGrip != null && ActiveAnchor.activeGrip.grabbedBy != null)
        {
            ActiveAnchor.activeGrip.transform.position = ActiveAnchor.activeGrip.grabbedBy.transform.position;
            ActiveAnchor.activeGrip.transform.rotation = ActiveAnchor.activeGrip.grabbedBy.transform.rotation;


            GrabObject.position = ActiveAnchor.activeGrip.grabbedBy.transform.position;
            GrabObject.rotation = ActiveAnchor.activeGrip.grabbedBy.transform.rotation;
        }
    }
}
