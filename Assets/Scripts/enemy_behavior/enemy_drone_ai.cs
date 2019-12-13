﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_drone_ai : Enemy
{
    public float angleBetween = 0.0f;
    public float HP = 20.0f;

    // Start is called before the first frame update
    void Start()
    {
        elapsed = Time.deltaTime;
        //  hp = 15.0f;
        alive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (alive)
        {
            //hp -= 0.01f
            if (HP <= 0)
            {
                PlayerStats.Instance.Score += 50;
                StartCoroutine(Dying());
            }
            Vector3 targetDir = target.position - transform.position;


            if (Vector3.Distance(target.position, transform.position) <= 2.0f)
            {
                explode();
            }


            angleBetween = Vector3.Angle(transform.forward, targetDir);
            transform.LookAt(target);

            transform.position += transform.forward * Time.deltaTime * movementSpeed;

            elapsed += Time.deltaTime;

            if (elapsed >= 5.0f)
            {
                movementSpeed = 40.0f;
            }

        }

    }

    public override void ChangeHP(float changeBy)
    {
        HP += changeBy;
    }

    private void OnTriggerEnter(Collider other)
    {
        HP -= 10;
        if (other.gameObject.tag == "Player" && alive)
        {
            alive = false;
            PlayerStats.Instance.HP -= 200;
            StartCoroutine(Dying());
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag != "enemy_bullet" && other.gameObject.tag != "Enemy")
            HP -= 10;

        if (other.gameObject.tag == "Player" && alive)
        {
            alive = false;
            PlayerStats.Instance.HP -= 200;
            StartCoroutine(Dying());
        }
    }

    private void explode()
    {
        HP -= 20;
    }

    IEnumerator Dying()
    {
        DisableColliders();
        alive = false;
        blastSound.Play();
        explosion.Play();
        yield return null;
        GetComponent<MeshRenderer>().enabled = false;
        while (explosion.isPlaying)
        {

            yield return null;
        }

        Destroy(gameObject);

    }

    public void set_target(Transform new_target)
    {

        target = new_target;
    }

    public void set_values(ParticleSystem value) {
        explosion = value;
    }

    public bool get_alive() {
        return (alive);
    }
}
