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
        if (OVRInput.Get(OVRInput.RawButton.Y))
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
                    tarRb.AddForce((transform.position - targetPos.position)*5);
                    Debug.Log("Pullll");
                }
            }

            if(targeted && (targetPos.position - transform.position).magnitude < 20)
            {
                if(handController.handState != HandState.Open)
                {
                    MechGripAnchor gripAnchor;
                    //if((gripAnchor = targetPos.GetComponent<)
                    //mechGrabber.ForceGrabObject(targeted)
                }
            }
        }
        else
        {
            targeted = false;
        }
    }

    private bool FindLightningGrab()
    {
        int layerMask = 1 << 8;

        Vector3 dir = aimForward.forward;

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.SphereCast(aimForward.position, 10, dir, out hit, 20000, layerMask))
        {
            targetPos = hit.transform;
            return true;
        } else
        {
            return false;
        }
    }
}
