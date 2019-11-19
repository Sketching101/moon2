using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class LaunchHand : MonoBehaviour
{
    [Header("Mech Hand")]
    public HandController handController;
    public Transform handTr;
    public Transform handSocket;
    public MechGrabber handGrabber;

    [Header("Debug")]
    public bool launched = false;
    public bool canInteract = false;
    public float maxTime = 3.0f;
    public float speed = 50.0f;

    private Vector3 SavedLocalPos;
    private Quaternion SavedLocalRot;

    float dist;
    float time_t = 0;
    

    void Update()
    {
        time_t += Time.deltaTime;
    }

    public void LaunchArm()
    {
        StartCoroutine(LaunchArmThread());
    }

    public void LaunchArm(Transform Target)
    {
        StartCoroutine(LaunchArmThread(Target));
    }

    /// <summary>
    /// Launch for n-seconds, before the arm retracts
    /// </summary>
    /// <param name="Target">The transform the hand targets</param>
    /// <returns></returns>
    public IEnumerator LaunchArmThread(Transform Target)
    {
        SavedLocalPos = handTr.localPosition;
        SavedLocalRot = handTr.localRotation;
        time_t = 0;
        launched = true;
        canInteract = true;
        yield return null;

        while (time_t < maxTime)
        {
            handTr.position += (Target.position - handTr.position).normalized * speed * Time.deltaTime;
            if(Target == null)
            {
                break;
            }
            yield return null;
            time_t += Time.deltaTime;
        }

        canInteract = false;
        time_t = 0;

        while(time_t < maxTime)
        {
            handTr.position += (handSocket.position - handTr.position).normalized * speed * Time.deltaTime;
            yield return null;
            time_t += Time.deltaTime;
        }

        handTr.localPosition = SavedLocalPos;
        handTr.localRotation = SavedLocalRot;

        handController.armState = ArmState.Attached;
        launched = false;
    }

    /// <summary>
    /// Launch for n-seconds, before the arm retracts
    /// </summary>
    /// <returns></returns>
    public IEnumerator LaunchArmThread()
    {
        SavedLocalPos = handTr.localPosition;
        SavedLocalRot = handTr.localRotation;

        launched = true;
        canInteract = true;

        time_t = 0;
        float last_time = 0;
        float deltaTime;
        yield return null;

        while (time_t < maxTime)
        {
            deltaTime = time_t - last_time;
            handTr.position += handTr.forward * speed * deltaTime;
            last_time = time_t;
            yield return null;
        }

        Debug.Log("Finished launching, please come back ffs");
        canInteract = false;
        time_t = 0;
        last_time = 0;
        dist = (handSocket.position - handTr.position).magnitude;
        yield return null;

        while (time_t < maxTime && dist > 5)
        {
            dist = (handSocket.position - handTr.position).magnitude;
            deltaTime = time_t - last_time;
            handTr.position += (handSocket.position - handTr.position).normalized * speed * deltaTime * 5;
            last_time = time_t;
            yield return null;
        }

        handTr.localPosition = SavedLocalPos;
        handTr.localRotation = SavedLocalRot;

        handController.armState = ArmState.Attached;    

        OVRInput.SetControllerVibration(1f, 1.5f, handController.controller);

        yield return new WaitForSeconds(.2f);

        OVRInput.SetControllerVibration(0, 0, handController.controller);



        launched = false;
    }

}
