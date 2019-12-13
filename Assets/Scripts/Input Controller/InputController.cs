using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InputController : MonoBehaviour
{
    public static InputController Instance { get; private set; } // static singleton

    [Header("Button Mappings")]
    [SerializeField]
    private OVRInput.RawButton PauseButton;
    [SerializeField]
    private OVRInput.RawButton ResetViewButton;


    [HideInInspector]
    public bool Pause, PauseDown;
    [HideInInspector]
    public bool ResetView, ResetViewDown;
    [HideInInspector]
    public bool RIdxTrigger, RIdxTriggerDown;
    [HideInInspector]
    public bool RHTrigger, RHTriggerDown;
    [HideInInspector]
    public bool LIdxTrigger, LIdxTriggerDown;
    [HideInInspector]
    public bool LHTrigger, LHTriggerDown;

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


    // On changes in input value, update button values
    void LateUpdate()
    {
        GetButtons();
    }

    private void GetButtons()
    {
        bool button_buf;
        if ((button_buf = OVRInput.Get(PauseButton)) != Pause)
        {
            Pause = button_buf;
        }
        if ((button_buf = OVRInput.GetDown(PauseButton)) != PauseDown)
        {
            PauseDown = button_buf;
        }

        if ((button_buf = OVRInput.Get(ResetViewButton)) != ResetView)
        {
            ResetView = button_buf;
        }
        if ((button_buf = OVRInput.GetDown(ResetViewButton)) != ResetViewDown)
        {
            ResetViewDown = button_buf;
        }

        if ((button_buf = OVRInput.Get(OVRInput.RawButton.RIndexTrigger)) != RIdxTrigger)
        {
            RIdxTrigger = button_buf;
        }
        if ((button_buf = OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger)) != RIdxTriggerDown)
        {
            RIdxTriggerDown = button_buf;
        }

        if ((button_buf = OVRInput.Get(OVRInput.RawButton.LIndexTrigger)) != LIdxTrigger)
        {
            LIdxTrigger = button_buf;
        }
        if ((button_buf = OVRInput.GetDown(OVRInput.RawButton.LIndexTrigger)) != LIdxTriggerDown)
        {
            LIdxTriggerDown = button_buf;
        }

        if ((button_buf = OVRInput.Get(OVRInput.RawButton.RHandTrigger)) != RHTrigger)
        {
            RHTrigger = button_buf;
        }
        if ((button_buf = OVRInput.GetDown(OVRInput.RawButton.RHandTrigger)) != RHTriggerDown)
        {
            RHTriggerDown = button_buf;
        }

        if ((button_buf = OVRInput.Get(OVRInput.RawButton.LHandTrigger)) != LHTrigger)
        {
            LHTrigger = button_buf;
        }
        if ((button_buf = OVRInput.GetDown(OVRInput.RawButton.LHandTrigger)) != LHTriggerDown)
        {
            LHTriggerDown = button_buf;
        }
    }
}
