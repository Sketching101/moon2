using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechHandgun : MonoBehaviour
{
    [Header("Mech Hand")]
    public Transform Fingertip;
    public HandController handController;

    [Header("Prefabs")]
    public GameObject projectilePrefab;

    [Header("Audio Source")]
    public int gunAudio = 7;

    int projectile_count = 0;

    CollisionSphere lastCol = null;

    // Update is called once per frame
    void Update()
    {
        if(!DissolvingHand.Instance.HandsExist)
        {
            return;
        }

        int layerMask = 1 << 8;

        Vector3 dir = Fingertip.right;

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(Fingertip.position, dir, out hit, 4000, layerMask) && hit.transform.GetComponent<Terrain>() == null)
        {
            CollisionSphere col = hit.transform.gameObject.GetComponent<CollisionSphere>();
            if (col != null)
            {
                col.ToggleHighlight(true);
            }
            if (lastCol != col && lastCol != null)
            {
                lastCol.ToggleHighlight(false);
            }
            lastCol = col;
        }
        else if (lastCol != null)
        {
            lastCol.ToggleHighlight(false);
        }
    }

    public void FireGun()
    {
        Vector3 dir = Fingertip.right;
        Quaternion rot = Fingertip.rotation;
        GameObject clone = Instantiate(projectilePrefab, Fingertip.position, rot) as GameObject;

        clone.GetComponent<Rigidbody>().velocity = dir * 200;
        SFXManager.Instance.PlayClip(gunAudio);
        projectile_count++;
    }

    public void DetectTargets()
    {

    }

    private Vector3 FireDirectionPrimary(Transform Target)
    {
        return new Vector3();
    }
    
    private Quaternion FireRotationPrimary(Vector3 dir)
    {
        return new Quaternion();
    }
}
