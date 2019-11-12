using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullUpMenu : MonoBehaviour {

    public static PullUpMenu Instance { get; private set; } // static singleton

    public bool Paused = false;

    public UIController UICanvas;

    public enum GameState
    {
        Paused, MainMenu, Playing, Dead
    }

    public enum GameMode
    {
        None, Regular, Tutorial
    }
    public GameMode gameMode;
    public GameState gameState;

    int PausedFrameCount = 0;

    public bool PausedFrame = false;

    void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(gameObject); }
    }

    void Update()
    {
        if(PausedFrame)
        {
            PausedFrameCount++;
        }
        if(PausedFrameCount > 3)
            PausedFrame = false;

        if(OVRInput.GetDown(OVRInput.Button.Two, OVRInput.Controller.RTouch) && gameState != GameState.MainMenu && gameState != GameState.Dead)
        {
            Paused = !Paused;
            if(Paused)
            {
                PausedFrame = true;
                gameState = GameState.Paused;
            } else
            {
                gameState = GameState.Playing;
            }


        }
    }
}
