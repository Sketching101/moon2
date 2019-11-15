using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmController : MonoBehaviour
{
    [Header("Robo Hands")]
    public Transform LeftHand;
    public Transform RightHand;

    [Header("Actual Hands")]
    public Transform LeftReal;
    public Transform RightReal;

    [Header("Anchors")]
    public Transform LeftAnchor;
    public Transform RightAnchor;

    [Header("Misc")]
    public float distMul;

    // Update is called once per frame
    void Update()
    {

        LeftAnchor.position = LeftReal.position;
        LeftAnchor.rotation = LeftReal.rotation;

        RightAnchor.position = RightReal.position;
        RightAnchor.rotation = RightReal.rotation;

        LeftHand.localRotation = LeftAnchor.localRotation;
        LeftHand.localPosition = LeftAnchor.localPosition * distMul;

        RightHand.localRotation = RightAnchor.localRotation;
        RightHand.localPosition = RightAnchor.localPosition * distMul;
    }
}
