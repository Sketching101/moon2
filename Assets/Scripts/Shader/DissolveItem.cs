using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveItem : MonoBehaviour
{
    public Renderer rendererWithMat;

    enum DissolveState
    {
        Dissolved, Generated, Dissolving, Generating
    }

    private IEnumerator runningCoroutine;

    [SerializeField]
    private DissolveState dissolveItem;

    private void Awake()
    {
        if(rendererWithMat.material.GetFloat("_DissolveAmount") <= 0)
        {
            dissolveItem = DissolveState.Generated;
        } else
        {
            dissolveItem = DissolveState.Dissolved;
        }
    }

    public void ToggleItem()
    {

        if(dissolveItem == DissolveState.Generated || dissolveItem == DissolveState.Generating)
        {
            if(dissolveItem == DissolveState.Generating)
            {
                StopCoroutine(runningCoroutine);
            }
            Dissolve();
        } else 
        {
            if (dissolveItem == DissolveState.Dissolving)
            {
                StopCoroutine(runningCoroutine);
            }
            Generate();
        }
    }

    public void Generate()
    {
        runningCoroutine = GenerateThread();
        StartCoroutine(runningCoroutine);
    }

    public void Dissolve()
    {
        runningCoroutine = DissolveThread();
        StartCoroutine(runningCoroutine);
    }

    public IEnumerator GenerateThread()
    {
        dissolveItem = DissolveState.Generating;
        float dissolve = rendererWithMat.material.GetFloat("_DissolveAmount");
        yield return null;
        while (dissolve > 0)
        {
            dissolve = Mathf.Lerp(dissolve, -0.3f, .02f);
            rendererWithMat.material.SetFloat("_DissolveAmount", dissolve);
            yield return null;
        }
        dissolveItem = DissolveState.Generated;
    }

    public IEnumerator DissolveThread()
    {
        dissolveItem = DissolveState.Dissolving;
        float dissolve = rendererWithMat.material.GetFloat("_DissolveAmount");
        yield return null;
        while (dissolve < 1)
        {
            dissolve = Mathf.Lerp(dissolve, 1.3f, .02f);
            rendererWithMat.material.SetFloat("_DissolveAmount", dissolve);
            yield return null;
        }
        dissolveItem = DissolveState.Dissolved;
        GetComponent<HeldItem>().Holster();
    }
}
