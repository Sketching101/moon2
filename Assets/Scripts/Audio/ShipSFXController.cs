using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSFXController : MonoBehaviour
{
    // Declare any public variables that you want to be able 
    // to access throughout your scene
    public static ShipSFXController Instance { get; private set; } // static singleton

    [Header("Audio Outputs")]
    [SerializeField]
    private AudioClip[] SourceAudio;
    [SerializeField]
    private AudioSource SourceSFX;

    void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(gameObject); }
    }
    
    public void PlayClip(int idx)
    {
        SourceSFX.clip = SourceAudio[idx];
        SourceSFX.Play();
    }
}
