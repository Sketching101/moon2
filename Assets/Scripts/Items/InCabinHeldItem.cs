using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AltVRGrabbable))]
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public abstract class InCabinHeldItem : MonoBehaviour
{
    [Header("Associated Held Item Prefab")]
    public GameObject heldItemObj;

    [Header("Active Held Items")]
    public HeldItem heldItem;

    [SerializeField]protected List<AltVRGrabber> grabberHover;

    public abstract void OnLetGo();

    public abstract void OnGrab();

    public abstract void OnHoverEnter();

    public abstract void OnHoverExit();

    protected void InCabinAwake()
    {
        grabberHover = new List<AltVRGrabber>();
    }


    protected void OnTriggerEnter(Collider other)
    {

        AltVRGrabber grabber = other.gameObject.GetComponent<AltVRGrabber>();
        if (grabber != null && !grabberHover.Contains(grabber))
        {
            grabberHover.Add(grabber);
            OnHoverEnter();
        }
    }

    protected void OnTriggerExit(Collider other)
    {
        AltVRGrabber grabber = other.gameObject.GetComponent<AltVRGrabber>();
        if (grabber != null && grabberHover.Contains(grabber))
        {
            grabberHover.Remove(grabber);
            if(grabberHover.Count == 0)
            {
                OnHoverExit();
            }
        }
    }
}
