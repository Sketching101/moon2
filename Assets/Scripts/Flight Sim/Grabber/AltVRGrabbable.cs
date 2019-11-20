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

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(SetRotation && isGrabbed)
        {
            transform.rotation = m_grabbedBy.transform.rotation;
        }
	}

    void OnTriggerEnter(Collider other)
    {
        
    }
}
