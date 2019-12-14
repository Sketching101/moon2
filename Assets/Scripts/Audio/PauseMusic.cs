using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PauseMusic : MonoBehaviour
{
    public bool pauseMusic;
    private bool musicPaused;
    public AudioMixerSnapshot paused;
    public AudioMixerSnapshot unpaused;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(pauseMusic)
        {
            pauseMusic = false;
            Pause();
        }
    }

    public void Pause()
    {
        Lowpass();
    }

    public void Lowpass()
    {
        if (musicPaused)
        {
            paused.TransitionTo(.01f);
            musicPaused = false;
        }
        else
        {
            musicPaused = true;
            unpaused.TransitionTo(.01f);
        }
    }
}
