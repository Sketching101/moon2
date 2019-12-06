using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmController : MonoBehaviour
{
    public static ArmController Instance { get; private set; }

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
    public Transform CenterEye;
    public Transform MechObjects;
    public Transform HideMechObjects;


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

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        ResetHands();
    }

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
                LeftHand.position = (LeftAnchor.position - CenterEye.position) * distMul + CenterEye.position;
            } else if(LeftArm == ArmState.Launched)
            {
                LeftAnchor.rotation = LeftReal.rotation;

                LeftLaunchHand.transform.rotation = LeftAnchor.rotation;
            }

            if (RightArm == ArmState.Attached)
            {
                RightAnchor.position = RightReal.position;
                RightAnchor.rotation = RightReal.rotation;

                RightHand.localRotation = RightAnchor.localRotation;
                RightHand.position = (RightAnchor.position - CenterEye.position) * distMul + CenterEye.position;
            }
            else if (RightArm == ArmState.Launched)
            {
                RightAnchor.rotation = RightReal.rotation;

                RightLaunchHand.transform.rotation = RightAnchor.rotation;
            }
            
        }
        else
        {
            /*
            LeftHand.localRotation = RestLeft.localRotation;
            LeftHand.localPosition = RestLeft.localPosition;
            RightHand.localRotation = RestRight.localRotation;
            RightHand.localPosition = RestRight.localPosition;
            */
        }
    }

    public void ResetHands()
    {
        MechObjects.localPosition = new Vector3();
        MechObjects.localRotation = new Quaternion();
    }

    public void HideHands()
    {
        MechObjects.position = HideMechObjects.position;
    }
}

public enum ArmState
{
    Attached, Launched
}