using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shootable : MonoBehaviour
{

    public GameObject Highlight;
    public Rigidbody rb;

    // Use this for initialization

    protected virtual void Awake()
    {
        if(rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }
    }

    protected virtual void Update()
    {

    }

    public virtual void ToggleHighlight(bool toggle)
    {
        if (Highlight.activeSelf != toggle)
            Highlight.SetActive(toggle);
    }

    public virtual void OnShot(GameObject other)
    {
        if (other.gameObject.tag == "bullet")
        {
            Destroy(other.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.tag == "bullet" || other.gameObject.tag == "enemy_bullet"))
        {
            OnShot(other.gameObject);
        }

        if (other.gameObject.tag == "Blade")
        {
            //Rigidbody rb = GetComponent<Rigidbody>();
            //if (rb != null)
            //{
            //    StartCoroutine(SlicedAftereffect(rb));
            //}
        }

        if (other.gameObject.tag == "Player")
        {
            PlayerStats.Instance.HP -= 75;
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if ((other.gameObject.tag == "bullet" || other.gameObject.tag == "enemy_bullet"))
        {
            OnShot(other.gameObject);
        }

        if (other.gameObject.tag == "Blade")
        {
            //Rigidbody rb = GetComponent<Rigidbody>();
            //if (rb != null)
            //{
            //    StartCoroutine(SlicedAftereffect(rb));
            //}
        }

        if (other.gameObject.tag == "Player")
        {
            PlayerStats.Instance.HP -= 75;
        }
    }

    public void Sliced(Vector3 dir)
    {
        StartCoroutine(SlicedAftereffect(dir));
    }

    IEnumerator SlicedAftereffect(Vector3 dir)
    {
        yield return new WaitForSeconds(3);
        rb.AddForce(dir*100000);
        yield return null;
    }
}
