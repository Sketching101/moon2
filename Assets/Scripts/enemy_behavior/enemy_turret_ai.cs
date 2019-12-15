using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_turret_ai : Enemy
{
    public float angleBetween = 0.0f;
    public Transform createAt;
    public float timeBetweenFire;
    public GameObject model;

    public GameObject rocketProjectile;

    public Transform ExplodeAt;
    public float HP = 100.0f;

    // Start is called before the first frame update
    void Start()
    {
        elapsed = Time.deltaTime;
        //  hp = 10.0f;
        alive = true;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (alive)
        {
            //hp -= 0.01f;
            if (HP <= 0)
            {
                StartCoroutine(Dying());
            }

            //transform.LookAt(target);


            elapsed += Time.deltaTime;

            if (elapsed > timeBetweenFire)
            {
                shoot(elapsed);
                elapsed = 0.0f;
                //need offset for shot
            }
        }

    }

    public override void ChangeHP(float changeBy)
    {
        HP += changeBy;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "bullet" && alive)
        {
            HP -= 10;
            if (other.gameObject.GetComponent<RocketController>() != null)
            {
                HP -= 10;
                explosion.Play();
                blastSound.Play();
            }
        }
        if (other.gameObject.tag != "enemy_bullet" && other.gameObject.tag != "Enemy")
        {
            HP -= 10;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "bullet" && alive)
        {
            HP -= 10;
            if (other.gameObject.GetComponent<RocketController>() != null)
            {
                HP -= 10;
                explosion.Play();
                blastSound.Play();
            }
        }
        if (other.gameObject.tag != "enemy_bullet" && other.gameObject.tag != "Enemy")
        {
            HP -= 10;
            ExplodeAt.position = other.GetContact(0).point;
        }
    }

    void shoot(float time)
    {
        GameObject tempObj;

        tempObj = Instantiate(rocketProjectile, createAt.position, transform.rotation) as GameObject;
        Vector3 targetDir = target.position - transform.position;

        //targetDir += Vector3(0, 0, 1);
        angleBetween = Vector3.Angle(transform.forward, targetDir);
        tempObj.transform.LookAt(target);

        //Get the Rigidbody that is attached to that instantiated bullet
        Rigidbody projectile = tempObj.GetComponent<Rigidbody>();

        //Shoot the Bullet 
        projectile.velocity = tempObj.transform.forward * 50;
    }

    IEnumerator Dying()
    {
        DisableColliders();
        alive = false;
        PlayerStats.Instance.Score += 100;
        explosion.Play();
        if (!slicedCopy)
            blastSound.Play();
        yield return null;

        GetComponent<Rigidbody>().useGravity = true;
        while (explosion.isPlaying)
        {
            GetComponent<Rigidbody>().AddForceAtPosition(ExplodeAt.right * 5, ExplodeAt.position);
            yield return null;
        }
        if(spawner != null)
        {
            spawner.currEnemy = null;
        }
        GetComponent<Rigidbody>().isKinematic = true;
        Destroy(model);
        explosion.Play();
        if (!slicedCopy)
            blastSound.Play();

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

    public void set_values(Transform new_target) {
        target = new_target;
    }

    public bool get_alive()
    {
        return (alive);
    }
}
