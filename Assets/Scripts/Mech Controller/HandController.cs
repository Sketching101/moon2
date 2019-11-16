using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ManualControls;
/*
public class HandControllerDep : MonoBehaviour
{
    [Header("Fingers")]
    public FingerController Thumb;
    public FingerController IndexFinger;
    public FingerController MiddleFinger;
    public FingerController RingFinger;
    public FingerController Pinky;


    [Header("Level Based Settings")]
    public bool StartWHands;

    [Header("Misc")]
    public HandState handState;

    bool coroutine = false;

    bool HandsExist;

    // Start is called before the first frame update
    void Start()
    {
        HandsExist = true;
    }

    void Update()
    {
        if(Thumb.closeFistFlag && IndexFinger.closeFistFlag && MiddleFinger.closeFistFlag)
        {
            handState = HandState.Fist;
        } else if(Thumb.openPalmFlag && IndexFinger.openPalmFlag && MiddleFinger.closeFistFlag)
        {
            handState = HandState.FingerGun;
        } else if(Thumb.closeFistFlag && IndexFinger.openPalmFlag && MiddleFinger.closeFistFlag)
        {
            if(handState == HandState.FingerGun)
            {

            }
            handState = HandState.Point;
        } else if(Thumb.closeFistFlag && IndexFinger.closeFistFlag && MiddleFinger.openPalmFlag)
        {
            handState = HandState.OK;
        } else if(Thumb.openPalmFlag && IndexFinger.closeFistFlag && MiddleFinger.closeFistFlag)
        {
            handState = HandState.ThumbsUp;
        } else
        {
            handState = HandState.Open;
        }
    }

    // NOT WORKING //
    /*
    public bool ToggleHands()
    {
        if (!coroutine)
        {
            coroutine = true;
            HandsExist = !HandsExist;
            StartCoroutine(ToggleDissolution());
            return true;
        }
        else
        {
            return false;
        }
    }

    private IEnumerator ToggleDissolution()
    {
        foreach (Material mat in Materials)
        {
            mat.SetFloat("_Trigger_Time", Time.timeSinceLevelLoad);
            mat.SetFloat("_Playing_Flag", 1);
            yield return null;
        }

        yield return new WaitForSeconds(1f);

        float active = 0f;
        if (HandsExist)
        {
            active = 1f;
        }

        foreach (Material mat in Materials)
        {
            mat.SetFloat("_Arms_Active", active);
            mat.SetFloat("_Playing_Flag", 0);
            yield return null;
        }

        coroutine = false;
    }


    private IEnumerator DissolveMeshes()
    {

        foreach (Material mat in Materials)
        {
            mat.SetFloat("_Trigger_Time", Time.timeSinceLevelLoad);
            mat.SetFloat("_Playing_Flag", 1);
            yield return null;
        }

        yield return new WaitForSeconds(1f);

        foreach (Material mat in Materials)
        {
            mat.SetFloat("_Arms_Active", 0);
            mat.SetFloat("_Playing_Flag", 0);
            yield return null;
        }

        coroutine = false;
    }

    private IEnumerator CreateMeshes()
    {
        foreach (Material mat in Materials)
        {
            mat.SetFloat("_Trigger_Time", Time.timeSinceLevelLoad);
            mat.SetFloat("_Playing_Flag", 1);
            yield return null;
        }

        yield return new WaitForSeconds(1f);

        foreach (Material mat in Materials)
        {
            mat.SetFloat("_Arms_Active", 1);
            mat.SetFloat("_Playing_Flag", 0);
            yield return null;
        }

        coroutine = false;
    }*/
//}
