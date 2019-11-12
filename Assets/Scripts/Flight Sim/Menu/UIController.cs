using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {
    [SerializeField]
    private RectTransform[] PauseState;
    [SerializeField]
    private GameObject[] SelectMenu;

    [SerializeField]
    private int state_t = 0;
    public int nextState = 0;

    private bool LastPaused = false;

	// Use this for initialization
	void Start () {
        LastPaused = PullUpMenu.Instance.Paused;
    }

    // Update is called once per frame
    void Update () {
        if(LastPaused != PullUpMenu.Instance.Paused)
        {
            if (PullUpMenu.Instance.Paused)
                nextState = 2;
            else
                nextState = 1;
        }

        StateMachine();

        LastPaused = PullUpMenu.Instance.Paused;
	}

    private void StateMachine()
    {
        state_t = nextState;
        switch (state_t)
        {
            case 0: // idle
                nextState = 0;
                break;
            case 1: // Close menu
                foreach (RectTransform MenuItem in PauseState)
                {
                    MenuItem.gameObject.SetActive(false);
                }
                nextState = 0;
                break;
            case 2: // Open menu
                foreach (RectTransform MenuItem in PauseState)
                {
                    MenuItem.gameObject.SetActive(true);
                }
                nextState = 0;
                break;
        }
    }
}
