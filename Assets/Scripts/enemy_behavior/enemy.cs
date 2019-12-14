using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Rigidbody rb;
    public EnemySpawner spawner;
    public MeshRenderer mesh;
    public Collider[] colliders;

    public float movementSpeed = 20.0f;
    public float elapsed = 0.0f;
    public AudioSource blastSound;
    public ParticleSystem explosion;
    public Transform target;

    [HideInInspector]
    public bool slicedCopy = false;

    public bool alive;

    protected void DisableColliders()
    {
        foreach(Collider col in colliders)
        {
            if(col != null)
                col.enabled = false;
        }
    }

    public virtual void ChangeHP(float changeBy)
    {

    }

    public virtual void OnCut()
    {
        ChangeHP(-1000);
    }
}
