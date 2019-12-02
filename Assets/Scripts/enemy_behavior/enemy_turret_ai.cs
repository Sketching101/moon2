using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_turret_ai : MonoBehaviour
{
    public float angleBetween = 0.0f;
    public Transform target;

    public GameObject model;

    public GameObject rocketProjectile;
    public float elapsed = 0.0f;
    public AudioClip blastSound;
    public ParticleSystem explosion;

    public bool alive;
    public float hp = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        elapsed = Time.deltaTime;
        //  hp = 10.0f;
        alive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (alive)
        {
            //hp -= 0.01f;
            if (hp <= 0)
            {
                StartCoroutine(Dying());
            }
            //Vector3 targetDir = target.position - transform.position;
            //angleBetween = Vector3.Angle(transform.forward, targetDir);
            //transform.LookAt(target);


            elapsed += Time.deltaTime;

            if (elapsed > 2.0)
            {
                shoot(elapsed);
                elapsed = 0.0f;
                //need offset for shot
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag != "enemy_bullet" && other.gameObject.tag != "Enemy") 
            hp -= 10;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag != "enemy_bullet" && other.gameObject.tag != "Enemy")
            hp -= 10;
    }

    void shoot(float time)
    {
        GameObject tempObj;
        
        tempObj = Instantiate(rocketProjectile) as GameObject;

        //Set position  of the bullet in front of the player
        tempObj.transform.position = transform.position + transform.forward;

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
        alive = false;
        AudioSource.PlayClipAtPoint(blastSound, target.position);
        explosion.Play();
        yield return null;

        GetComponent<Rigidbody>().AddForce(transform.forward * -1000);


        while (explosion.isPlaying)
        {
            Debug.Log("Dead but exploding");
            yield return null;
        }

        GetComponent<Rigidbody>().isKinematic = true;
        Destroy(model);
        explosion.Play();
        while (explosion.isPlaying)
        {
            Debug.Log("Dead but exploding");
            yield return null;
        }

        Destroy(gameObject);

    }
}
