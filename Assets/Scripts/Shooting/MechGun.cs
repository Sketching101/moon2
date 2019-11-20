using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechGun : HeldItem
{
    [Header("Anchors")]
    public Transform GunBarrel;

    [Header("Prefabs")]
    public GameObject Laser;

    [Header("Stats")]
    public float damage = 5;

    public override void PrimaryAction()
    {
        GameObject bullet = Instantiate(Laser, GunBarrel.position, GunBarrel.rotation);
        bullet.GetComponent<Rigidbody>().velocity = GunBarrel.forward * damage * 50;
    }
}
