using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ShipControllerNS;

public class InteractiveTutorial : MonoBehaviour {
    public collision_spheres TargetAnchor;
    public Text[] Texts;
    public ShipController ship;

    int state = 0;

	// Use this for initialization
	void Start () {
        Texts[0].enabled = true;
        Texts[1].enabled = false;
        Texts[2].enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        switch(state)
        {
            case 0:
                ship.MinVelocity = 0;
                ship.MaxVelocity = 0;
                state++;
                break;
            case 1:
                if(!TargetAnchor.visible)
                {
                    state++;
                    Texts[0].enabled = false;
                    Texts[1].enabled = true;
                }
                break;
            case 2:
                ship.MinVelocity = 0;
                ship.MaxVelocity = 500;
                ship.Axelaration = 10;
                state++;
                break;
            case 3:
                if(ship.velocity_display >= 30)
                {
                    ship.MinVelocity = 30;
                    state++;
                    Texts[1].enabled = false;
                    Texts[2].enabled = true;
                }
                break;
            default:
                break;
                

        }
	}
}
