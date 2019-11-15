using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    [Header("Fingers")]
    public FingerController Thumb;
    public FingerController IndexFinger;
    public FingerController MiddleFinger;
    public FingerController RingFinger;
    public FingerController Pinky;

    [Header("Associate Touch Values")]

    [Header("Materials")]
    public Material[] Materials;


    [Header("Level Based Settings")]
    public bool StartWHands;

    bool coroutine = false;

    bool HandsExist;

    // Start is called before the first frame update
    void Start()
    {
        /*
        HandsExist = true;
        float active = HandsExist ? 1 : 0;
        foreach (Material mat in Materials)
        {
            mat.SetFloat("_Arms_Active", active);
        } */
    }

    void Update()
    {

    }

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
    } */
}