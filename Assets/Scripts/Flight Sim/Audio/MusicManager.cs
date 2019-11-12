using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MusicManager : MonoBehaviour {

    public System.Random rng = new System.Random();

    public AudioClip[] MusicToShuffle;
    public AudioClip YouAreDead;
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


        if(!playingDead && PullUpMenu.Instance.gameState == PullUpMenu.GameState.Dead)
        {
            playingDead = true;
            MusicSpeakers.clip = YouAreDead;
            MusicSpeakers.Play();
        }
        else if (!MusicSpeakers.isPlaying && Music.Count > 0)
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
