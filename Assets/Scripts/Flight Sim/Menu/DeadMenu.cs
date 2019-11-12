using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadMenu : MonoBehaviour {


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(PullUpMenu.Instance.gameState == PullUpMenu.GameState.Dead)
        {
            if (OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.RTouch))
            {
                if (PullUpMenu.Instance.gameMode == PullUpMenu.GameMode.Regular)
                    MenuSelect.Instance.StartGame();
                else if (PullUpMenu.Instance.gameMode == PullUpMenu.GameMode.Tutorial)
                    MenuSelect.Instance.StartTutorial();
            } else if(OVRInput.GetDown(OVRInput.Button.Two, OVRInput.Controller.RTouch))
            {
                MenuSelect.Instance.MainMenu();
            } else if(OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
            {
                MenuSelect.Instance.ExitGame();
            }
        }
	}
}
