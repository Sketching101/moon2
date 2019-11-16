using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum HandState
{
    Fist, Open, FingerGun, ThumbsUp, Point, OK
}

public class HandController : MonoBehaviour
{
    [Header("Fingers")]
    public FingerController Thumb;
    public FingerController IndexFinger;
    public FingerController MiddleFinger;
    public FingerController RingFinger;
    public FingerController Pinky;

    public LaunchHand launchHand;

    public ArmState armState;

    [Header("Hand Objects")]
    public MechHandgun handGun;
    public OVRInput.Controller controller;

    [Header("Misc")]
    public HandState handState;

    [Header("Level Based Settings")]
    public bool StartWHands;

    bool coroutine = false;

    void Update()
    {
        if (!DissolvingHand.Instance.HandsExist)
            return;

        if (Thumb.closeFistFlag && IndexFinger.closeFistFlag && MiddleFinger.closeFistFlag)
        {
            handState = HandState.Fist;
        }
        else if (Thumb.openPalmFlag && IndexFinger.openPalmFlag && MiddleFinger.closeFistFlag)
        {
            handState = HandState.FingerGun;
        }
        else if (Thumb.closeFistFlag && IndexFinger.openPalmFlag && MiddleFinger.closeFistFlag)
        {
            if (handGun != null && handState == HandState.FingerGun)
            {
                handGun.FireGun();
            }
            handState = HandState.Point;
        }
        else if (Thumb.closeFistFlag && IndexFinger.closeFistFlag && MiddleFinger.openPalmFlag)
        {
            handState = HandState.OK;
        }
        else if (Thumb.openPalmFlag && IndexFinger.closeFistFlag && MiddleFinger.closeFistFlag)
        {
            handState = HandState.ThumbsUp;
        }
        else
        {
            handState = HandState.Open;
        }

        if (OVRInput.GetDown(OVRInput.Button.PrimaryThumbstick, controller) && HandState.Fist == handState)
        {
            FireHand();
        }
    }

    private void FireHand()
    {
        int layerMask = 1 << 8;

        Vector3 dir = launchHand.transform.right;

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(launchHand.transform.position, dir, out hit, 4000, layerMask) && hit.transform.GetComponent<Terrain>() == null)
        {
            CollisionSphere col = hit.transform.gameObject.GetComponent<CollisionSphere>();
            if (col != null)
            {
                armState = ArmState.Launched;
                StartCoroutine(launchHand.LaunchArmThread(col.transform));
            } else
            {
                armState = ArmState.Launched;
                StartCoroutine(launchHand.LaunchArmThread());
            }
        } else
        {
            armState = ArmState.Launched;
            StartCoroutine(launchHand.LaunchArmThread());
        }

    }
}