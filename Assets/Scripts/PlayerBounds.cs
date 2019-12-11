using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBounds : MonoBehaviour
{
    public static PlayerBounds Instance { get; private set; } // static singleton

    // Surfaces across from each other are concurrent (0,1), (2,3), (4,5)
    public Transform[] Surfaces;
    public Transform PlayerObject;
    public bool inBounds = true;
    public float dot1;
    public float damage = 50f;
    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        bool inBoundsBuf = true;
        int len = Surfaces.Length;
        for(int i = 0; i < len; i += 2)
        {
            Vector3 dist1 = (PlayerObject.position - Surfaces[i].position);
            Vector3 dist2 = (PlayerObject.position - Surfaces[i + 1].position);
            Vector3 betweenPlanes = (Surfaces[i].position - Surfaces[i + 1].position);
            dot1 = Vector3.Dot(Vector3.Project(dist1, betweenPlanes), Vector3.Project(dist2, betweenPlanes));
            if(dot1 > 0)
            {
                inBoundsBuf = false;
                break;
            }
        }
        inBounds = inBoundsBuf;

        if(!inBounds)
        {
            PlayerStats.Instance.HP -= damage * Time.deltaTime;
        }
    }
}
