using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerController : MonoBehaviour
{
    [Header("Joints")]
    public Transform[] JointPos;

    [Header("Stats")]
    public float rotationSpeed;

    public bool RotateThis;
    public int joint;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A) && RotateThis)
        {
            for (int i = 0; i < JointPos.Length; i++)
            {
                if(i == joint || joint == -1)
                    JointPos[i].localRotation = Quaternion.Slerp(JointPos[i].localRotation, Quaternion.Euler(0f, 0f, 90f), rotationSpeed * Time.deltaTime);
            }
        } else
        {
            for (int i = 0; i < JointPos.Length; i++)
            {
                JointPos[i].localRotation = Quaternion.Slerp(JointPos[i].localRotation, Quaternion.Euler(0f, 0f, 0f), rotationSpeed * Time.deltaTime);
            }
        }
    }
}
