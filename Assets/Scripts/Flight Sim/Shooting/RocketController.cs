using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : MonoBehaviour {

    public Vector3 TargetPosition;
    float time_t = 0;
    float LifeTime = 10;
    public Rigidbody rb;

	// Use this for initialization
	void Start () {
        if (rb == null)
            rb = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
        time_t += Time.deltaTime;
        if (time_t > LifeTime)
            Destroy(gameObject);
        if(TargetPosition == Vector3.zero)
        {
            return;
        }

        transform.LookAt(TargetPosition);
        rb.velocity = 100 * transform.forward;
	}
}
