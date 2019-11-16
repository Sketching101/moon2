using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MusicManager : MonoBehaviour {

    public System.Random rng = new System.Random();

    [Header("In-Game Music")]
    public AudioClip[] MusicToShuffle;
    [Header("Menu Music")]
    public AudioClip YouAreDead;
    [Header("Music Source")]
    public AudioSource MusicSpeakers;

    int musicIdx = 0;
    bool playingDead = false;
    List<int> Music;

    void Awake()
    {
        Music = new List<int>();
        for(int i = 0; i < MusicToShuffle.Length; i++)
        {
            Music.Add(i);
        }

        Music = ShuffleList(Music);
    }

    // Update is called once per frame
    void Update () {

        if (!MusicSpeakers.isPlaying && Music.Count > 0)
        {
            MusicSpeakers.clip = MusicToShuffle[Music[musicIdx]];
            musicIdx++;
            if (musicIdx >= MusicToShuffle.Length)
                musicIdx = 0;

            MusicSpeakers.Play();
        }
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
}
