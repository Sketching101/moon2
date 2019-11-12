using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ManualControls;

public class MechController : MonoBehaviour
{
    [Header("Manual Controls")]
    [SerializeField]
    private Joystick RotateJoystick;
    [SerializeField]
    private Throttle VelocityThrottle;

    [Header("Ship Objects")]
    [SerializeField]
    private Transform MechTransform;

    [Header("Stats")]
    [SerializeField]
    private float speed;
    

    // Use this for initialization
    void Awake()
    {
    }

    void Update()
    {
        MechTransform.position += MechTransform.forward * Time.deltaTime * speed;    
    }

}
