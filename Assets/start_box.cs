using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class start_box : MonoBehaviour {

    public OVRGrabbable start_grab;
    public OVRGrabbable quit_grab;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (start_grab.isGrabbed) {
            SceneManager.LoadScene("main");
        }
        if (quit_grab.isGrabbed) {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
	}
}
