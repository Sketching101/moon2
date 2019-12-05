using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enemy_spawner spawner;

    public float movementSpeed = 20.0f;
    public float elapsed = 0.0f;
    public AudioSource blastSound;
    public ParticleSystem explosion;
    public Transform target;

    public bool alive;

}
