using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSphere : MonoBehaviour {

    public bool visible = true;
    public float invis_timer;
    public ParticleSystem explosionVFX;
    public GameObject Highlight;
    public GameObject RealObj;

    AudioSource explosionSFX;

	// Use this for initialization
	void Awake () {
        explosionSFX = gameObject.GetComponent<AudioSource>();
        explosionVFX = GetComponentInChildren<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!visible)
        {
           invis_timer += Time.deltaTime;
            if (invis_timer >= 5.0f) {
                visible = true;

                RealObj.SetActive(true);
                gameObject.GetComponent<Collider>().enabled = true;
            }
        }
    }

    public void ToggleHighlight(bool toggle)
    {
        if(Highlight.activeSelf != toggle)
            Highlight.SetActive(toggle);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("COLLISION");
        if ((other.gameObject.tag == "bullet" || other.gameObject.tag == "Blade") && visible) {
            if (other.gameObject.tag == "bullet")
            {
                Destroy(other.gameObject);
            }
            visible = false;
            RealObj.SetActive(false);
            gameObject.GetComponent<Collider>().enabled = false;
            invis_timer = 0.0f;
            explosionSFX.Play();
            explosionVFX.Play();
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("COLLISION");
        if ((other.gameObject.tag == "bullet" || other.gameObject.tag == "Blade") && visible)
        {
            if (other.gameObject.tag == "bullet")
            {
                Destroy(other.gameObject);
            }
            visible = false;
            RealObj.SetActive(false);
            gameObject.GetComponent<Collider>().enabled = false;
            invis_timer = 0.0f;
            explosionSFX.Play();
            explosionVFX.Play();
        }
    }
}
