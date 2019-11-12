using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBlastController : MonoBehaviour {

    float time_t = 0;
    float LifeTime = 5;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        time_t += Time.deltaTime;
        if (time_t > LifeTime)
            Destroy(gameObject);
    }
}
