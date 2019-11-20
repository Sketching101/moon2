using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HeldItem : MonoBehaviour
{
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
}
