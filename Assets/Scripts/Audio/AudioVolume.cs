using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioVolume : MonoBehaviour
{
    [SerializeField]
    private AudioMixer masterMixer;

    public bool testAudio;

    public void AdjustMusicVolume(float newVolume)
    {
        masterMixer.SetFloat("volumeMusic", newVolume);
    }

    public void AdjustSFXVolume(float newVolume)
    {
        masterMixer.SetFloat("volumeSFX", newVolume);
    }

    public void AdjustMasterVolume(float newVolume)
    {
        Debug.Log(masterMixer.SetFloat("volumeMaster", newVolume));
    }
}
