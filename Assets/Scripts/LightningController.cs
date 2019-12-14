using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.LightningBolt;

[RequireComponent(typeof(LightningBoltScript))]
public class LightningController : MonoBehaviour
{
    [Header("Important Objects")]
    public MechGrabber mechGrabber;
    public HandController handController;
    public LightningBoltScript lightningBolt;

    [Header("Anchors")]
    public Transform aimForward;
    [SerializeField]
    private Transform targetPos;
    public float dmg = 10;

    public float chargeUntil = 20;
    public float charge = 0;

    private bool burnout = false;

    private Physics physics;

    bool targeted = false;

    private void Awake()
    {
        if (lightningBolt == null)
            lightningBolt = GetComponent<LightningBoltScript>();

        if (aimForward == null)
            aimForward = transform;
    }

    private void Update()
    {
        if(charge > chargeUntil)
        {
            burnout = false;
        }

        if (OVRInput.Get(OVRInput.RawButton.Y) && !burnout)
        {
            if (targetPos == null)
            {
                targeted = false;
            }

            if (!targeted && FindLightningGrab())
            {
                lightningBolt.EndObject = targetPos.gameObject;
                lightningBolt.StartObject = aimForward.gameObject;
                lightningBolt.Trigger();
                targeted = true;
            }
            else if(targeted)
            {     
                lightningBolt.Trigger();
                Rigidbody tarRb;
                if ((tarRb = targetPos.GetComponent<Rigidbody>()) != null)
                {
                    tarRb.AddForce((transform.position - targetPos.position));
                    if (tarRb.tag == "Enemy") {
                        Enemy enemy = tarRb.GetComponent<Enemy>();
                        PlayerStats.Instance.AddHP(dmg * Time.deltaTime);
                        enemy.ChangeHP(-1 * dmg * Time.deltaTime);
                        charge -= Time.deltaTime * 10;
                    }
                }
            }
        }
        else
        {
            if(charge <= chargeUntil)
            {
                charge += Time.deltaTime;
            }
            targeted = false;
        }
    }

    private bool FindLightningGrab()
    {
        int layerMask = 1 << 8;

        Vector3 dir = aimForward.forward;

        RaycastHit[] hits;
        // Does the ray intersect any objects excluding the player layer
        hits = physics.ConeCastAll(aimForward.position, 100, dir, 1000, 30, layerMask, "Enemy");
        if (hits.Length != 0)
        {
            targetPos = hits[0].transform;
            return true;
        } else
        {
            return false;
        }
    }
}
