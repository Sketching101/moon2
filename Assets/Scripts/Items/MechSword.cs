using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BzKovSoft.ObjectSlicerSamples;

public class MechSword : HeldItem
{
    [Header("Anchors")]
    public Transform SwordTip;
    public TrailRenderer trailRenderer;
    [Header("Slicer")]
    public BzKnife _blade;

    public override void PrimaryAction()
    {
        _blade.BeginNewSlice();
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
