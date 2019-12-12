using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanicLighting : MonoBehaviour
{
    public AnimationCurve panicCurve;
    public Color panicColor;

    [Header("Panic Indicators")]
    public Light[] panicLights;
    public ParticleSystem[] smokeVFX;

    private float time_t = 0;
    public float panicAtHP;
    public float[] smokeAtHP;
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

        for(int j = 0; j < smokeAtHP.Length; j++)
        {
            if (smokeAtHP[j] > PlayerStats.Instance.HP)
            {
                if(!smokeVFX[j].isPlaying)
                    smokeVFX[j].Play();
            } else
            {
                if (smokeVFX[j].isPlaying)
                    smokeVFX[j].Stop();
            }
        }


        if (enablePanic || disablePanic)
        {
   

            enablePanic = false;
            disablePanic = false;
            time_t = 0;
        }
    }
}
