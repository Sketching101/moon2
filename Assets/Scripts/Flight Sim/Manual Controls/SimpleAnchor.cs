using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleAnchor : MonoBehaviour
{
    public Transform Anchor;
    public bool setRotation = false;

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Anchor.position;
        if(setRotation)
        {
            transform.rotation = Anchor.rotation;
        }
    }
}
