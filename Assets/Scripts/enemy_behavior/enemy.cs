using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemySpawner spawner;

    public float movementSpeed = 20.0f;
    public float elapsed = 0.0f;
    public AudioSource blastSound;
    public ParticleSystem explosion;
    public Transform target;

    public bool alive;

    protected void DisableColliders()
    {
        Collider[] colliders = GetComponents<Collider>();
        foreach(Collider col in colliders)
        {
            col.enabled = false;
        }
    }

    public virtual void ChangeHP(float changeBy)
    {

    }
}
