using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shootable : MonoBehaviour
{

    public GameObject Highlight;


    // Use this for initialization

    protected virtual void Awake()
    {

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
        if ((other.gameObject.tag == "bullet" || other.gameObject.tag == "Blade" || other.gameObject.tag == "enemy_bullet"))
        {
            OnShot(other.gameObject);
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if ((other.gameObject.tag == "bullet" || other.gameObject.tag == "Blade" || other.gameObject.tag == "enemy_bullet"))
        {
            OnShot(other.gameObject);
        }
    }
}
