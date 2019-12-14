using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioDisplay : MonoBehaviour
{
    public Slider slider;
    public AudioMixer audioMix;
    public string volName;
    private float max = 20;
    private float min = -20;
    public GameObject highlight;
    public float volume;

    void Awake()
    {
        slider.maxValue = max;
        slider.minValue = min;
        audioMix.GetFloat(volName, out volume);
        ChangeVolume(0);
    }

    public void SetActiveHighlight(bool active)
    {
        highlight.SetActive(active);
    }

    public void ChangeVolume(float diffVolume)
    {
        volume = diffVolume + volume;
        float setVolume = volume;
        if (volume <= min)
        {
            volume = min;
            setVolume = -80;
        }
        else if(volume >= max)
        {
            volume = max;
            setVolume = max;
        }
        audioMix.SetFloat(volName, setVolume);
        slider.value = volume;
    }
}
