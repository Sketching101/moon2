using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour {
    public GameObject TutorialParent;
	// Use this for initialization
	void Start () {
		if(PullUpMenu.Instance.gameMode != PullUpMenu.GameMode.Tutorial)
        {
            if (TutorialParent != null)
                TutorialParent.SetActive(false);
        } else
        {
            if (TutorialParent != null)
                TutorialParent.SetActive(true);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
