using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aiming : MonoBehaviour {

    public Transform TargetReticle;
    public Transform PlayerEye;

    LayerMask layerMask;

	public Vector3 FireDirectionSecondary(Transform Source, out Vector3 TargetPos)
    {
        int layerMask = 1 << 10;

        Vector3 dir = (TargetReticle.position - PlayerEye.position);

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.SphereCast(Source.position, 4, dir, out hit, 4000, layerMask) && hit.transform.GetComponent<Terrain>() == null)
        {
            TargetPos = hit.point;
            return (hit.transform.position - Source.position).normalized;
        } else
        {
            TargetPos = new Vector3();
            return Source.forward;
        }
    }

    public Quaternion FireRotationSecondary(Vector3 dir)
    {
        return Quaternion.LookRotation(dir, Vector3.up);
    }

    public Vector3 FireDirectionPrimary(Transform Source)
    {
        int layerMask = 1 << 10;

        Vector3 dir = (TargetReticle.position - PlayerEye.position);

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.SphereCast(Source.position, 4, dir, out hit, 4000, layerMask) && hit.transform.GetComponent<Terrain>() == null)
        {
            return (hit.transform.position - Source.position).normalized;
        }
        else
        {
            return Source.forward;
        }
    }

    public Quaternion FireRotationPrimary(Vector3 dir)
    {
        return Quaternion.LookRotation(dir, Vector3.up);
    }
}
