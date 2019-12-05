using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSphere : Shootable {

    public ParticleSystem explosionVFX;
    protected AudioSource explosionSFX;

    public GameObject RealObj;
    public bool visible = true;
    public float invis_timer;

    protected override void Awake()
    {
        base.Awake();
        explosionSFX = gameObject.GetComponent<AudioSource>();
        explosionVFX = GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    protected override void Update () {
        base.Update();
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

    public override void OnShot(GameObject other)
    {
        if (visible)
        {
            base.OnShot(other);
            visible = false;
            RealObj.SetActive(false);
            gameObject.GetComponent<Collider>().enabled = false;
            invis_timer = 0.0f;
            explosionSFX.Play();
            explosionVFX.Play();
        }
    }
}
