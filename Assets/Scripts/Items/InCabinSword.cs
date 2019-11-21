using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InCabinSword : InCabinHeldItem
{
    private void Awake()
    {
        InCabinAwake();
    }

    public override void OnHoverEnter()
    {
        Transform moveTo = grabberHover[0].MechGrabberTr;
        heldItem.SetPosition(moveTo);
    }

    public override void OnHoverExit()
    {
        heldItem.Holster();
    }

    public override void OnGrab()
    {
        heldItem.OnGrab();
        heldItem.dissolveMaterial.ToggleItem();
    }

    public override void OnLetGo()
    {
        heldItem.dissolveMaterial.ToggleItem();
    }
}
