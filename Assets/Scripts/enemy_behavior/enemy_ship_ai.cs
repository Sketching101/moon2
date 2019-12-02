using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_ship_ai : MonoBehaviour
{
    public float angleBetween = 0.0f;
    public Transform target;

    public GameObject rocketProjectile;
    public float movementSpeed = 15.0f;
    public float elapsed = 0.0f;
    public bool moving_right = true;
    public AudioClip blastSound;
    public ParticleSystem explosion;

    public bool alive;
    public float hp = 15.0f;
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

            if (elapsed > 7.5 && moving_right)
            {
                elapsed = 0.0f;
                moving_right = false;
                shoot();
            }

            if (elapsed > 7.5 && !moving_right)
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

        //Set position  of the bullet in front of the player
        tempObj.transform.position = transform.position + transform.forward;

        //Vector3 targetDir = target.position - transform.position;
        //angleBetween = Vector3.Angle(transform.forward, targetDir);
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

        while (explosion.isPlaying)
        {
            Debug.Log("Dead but exploding");
            yield return null;
        }

        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<MeshRenderer>().enabled = false;
        explosion.Play();
        while (explosion.isPlaying)
        {
            Debug.Log("Dead but exploding");
            yield return null;
        }

        Destroy(gameObject);

    }
}
