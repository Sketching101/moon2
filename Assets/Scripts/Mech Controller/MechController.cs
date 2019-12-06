using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ManualControls;

public class MechController : MonoBehaviour
{
    private enum Mode
    {
        Ship, Mech, None
    }

    [Header("Manual Controls")]
    [SerializeField]
    private Joystick RotateJoystick;
    [SerializeField]
    private Throttle VelocityThrottle;
    [SerializeField]
    private Throttle ModeThrottle;

    [Header("Mech Objects")]
    [SerializeField]
    private Transform MechTransform;

    [Header("Stats")]
    [SerializeField]
    private float speed;

    [Header("Ship State")]
    [SerializeField]
    private Mode CurrentMode;

    private string CurrentModeSt
    {
        get
        {
            if(CurrentMode == Mode.Mech)
            {
                return "Mech";
            }
            else if(CurrentMode == Mode.Ship)
            {
                return "Ship";
            }
            else
            {
                return "None";
            }
        }
    }

    string ship = "Ship";
    string mech = "Mech";
    string none = "None";

    [Header("Ship Objects")]
    public TransformShip ShipTransformer;

    [Header("Mobility Stats")]
    [SerializeField]
    public float MinVelocity;
    [SerializeField]
    public float Axelaration;
    [SerializeField]
    public float MaxVelocity;

    bool Collided = false;
    
    [Header("Debug Values")]
    [SerializeField]
    private float time_t = 0;
    [SerializeField]
    float roll = 0.0f;
    [SerializeField]
    float pitch = 0.0f;
    [SerializeField]
    float yaw = 0.0f;


    float velocity_t = 0;
    float acceleration_t = 0;

    public float velocity_display = 0;

    // Use this for initialization
    void Awake()
    {
        velocity_t = MinVelocity;
        if(CurrentMode == Mode.Mech)
        {
            TurnIntoMech();
        } else
        {
            TurnIntoShip();
        }
    }


    void Update()
    {
        if (InputController.Instance.PauseDown)
        {
            Debug.Log("Pause Pressed");
        }

        if (IsMode(ship))
        {
            ShipUpdate();

        }
        else if (IsMode(mech))
        {
            MechUpdate();
        }
    }

    void FixedUpdate()
    {
        if (IsMode(ship))
        {
            ShipFixedUpdate();
        }
        else if (IsMode(mech))
        {
            MechFixedUpdate();
        }
    }

    private bool IsMode(string st)
    {
        return st == CurrentModeSt;
    }

    private void MechUpdate()
    {
        if (ModeThrottle.GetThrottleOut() == -1f)
        {
            TurnIntoShip();
        }
    }

    private void MechFixedUpdate()
    {

    }

    public void TurnIntoShip()
    {
        CurrentMode = Mode.Ship;
        ShipTransformer.TurnIntoShip();
        if (DissolvingHand.Instance != null)
            DissolvingHand.Instance.ToggleHands(false);
    }

    public void TurnIntoMech()
    {
        CurrentMode = Mode.Mech;
        ShipTransformer.TurnIntoMech();
        if(DissolvingHand.Instance != null)
            DissolvingHand.Instance.ToggleHands(true);
    }

    ///////////////////////// SHIP CONTROLLER ///////////////////////////
    private void ShipUpdate()
    {
        Vector3 JoystickInput = RotateJoystick.GetJoystickOut();
        float ThrottleInput = VelocityThrottle.GetThrottleOut();

        pitch = JoystickInput.y;
        roll = JoystickInput.x;
        yaw = RotateJoystick.GetYawOut() * 5;

        acceleration_t = Axelaration * ThrottleInput;


        if (velocity_t <= MinVelocity)
            velocity_t = MinVelocity;
        else if (velocity_t > MaxVelocity)
            velocity_t = MaxVelocity;

        velocity_display = velocity_t;

        if (ModeThrottle.GetThrottleOut() == 1f)
        {
            TurnIntoMech();
        }
    }

    private void ShipFixedUpdate()
    {
        velocity_t += acceleration_t * Time.fixedDeltaTime;

        MechTransform.position += velocity_t * MechTransform.forward * Time.fixedDeltaTime;

        MechTransform.Rotate(MechTransform.right, pitch / 10, Space.World);
        MechTransform.Rotate(MechTransform.forward, roll * -1 / 10, Space.World);
        MechTransform.Rotate(MechTransform.up, yaw / 10, Space.World);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "CrashCollision")
            MenuSelect.Instance.LoseGame();
    }

    void OnTriggerExit(Collider other)
    {
        Collided = false;
    }
}
