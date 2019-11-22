using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ManualControls;

public class AltVRGrabber : MonoBehaviour {
    [SerializeField]
    public OVRInput.Controller Controller;

    public AltVRGrabbable GrabbedObject = null;

    public List<AltVRGrabbable> GrabbableInRange;

    public Transform HandAnchor;

    public Transform MechGrabberTr;

	// Use this for initialization
	void Start () {
        GrabbableInRange = new List<AltVRGrabbable>();
	}
	
	// Update is called once per frame
	void Update () {
        if (HandAnchor != null)
        {
            transform.position = HandAnchor.position;
            transform.rotation = HandAnchor.rotation;
        }

        if (OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, Controller) && GrabbableInRange.Count > 0)
        {
            GrabObject(GrabbableInRange[0]);
        } else
        {
            LetGoObject();
        }

        if(GrabbedObject != null)
        {
            GrabbedObject.transform.position = transform.position;
        }

        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Grabbable" && GrabbedObject == null)
        {
            GrabbableInRange.Add(other.gameObject.GetComponent<AltVRGrabbable>());
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Grabbable" && GrabbedObject != other.gameObject.GetComponent<AltVRGrabbable>())
        {
            GrabbableInRange.Remove(other.gameObject.GetComponent<AltVRGrabbable>());
        }
    }

    private void GrabObject(AltVRGrabbable grabbed)
    {
        if (GrabbedObject == null)
        {
            GrabbedObject = grabbed;
            GrabbedObject.m_grabbedBy = this;
        }
    }

    private void LetGoObject()
    {
        if (GrabbedObject != null)
        {
            GrabbedObject.m_grabbedBy = null;
            GrabbedObject = null;
        }
    }
}
