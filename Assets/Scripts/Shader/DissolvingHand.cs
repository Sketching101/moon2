using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolvingHand : MonoBehaviour
{
    Renderer rend;

    // Start is called before the first frame update
    void Start()
    {
       
        rend = GetComponent<Renderer>();

    }

    // Update is called once per frame
    void Update()
    {
        if(OVRInput.GetDown(OVRInput.RawButton.A))
        {
            StartCoroutine(Dissolve());
        } else if(OVRInput.GetDown(OVRInput.RawButton.B))
        {
            StartCoroutine(Generate());
        }
    }

    public IEnumerator Generate()
    {
        float dissolve = 1;
        yield return null;
        while (dissolve > 0)
        {
            dissolve = Mathf.Lerp(dissolve, -0.3f, .02f);
            rend.sharedMaterial.SetFloat("_DissolveAmount", dissolve);
            yield return null;
        }
    }

    public IEnumerator Dissolve()
    {
        float dissolve = 0;
        yield return null;
        while (dissolve < 1)
        {
            dissolve = Mathf.Lerp(dissolve, 1.3f, .02f);
            rend.sharedMaterial.SetFloat("_DissolveAmount", dissolve);
            yield return null;
        }

    }
}
