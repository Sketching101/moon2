using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HeldItem : MonoBehaviour
{
    [Header("Held Item Components")]
    public DissolveItem dissolveMaterial;
    public Transform objectParent;

    [Header("Important Anchors")]
    public Transform holster;

    /// <summary>
    /// Primary action on index trigger pull
    /// </summary>
    public abstract void PrimaryAction();

    /// <summary>
    /// Primary action while index trigger is pulled
    /// </summary>
    public void PrimaryActionCont()
    {
        return;
    }

    public void SetPosition(Transform tr)
    {
        transform.SetPositionAndRotation(tr.position, tr.rotation);
        objectParent.localPosition = new Vector3();
        objectParent.localRotation = new Quaternion();
    }

    public virtual void Holster()
    {
        transform.position = holster.position;
    }

    public virtual void OnGrab()
    {

    }
}
