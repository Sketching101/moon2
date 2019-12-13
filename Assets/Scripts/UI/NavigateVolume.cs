using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigateVolume : MonoBehaviour
{
    public AudioDisplay[] audioDisplays;

    public int activeDisplay = 1;

    private float timeSinceSwitch = 2;
    private float resetTime = .3f;

    private bool canSwitch;
    float change;

    private void Awake()
    {
        SwitchDisplay(activeDisplay);
    }

    // Update is called once per frame
    void Update()
    {
        if(!PauseMenu.isPaused)
            return;

        timeSinceSwitch += Time.unscaledDeltaTime;
        canSwitch = (timeSinceSwitch > resetTime);

        if (canSwitch && (change = OVRInput.Get(OVRInput.RawAxis2D.LThumbstick).x) != 0)
        {
            if(change > 0f)
            {
                timeSinceSwitch = 0;
                activeDisplay++;
                if (activeDisplay >= audioDisplays.Length)
                    activeDisplay = 0;
                SwitchDisplay(activeDisplay);
            } else if(change < 0f)
            {
                timeSinceSwitch = 0;
                activeDisplay--;
                if (activeDisplay < 0)
                    activeDisplay = audioDisplays.Length - 1;
                SwitchDisplay(activeDisplay);
            }
        }

        if((change = OVRInput.Get(OVRInput.RawAxis2D.RThumbstick).x) != 0)
        {
            audioDisplays[activeDisplay].ChangeVolume(change);
        }
    }

    private void SwitchDisplay(int activeDisp)
    {
        for(int i = 0; i < audioDisplays.Length; i++)
        {
            if(i == activeDisp)
            {
                audioDisplays[i].SetActiveHighlight(true);
            }
            else
            {
                audioDisplays[i].SetActiveHighlight(false);
            }
        }
    }


}
