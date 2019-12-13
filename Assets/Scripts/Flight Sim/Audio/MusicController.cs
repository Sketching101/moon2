using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ManualControls;

public class MusicController : MonoBehaviour
{
    public Throttle playlistController;
    public float curVal = 0.0f;
    public float switchThreshold;
    public float switchThreshBuf;
    public bool switchedSong = false;
    float time_t = 0;
    float timeBeforeSwitch = 1f;

    // Update is called once per frame
    void Update()
    {
        curVal = playlistController.GetThrottleOut();
        time_t += Time.deltaTime;

        if(!switchedSong && curVal > switchThreshold)
        {
            switchedSong = true;
            MusicManager.Instance.NextSong();
            time_t = 0;
        } else if(!switchedSong && curVal < switchThreshold * -1)
        {
            switchedSong = true;
            MusicManager.Instance.PrevSong();
            time_t = 0;
        } else if(switchedSong && (curVal > switchThreshBuf * -1 && curVal < switchThreshBuf) && time_t > timeBeforeSwitch)
        {
            switchedSong = false;
        }
    }
}
