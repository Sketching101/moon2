using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private float time_t = 0;
    

    void Update()
    {
        time_t += Time.deltaTime;    
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
        }

        canInteract = false;
        time_t = 0;

        while((handSocket.position - handSocket.position).magnitude > speed)
        {
            handTr.position += (handSocket.position - handTr.position).normalized * speed * Time.deltaTime;
            yield return null;
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

        time_t = 0;
        launched = true;
        canInteract = true;
        yield return null;

        while (time_t < maxTime)
        {
            handTr.position += handTr.forward * speed * Time.deltaTime;
            yield return null;
        }

        canInteract = false;
        time_t = 0;

        while ((handSocket.position - handSocket.position).magnitude > speed)
        {
            handTr.position += (handSocket.position - handTr.position).normalized * speed * Time.deltaTime;
            yield return null;
        }

        handTr.localPosition = SavedLocalPos;
        handTr.localRotation = SavedLocalRot;
        handController.armState = ArmState.Attached;
        launched = false;
    }

}
