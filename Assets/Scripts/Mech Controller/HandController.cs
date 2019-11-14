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

    [Header("Materials")]
    public Renderer[] Materials;

    bool coroutine = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void Update()
    {
        /* Broken 
        if(Input.GetKeyDown(KeyCode.D) && !coroutine)
        {
            coroutine = true;
            StartCoroutine(DissolveMeshes());
        } else if(Input.GetKeyDown(KeyCode.S) && !coroutine)
        {
            coroutine = true;
            StartCoroutine(CreateMeshes());
        }*/
    }

    private IEnumerator DissolveMeshes()
    {
        float dissolve = 0;
        while (dissolve < 1)
        {
            dissolve = Mathf.Lerp(dissolve, 1, .1f);
            foreach (Renderer mat in Materials)
            {
                mat.sharedMaterial.SetFloat("_Dissolve", dissolve);
                yield return null;
            }
        }
        yield return null;
    }

    private IEnumerator CreateMeshes()
    {
        float dissolve = 1;
        while (dissolve > 0)
        {
            dissolve = Mathf.Lerp(dissolve, 0, .1f);
            foreach (Renderer mat in Materials)
            {
                mat.sharedMaterial.SetFloat("_Dissolve", dissolve);
                yield return null;
            }
        }
        yield return null;
        coroutine = false;
    }
}
