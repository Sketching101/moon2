using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour {

    public static ScoreController Instance { get; private set; } // static singleton

    public int score = 0;

    // Use this for initialization
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
    }

    public void incScore(int increaseBy)
    {
        score += increaseBy;
    }
}
