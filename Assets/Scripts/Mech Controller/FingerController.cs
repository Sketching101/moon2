using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerControllerDep : MonoBehaviour
{
    [Header("Joints")]
    public Transform[] JointPos;

    [Header("Stats")]
    public float rotationSpeed;

    [Header("Associated Touch")]
    public OVRInput.RawTouch Touch;
    public OVRInput.RawButton Button;

    public bool closeFistFlag;
    public bool openPalmFlag;

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.Get(Touch) || OVRInput.Get(Button))
        {
            CloseFist();
        }
        else
            OpenPalm();

        // Close/Open Fist
        if (closeFistFlag)
        {
            for (int i = 0; i < JointPos.Length; i++)
            {
                 JointPos[i].localRotation = Quaternion.Slerp(JointPos[i].localRotation, Quaternion.Euler(0f, 0f, 90f), rotationSpeed * Time.deltaTime);
            }
        } else if(openPalmFlag)
        {
            for (int i = 0; i < JointPos.Length; i++)
            {
                JointPos[i].localRotation = Quaternion.Slerp(JointPos[i].localRotation, Quaternion.Euler(0f, 0f, 0f), rotationSpeed * Time.deltaTime);
            }
        }
    }

    public void CloseFist()
    {
        openPalmFlag = false;
        closeFistFlag = true;
    }

    public void OpenPalm()
    {
        openPalmFlag = true;
        closeFistFlag = false;
    }

    public void FreezeHand()
    {
        openPalmFlag = false;
        closeFistFlag = false;
    }
}
