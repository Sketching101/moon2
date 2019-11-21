using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltVRGrabbable : MonoBehaviour {

    public AltVRGrabber m_grabbedBy = null;
    public bool SetRotation;
    /// <summary>
    /// If true, the object is currently grabbed.
    /// </summary>
    public bool isGrabbed
    {
        get { return m_grabbedBy != null; }
    }

    private bool grabbedLastFrame = false;

    public bool grabbedThisFrame = false;
    public bool letGoThisFrame = false;

    public InCabinHeldItem heldItem;

    private void Awake()
    {
        if(heldItem == null)
        {
            heldItem = GetComponent<InCabinHeldItem>();
        }
    }

    // Update is called once per frame
    void Update () {
        grabbedThisFrame = (!grabbedLastFrame && isGrabbed);
        letGoThisFrame = (grabbedLastFrame && !isGrabbed);

		if(SetRotation && isGrabbed)
        {
            transform.rotation = m_grabbedBy.transform.rotation;
        }

        if(grabbedThisFrame && heldItem != null)
        {
            heldItem.OnGrab();
        } else if(letGoThisFrame && heldItem != null)
        {
            heldItem.OnLetGo();
        }
        grabbedLastFrame = isGrabbed;
    }
}
