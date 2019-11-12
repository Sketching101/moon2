using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixRotation : MonoBehaviour
{
    public Transform positionAnchor;
    public Transform lookAtAnchor;

    void Update()
    {
        gameObject.transform.position = positionAnchor.position;
        gameObject.transform.LookAt(lookAtAnchor, transform.parent.up);
    }
}