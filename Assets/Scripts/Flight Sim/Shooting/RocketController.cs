using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : MonoBehaviour {

    public Transform TargetPosition;
    float time_t = 0;
    float LifeTime = 10;
    public Rigidbody rb;

    public ParticleSystem explosion;
    public AudioSource blastSound;
    public GameObject model;

    public bool alive;

	// Use this for initialization
	void Start () {
        alive = true;
        if (rb == null)
            rb = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
        if (!alive)
            return;
        time_t += Time.deltaTime;
        if (time_t > LifeTime)
            Destroy(gameObject);
        if(TargetPosition == null)
        {
            return;
        }

        transform.LookAt(TargetPosition);
        rb.velocity = 100 * transform.forward;
	}

    private void OnTriggerEnter(Collider collision)
    {
        if(!alive)
        {
            return;
        }
        if (blastSound != null && explosion != null && model != null && collision.gameObject != gameObject
            && (collision.gameObject.tag == "bullet" || collision.gameObject.tag == "Blade" || collision.gameObject.tag == "enemy_bullet"))
        {
            StartCoroutine(Dying());
        }
        if (collision.gameObject.tag == "Player")
        {
            PlayerStats.Instance.HP -= 10;
            StartCoroutine(Dying());
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(!alive)
        {
            return;
        }

        if (collision.gameObject.tag == "Player")
        {
            PlayerStats.Instance.HP -= 10;
            StartCoroutine(Dying());
        }
    }


    IEnumerator Dying()
    {
        alive = false;

        rb.velocity = new Vector3(0, 0, 0);
        blastSound.Play();
        explosion.Play();
        yield return null;
        Destroy(model);

        while (explosion.isPlaying)
        {
            Debug.Log("Dead but exploding");
            yield return null;
        }
        

        Destroy(gameObject);

    }
}
