using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingRotation : MonoBehaviour
{
    public Transform positionAnchor;

    void Update()
    {
        gameObject.transform.position = positionAnchor.position;
        gameObject.transform.rotation = new Quaternion(positionAnchor.rotation.x, positionAnchor.rotation.y, 0, positionAnchor.rotation.w);
    }
}