using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ShipControllerNS;
using UnityEngine.UI;

public class ShipUI : MonoBehaviour
{
    public RectTransform HUD;
    public RectTransform Lost;

    public ShipController shipController;

    public Text VelocityDisplay;
    public Text ScoreDisplay;
    public Text ScoreDisplayDead;

    public Button PrimaryDisplayAc, PrimaryDisplayInAc;
    public Button SecondaryDisplayAc, SecondaryDisplayInAc;

    public Projectile projectileScript;

    bool died = false;

    bool PrimRdy = true;
    bool SecRdy = true;

	// Use this for initialization
	void Awake () {
        PrimaryDisplayAc.gameObject.SetActive(true);
        SecondaryDisplayAc.gameObject.SetActive(true);
        PrimaryDisplayInAc.gameObject.SetActive(false);
        SecondaryDisplayInAc.gameObject.SetActive(false);
        Lost.gameObject.SetActive(false);
        HUD.gameObject.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {
        if (PullUpMenu.Instance.gameState == PullUpMenu.GameState.Dead)
        {
            if(!died)
            {
                Debug.Log("Buff");
                Lost.gameObject.SetActive(true);
                ScoreDisplayDead.text = ScoreDisplay.text;

                HUD.gameObject.SetActive(false);
     
                died = true;
            }

            //if(OVRInput.GetDown(OVRInput.Button.Any))
            //{
            //    MenuSelect.Instance.MainMenu();
            //}
        }
        else
        {
            string st = shipController.velocity_display.ToString();
            st = st.Split('.')[0];
            VelocityDisplay.text = string.Format("{0} m/s", st);

            ScoreDisplay.text = string.Format("{0} pts", ScoreController.Instance.score);

            if (PrimRdy == projectileScript.CooldownPrimary)
            {
                PrimRdy = !projectileScript.CooldownPrimary;
                PrimaryDisplayAc.gameObject.SetActive(PrimRdy);
                PrimaryDisplayInAc.gameObject.SetActive(!PrimRdy);
            }


            if (SecRdy == projectileScript.CooldownSecondary)
            {
                SecRdy = !projectileScript.CooldownSecondary;
                SecondaryDisplayAc.gameObject.SetActive(SecRdy);
                SecondaryDisplayInAc.gameObject.SetActive(!SecRdy);
            }
        }
    }
}
