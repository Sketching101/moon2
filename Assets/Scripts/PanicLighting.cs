using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanicLighting : MonoBehaviour
{
    public AnimationCurve panicCurve;
    public Color panicColor;

    public Light[] panicLights;
    private float time_t = 0;
    public float panicAtHP;
    private bool enablePanic;
    private bool disablePanic;

    private void Awake()
    {
        time_t = 0;
        for (int i = 0; i < panicLights.Length; i++)
        {
            panicLights[i].color = panicColor;
            panicLights[i].intensity = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(time_t >= 1.3f)
        {
            time_t = 0;
        }

        float val = panicCurve.Evaluate(time_t);


        if (panicAtHP > PlayerStats.Instance.HP)
        {
            time_t += Time.deltaTime;
            for (int i = 0; i < panicLights.Length; i++)
            {
                panicLights[i].intensity = val;
            }
        }

        if(enablePanic || disablePanic)
        {

            enablePanic = false;
            disablePanic = false;
            time_t = 0;
        }
    }
}
