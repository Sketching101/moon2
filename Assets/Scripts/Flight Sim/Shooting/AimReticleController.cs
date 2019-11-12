using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ManualControls;

public class AimReticleController : MonoBehaviour {
    public Transform AimReticle;
    public Transform AimReticleMaxX;
    public Transform AimReticleMinX;

    public Throttle throttle;
    
	
	// Update is called once per frame
	void Update () {
        Vector3 pos = AimReticle.localPosition;
        Vector3 throttleOut = throttle.GetReticleOut();
		if(throttleOut.x > .5) {
            pos.x += Time.deltaTime;
        } else if (throttleOut.x < -.5)
        {
            pos.x -= Time.deltaTime;
        }

        if (throttleOut.y > .5)
        {
            pos.y += Time.deltaTime / 2;
        }
        else if (throttleOut.y < -.5)
        {
            pos.y -= Time.deltaTime / 2;
        }

        if (pos.x < AimReticleMinX.localPosition.x)
        {
            pos.x = AimReticleMinX.localPosition.x;
        } else if(pos.x > AimReticleMaxX.localPosition.x)
        {
            pos.x = AimReticleMaxX.localPosition.x;
        }

        if (pos.y < AimReticleMinX.localPosition.y)
        {
            pos.y = AimReticleMinX.localPosition.y;
        }
        else if (pos.y > AimReticleMaxX.localPosition.y)
        {
            pos.y = AimReticleMaxX.localPosition.y;
        }

        AimReticle.localPosition = pos;
	}
}
