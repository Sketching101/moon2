using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetView : MonoBehaviour
{
    bool ResettingView = false;

    // Start is called before the first frame update
    void Start()
    {
        OVRManager.display.RecenterPose();
    }

    // Update is called once per frame
    void Update()
    {
        if (InputController.Instance.ResetViewDown && !ResettingView)
        {
            ResettingView = true;
            StartCoroutine(ResetOrientation());
        }
    }

    IEnumerator ResetOrientation()
    {
        int i = 0;
        while (i < 10)
        {
            if (OVRInput.Get(OVRInput.RawButton.X, OVRInput.Controller.LTouch))
            {
                i++;
                yield return new WaitForSeconds(.1f);
                yield return null;
            }
            else
            {
                break;
            }
        }

        if (i >= 10)
            OVRManager.display.RecenterPose();

        ResettingView = false;

        yield return null;

    }
}
