using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltVRGrabbable : MonoBehaviour {

    public AltVRGrabber m_grabbedBy = null;

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
		
	}

    void OnTriggerEnter(Collider other)
    {
        
    }
}
