using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayParticleEffectOnce : MonoBehaviour
{
    public ParticleSystem[] particleSystems;

    public void PlayParticleSys()
    {
        foreach(ParticleSystem ps in particleSystems)
        {
            ps.Play();
        }
    }
}
