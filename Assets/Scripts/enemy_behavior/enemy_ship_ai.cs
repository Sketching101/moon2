using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_ship_ai : Enemy
{
    public float angleBetween = 0.0f;
    public Transform createAt;

    public float timeBetweenFire;

    public GameObject rocketProjectile;
    public bool moving_right = true;

    public Transform ExplodeAt;

    public float HP = 20.0f;
    // Start is called before the first frame update
    void Start()
    {
        elapsed = Time.deltaTime;
        moving_right = true;
      //  hp = 15.0f;
        alive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (alive)
        {
            if (HP <= 0)
            {
                StartCoroutine(Dying());
            }
            Vector3 targetDir = target.position - transform.position;
            angleBetween = Vector3.Angle(transform.forward, targetDir);
            transform.LookAt(target);

            transform.position += transform.forward * Time.deltaTime * movementSpeed;

            elapsed += Time.deltaTime;

            if (elapsed > timeBetweenFire && moving_right)
            {
                elapsed = 0.0f;
                moving_right = false;
                shoot();
            }

            if (elapsed > timeBetweenFire && !moving_right)
            {
                elapsed = 0.0f;
                moving_right = true;
                shoot();
            }


            if (moving_right)
            {
                transform.position += transform.right * Time.deltaTime * movementSpeed * 2;
            }
            else if (!moving_right)
            {
                transform.position -= transform.right * Time.deltaTime * movementSpeed * 2;
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
            HP -= 10;
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
            HP -= 10;
    }

    void shoot() { 
        GameObject tempObj;

        tempObj = Instantiate(rocketProjectile) as GameObject;
        tempObj.transform.position = transform.position + transform.forward;

        tempObj.transform.LookAt(target);

        Rigidbody projectile = tempObj.GetComponent<Rigidbody>();

        projectile.velocity = tempObj.transform.forward * 50;
    }

    public void set_target(Transform new_target)
    {

        target = new_target;
    }

    IEnumerator Dying()
    {
        alive = false;
        DisableColliders();
        PlayerStats.Instance.Score += 170;
        explosion.Play();
        if (!slicedCopy)
            blastSound.Play();
        yield return null;



        rb.useGravity = true;
        while (explosion.isPlaying)
        {
            rb.AddForceAtPosition(ExplodeAt.right * 5, ExplodeAt.position);
            yield return null;
        }
        if (spawner != null)
        {
            spawner.currEnemy = null;
        }
        rb.isKinematic = true;
        mesh.enabled = false;
        explosion.Play();
        if (!slicedCopy)
            blastSound.Play();

        while (explosion.isPlaying)
        {
            yield return null;
        }

        Destroy(rb.gameObject);

    }

    public bool get_alive()
    {
        return (alive);
    }
}
