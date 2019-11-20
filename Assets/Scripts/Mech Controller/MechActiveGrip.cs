using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechActiveGrip : MonoBehaviour
{
    public MechGripAnchor activeGrip;

    public void SetActiveGrip(MechGripAnchor grip)
    {
        grip.transform.SetParent(transform);
        activeGrip = grip;
    }

    public void ClearActiveGrip(Transform gripParent)
    {
        activeGrip.transform.SetParent(gripParent);
        activeGrip = null;
    }
}
