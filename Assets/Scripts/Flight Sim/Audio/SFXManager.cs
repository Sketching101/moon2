using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    // Declare any public variables that you want to be able 
    // to access throughout your scene
    public static SFXManager Instance { get; private set; } // static singleton

    [Header("Audio Outputs")]
    [SerializeField]
    private AudioSource[] SourceSFX;
    [SerializeField]
    private UnityEngine.Audio.AudioMixerGroup audioMixer;

    void Awake()
    {
        if (Instance == null) {
            Instance = this;
            for(int i = 0; i < SourceSFX.Length; i++)
            {
                SourceSFX[i].outputAudioMixerGroup = audioMixer;
            }
        }
        else {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Play the clip of a given object
    /// </summary>
    /// <param name="idx">0 : Left Cannon, 
    /// 1 : Right Cannon,
    /// 2 : Left Wing,
    /// 3 : Right Wing</param>
    public void PlayClip(int idx)
    {
        SourceSFX[idx].Play();
    }
}
