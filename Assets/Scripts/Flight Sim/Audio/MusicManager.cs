using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {

    public System.Random rng = new System.Random();

    public static MusicManager Instance { get; private set; }

    [Header("In-Game Music")]
    public AudioClip[] MusicToShuffle;
    [Header("Menu Music")]
    public AudioClip YouAreDead;
    [Header("Music Source")]
    public AudioSource MusicSpeakers;
    [SerializeField]
    private UnityEngine.Audio.AudioMixerGroup audioMixer;
    public bool DontPlay;

    int musicIdx = 0;
    bool playingDead = false;
    List<int> Music;

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

        Music = new List<int>();
        for(int i = 0; i < MusicToShuffle.Length; i++)
        {
            Music.Add(i);
        }

        MusicSpeakers.outputAudioMixerGroup = audioMixer;

        ShufflePlaylist();
    }

    // Update is called once per frame
    void Update () {
        if (DontPlay)
        {
            if (MusicSpeakers.isPlaying)
            {
                MusicSpeakers.Pause();
            }
        }
        else if (!MusicSpeakers.isPlaying && Music.Count > 0)
        {
            NextSong();
        }
    }

    public void NextSong()
    {
        if (musicIdx >= MusicToShuffle.Length - 1)
        {
            musicIdx = -1;
        }

        musicIdx++;

        MusicSpeakers.clip = MusicToShuffle[Music[musicIdx]];

        MusicSpeakers.Play();
    }

    public void PrevSong()
    {
        if (musicIdx <= 0)
        {
            musicIdx = MusicToShuffle.Length;
        }

        musicIdx--;

        MusicSpeakers.clip = MusicToShuffle[Music[musicIdx]];

        MusicSpeakers.Play();
    }

    public void ShufflePlaylist()
    {
        Music = ShuffleList(Music);
    }

    private List<E> ShuffleList<E>(List<E> inputList)
    {
        List<E> randomList = new List<E>();

        int randomIndex = 0;
        while (inputList.Count > 0)
        {
            randomIndex = rng.Next(0, inputList.Count); //Choose a random object in the list
            randomList.Add(inputList[randomIndex]); //add it to the new, random list
            inputList.RemoveAt(randomIndex); //remove to avoid duplicates
        }

        return randomList; //return the new random list
    }

    public void Pause()
    {
        if(MusicSpeakers.isPlaying)
        {
            MusicSpeakers.Pause();
        } else
        {
            MusicSpeakers.UnPause();
        }
    }
}
