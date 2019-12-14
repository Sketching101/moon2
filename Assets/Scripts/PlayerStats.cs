using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float HP = 1000;
    public int Score;
    private float maxHP;

    public static PlayerStats Instance { get; private set; } // static singleton

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
        maxHP = HP;
    }

    public void AddHP(float addHP)
    {
        HP = Mathf.Min(maxHP, addHP + HP);
    }
}
