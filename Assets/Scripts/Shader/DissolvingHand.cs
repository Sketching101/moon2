using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolvingHand : MonoBehaviour
{
    public Renderer[] rends;
    
    public Transform parentObject;

    public static DissolvingHand Instance { get; private set; } // static singleton

    public MeshRenderer[] Renderers;
    public Collider[] Colliders;

    private List<Collider> colList;
    private List<MeshRenderer> meshList;

    public bool StartWHand = true;

    bool dissolveWithThis = true;

    public bool HandsExist;

    // Start is called before the first frame update
    void Awake()
    {
        PopulateArrays();

        if (StartWHand)
        {
            HandsExist = true;
            StartCoroutine(Generate());

            SetMeshesAndColliders();
        } else
        {
            HandsExist = false;
            ClearMeshesAndColliders();
        }


        if (Instance == null) {
            Instance = this;
        }
        else {
            dissolveWithThis = false;
            Debug.Log("Already Exists");
        }

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

    public void PopulateArrays()
    {
        if (colList == null || meshList == null)
        {
            colList = new List<Collider>();
            meshList = new List<MeshRenderer>();
        }
        GetMeshesAndColliders(parentObject);
        Colliders = new Collider[colList.Count];
        Renderers = new MeshRenderer[meshList.Count];

        for(int i = 0; i < colList.Count; i++)
        {
            Colliders[i] = colList[i];
        }

        for(int i = 0; i < meshList.Count; i++)
        {
            Renderers[i] = meshList[i];
        }
    }

    public void ClearMeshesAndColliders() 
    {
        if (dissolveWithThis)
        {
            foreach (Renderer rend in rends)
            {
                rend.sharedMaterial.SetFloat("_DissolveAmount", 1);
            }
        }

        int mLen = Renderers.Length;
        int cLen = Colliders.Length;
        for (int i = 0; i < mLen; i++)
        {
            Renderers[i].enabled = false;
        }

        for (int i = 0; i < cLen; i++)
        {
            Colliders[i].enabled = false;
        }
    }

    public void SetMeshesAndColliders()
    {

        if (dissolveWithThis)
        {
            foreach (Renderer rend in rends)
            {
                rend.sharedMaterial.SetFloat("_DissolveAmount", 0);
            }
        }

        int mLen = Renderers.Length;
        int cLen = Colliders.Length;
        for (int i = 0; i < mLen; i++)
        {
            Renderers[i].enabled = true;
        }

        for (int i = 0; i < cLen; i++)
        {
            Colliders[i].enabled = true;
        }
    }


    private void GetMeshesAndColliders(Transform currentObj)
    {
        if(currentObj == null)
        {
            return;
        }
        int childCount = currentObj.childCount;

        for(int i = 0; i < childCount; i++)
        {
            GetMeshesAndColliders(currentObj.GetChild(i));
        }

        if(currentObj.gameObject.GetComponent<MeshRenderer>() != null)
        {
            foreach(MeshRenderer renderer in currentObj.gameObject.GetComponents<MeshRenderer>())
                meshList.Add(renderer);
        } 

        if(currentObj.gameObject.GetComponent<Collider>() != null)
        {
            foreach (Collider col in currentObj.gameObject.GetComponents<Collider>())
                colList.Add(col);
        }
    }

    public void ToggleHands(bool create)
    {
        if(create)
        {
            StartCoroutine(Generate());
        } else
        {
            StartCoroutine(Dissolve());
        }
    }

    public IEnumerator Generate()
    {

        Debug.Log("Generating");
        HandsExist = true;

        float dissolve = 1;
        int mLen = Renderers.Length;
        int cLen = Colliders.Length;

        for (int i = 0; i < mLen; i++)
        {
            Renderers[i].enabled = true;
            if(i % 5 == 0)
            {
                yield return null;
            }
        }

        for (int i = 0; i < cLen; i++)
        {
            Colliders[i].enabled = true;
            if (i % 5 == 0)
            {
                yield return null;
            }
        }

        yield return null;
        while (dissolve > 0)
        {
            if (dissolveWithThis)
            {
                dissolve = Mathf.Lerp(dissolve, -0.3f, .02f);
                foreach (Renderer rend in rends)
                {
                    rend.sharedMaterial.SetFloat("_DissolveAmount", dissolve);
                }
            }
            yield return null;
        }
        Debug.Log("Generated");
    }

    public IEnumerator Dissolve()
    {
        Debug.Log("Dissolving");
        float dissolve = 0;
        yield return null;
        while (dissolve < 1)
        {
            if (dissolveWithThis)
            {
                dissolve = Mathf.Lerp(dissolve, 1.3f, .02f);
                foreach (Renderer rend in rends)
                {
                    rend.sharedMaterial.SetFloat("_DissolveAmount", dissolve);
                }
            }
            yield return null;
        }
        int mLen = Renderers.Length;
        int cLen = Colliders.Length;
        for (int i = 0; i < mLen; i++)
        {
            Renderers[i].enabled = false;
            if (i % 5 == 0)
            {
                yield return null;
            }
        }

        for (int i = 0; i < cLen; i++)
        {
            Colliders[i].enabled = false;
            if (i % 5 == 0)
            {
                yield return null;
            }
        }
        Debug.Log("Dissolved");
        ArmController.Instance.HideHands();
        HandsExist = false;
    }
}
