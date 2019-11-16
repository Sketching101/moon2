using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmController : MonoBehaviour
{
    [Header("Robo Hands")]
    public Transform LeftHand;
    public Transform RightHand;

    public LaunchHand LeftLaunchHand;
    public LaunchHand RightLaunchHand;

    [Header("Actual Hands")]
    public Transform LeftReal;
    public Transform RightReal;

    [Header("Anchors")]
    public Transform LeftAnchor;
    public Transform RightAnchor;

    
    private ArmState RightArm
    {
        get
        {
            return RightLaunchHand.handController.armState;
        }
    }

    private ArmState LeftArm
    {
        get
        {
            return LeftLaunchHand.handController.armState;
        }
    }

    [Header("Misc")]
    public float distMul;

    [Header("Holsters")]
    public Transform RestLeft;
    public Transform RestRight;

    // Update is called once per frame
    void Update()
    {
        if (DissolvingHand.Instance.HandsExist)
        {
            if (LeftArm == ArmState.Attached)
            {
                LeftAnchor.position = LeftReal.position;
                LeftAnchor.rotation = LeftReal.rotation;

                LeftHand.localRotation = LeftAnchor.localRotation;
                LeftHand.localPosition = LeftAnchor.localPosition * distMul;
            } else if(LeftArm == ArmState.Launched)
            {
                LeftAnchor.rotation = LeftReal.rotation;

                LeftLaunchHand.transform.localRotation = LeftAnchor.localRotation;
            }

            if (RightArm == ArmState.Attached)
            {
                RightAnchor.position = RightReal.position;
                RightAnchor.rotation = RightReal.rotation;

                RightHand.transform.localRotation = RightAnchor.localRotation;
                RightHand.transform.localPosition = RightAnchor.localPosition * distMul;
            }
            else if (RightArm == ArmState.Launched)
            {
                RightAnchor.rotation = RightReal.rotation;

                RightLaunchHand.transform.localRotation = RightAnchor.localRotation;
            }
        }
        else
        {
            LeftHand.localRotation = RestLeft.localRotation;
            LeftHand.localPosition = RestLeft.localPosition * distMul;
            RightHand.localRotation = RestRight.localRotation;
            RightHand.localPosition = RestRight.localPosition * distMul;
        }


    }

    /// <summary>
    /// Launches left fist if possible
    /// </summary>
    /// <returns>If failed to launch left fist, return false. Otherwise, true.</returns>
    public bool LaunchLeft(Transform Target)
    {
        if(LeftArm == ArmState.Launched || LeftArm == ArmState.HoldingItem)
        {
            return false;
        } else
        {
            StartCoroutine(LeftLaunchHand.LaunchArmThread(Target));
            return true;
        }
    }

    /// <summary>
    /// Launches right fist if possible
    /// </summary>
    /// <returns>If failed to launch right fist, return false. Otherwise, true.</returns>
    public bool LaunchRight(Transform Target)
    {
        if (RightArm == ArmState.Launched || RightArm == ArmState.HoldingItem)
        {
            return false;
        }
        else
        {
            StartCoroutine(RightLaunchHand.LaunchArmThread(Target));
            return true;
        }
    }


}

public enum ArmState
{
    Attached, Launched, HoldingItem
}