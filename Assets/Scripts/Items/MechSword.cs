using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechSword : HeldItem
{
    [Header("Anchors")]
    public Transform SwordTip;
    public TrailRenderer trailRenderer;


    public override void PrimaryAction()
    {

    }
    
    public override void Holster()
    {
        trailRenderer.emitting = false;
        base.Holster();
    }

    public override void OnGrab()
    {
        base.OnGrab();
        trailRenderer.emitting = true;
    }
}
