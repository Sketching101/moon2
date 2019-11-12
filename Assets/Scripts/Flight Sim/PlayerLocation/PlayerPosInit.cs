using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPosInit : MonoBehaviour {

    public Transform PlayerPos;
    public Transform CameraPos;
    public Transform TranslateToPos;

	// Use this for initialization
	void Awake () {
        PlayerPos.position = TranslateToPos.position - CameraPos.localPosition;
        Debug.Log(TranslateToPos.localPosition);
	}

   /* void Update()
    {
        if (PlayerPos.localPosition != TranslateToPos.localPosition)
        {
            PlayerPos.localPosition = TranslateToPos.localPosition;
        }    
    }*/
}
