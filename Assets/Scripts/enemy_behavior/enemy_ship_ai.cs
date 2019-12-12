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

    public float hp = 20.0f;
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
            if (hp <= 0)
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


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "enemy_bullet" && other.gameObject.tag != "Enemy")
            hp -= 10;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag != "enemy_bullet" && other.gameObject.tag != "Enemy")
            hp -= 10;
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
        PlayerStats.Instance.Score += 170;
        explosion.Play();
        blastSound.Play();
        yield return null;

        GetComponent<Rigidbody>().useGravity = true;
        while (explosion.isPlaying)
        {
            GetComponent<Rigidbody>().AddForceAtPosition(ExplodeAt.right * 5, ExplodeAt.position);
            yield return null;
        }

        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<MeshRenderer>().enabled = false;
        explosion.Play();
        blastSound.Play();

        while (explosion.isPlaying)
        {
            yield return null;
        }

        Destroy(gameObject);

    }

    public bool get_alive()
    {
        return (alive);
    }
}
