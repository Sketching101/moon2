using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_drone_ai : MonoBehaviour
{
    public float angleBetween = 0.0f;
    public Transform target;

    public float movementSpeed = 20.0f;
    public float elapsed = 0.0f;
    public AudioClip blastSound;
    public ParticleSystem explosion;

    public bool alive;
    public float hp = 5.0f;
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
            if (hp <= 0)
            {
                StartCoroutine(Dying());
            }
            Vector3 targetDir = target.position - transform.position;


            if(Vector3.Distance(target.position, transform.position) <= 2.0f) {
                explode();
            }


            angleBetween = Vector3.Angle(transform.forward, targetDir);
            transform.LookAt(target);

            transform.position += transform.forward * Time.deltaTime * movementSpeed;

            elapsed += Time.deltaTime;

            if(elapsed >= 5.0f) {
                movementSpeed = 40.0f;
            }

        }

    }

    private void OnTriggerEnter(Collider other)
    {
         hp -= 10;
        //if other = player, deal damage
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag != "enemy_bullet" && other.gameObject.tag != "Enemy")
            hp -= 10;
    }

    private void explode() {
        hp -= 20;
        //player.hp -= 20
    }

    IEnumerator Dying()
    {
        alive = false;
        AudioSource.PlayClipAtPoint(blastSound, target.position);
        explosion.Play();
        yield return null;
        GetComponent<MeshRenderer>().enabled = false;
        while (explosion.isPlaying)
        {
            Debug.Log("Dead but exploding");
            yield return null;
        }

        Destroy(gameObject);

    }
}
