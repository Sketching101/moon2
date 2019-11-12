using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ManualControls;
using UnityEngine.UI;

public class MenuSelect : MonoBehaviour
{
    public Button[] Buttons;
    public RectTransform[] ButtonHighlight;

    public Throttle throttle;
    public Joystick joystick;

    [SerializeField] int buttonIdx = 0;

    int SetLast = 0;
    [SerializeField] bool canSet = false;

    [SerializeField] bool canHighlight = true;

    public static MenuSelect Instance { get; private set; } // static singleton

    bool Menu, OldMenu;


    // Use this for initialization
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Update () {
        if(PullUpMenu.Instance.gameState == PullUpMenu.GameState.Playing)
        {
            return;
        }

        Menu = (PullUpMenu.Instance.gameState == PullUpMenu.GameState.Paused || PullUpMenu.Instance.gameState == PullUpMenu.GameState.MainMenu || PullUpMenu.Instance.gameState == PullUpMenu.GameState.Dead);

        if (joystick.GetJoystickOut().x > 0 && canSet && Menu)
        {
            if(buttonIdx < Buttons.Length - 1)
            {
                buttonIdx++;
            }
            SetLast = buttonIdx;
            canSet = false;
            canHighlight = true;
        }
        else if(joystick.GetJoystickOut().x < 0 && canSet && Menu)
        {
            if(buttonIdx > 0)
            {
                buttonIdx--;
            }
            SetLast = buttonIdx;
            canSet = false;
            canHighlight = true;
        }
        else if(joystick.GetJoystickOut().x == 0 && throttle.GetThrottleOut() == 0 && Menu)
        {
            canSet = true;
        }

        if(canHighlight && Menu)
        {
            foreach(RectTransform rt in ButtonHighlight)
            {
                rt.gameObject.SetActive(false);
            }

            ButtonHighlight[buttonIdx].gameObject.SetActive(true);
            canHighlight = false;
        }

        if (!(OldMenu != Menu && throttle.GetThrottleOut() != 0))
        {

            if (throttle.GetThrottleOut() == 1 && canSet && Menu)
            {
                Buttons[buttonIdx].onClick.Invoke();
            }

            OldMenu = Menu;
        }
	}
    
    public void ExitGame()
    {
        SceneController.Instance.ExitGame();
    }

    public void StartGame()
    {
        SceneController.Instance.StartGame();
        PullUpMenu.Instance.gameMode = PullUpMenu.GameMode.Regular;
        PullUpMenu.Instance.gameState = PullUpMenu.GameState.Playing;
    }

    public void MainMenu()
    {
        PullUpMenu.Instance.gameMode = PullUpMenu.GameMode.None;
        PullUpMenu.Instance.gameState = PullUpMenu.GameState.MainMenu;

        SceneController.Instance.ToMainMenu();
    }

    public void StartTutorial()
    {
        PullUpMenu.Instance.gameMode = PullUpMenu.GameMode.Tutorial;
        SceneController.Instance.StartTutorial();
        PullUpMenu.Instance.gameState = PullUpMenu.GameState.Playing;
    }

    public void LoseGame()
    {
        PullUpMenu.Instance.gameState = PullUpMenu.GameState.Dead;
    }
}
