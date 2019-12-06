using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformShip : MonoBehaviour
{
    public Transform body;
    public Transform shipMode;
    public Transform mechMode;
    public float rotationSpeed;
    private IEnumerator activeCoroutine;

    public void TurnIntoShip()
    {
        if (activeCoroutine != null)
        {
            StopCoroutine(activeCoroutine);
            activeCoroutine = null;
        }

        Debug.Log("turn into ship");
        activeCoroutine = TurnIntoShipCoroutine();
        StartCoroutine(activeCoroutine);
    }

    public void TurnIntoMech()
    {
        if (activeCoroutine != null)
        {
            StopCoroutine(activeCoroutine);
            activeCoroutine = null;
        }

        activeCoroutine = TurnIntoMechCoroutine();
        StartCoroutine(activeCoroutine);
    }

    IEnumerator TurnIntoShipCoroutine()
    {
        while (body.localRotation != shipMode.localRotation && body.localPosition != shipMode.localPosition)
        {
            if (body.localRotation != shipMode.localRotation)
                body.localRotation = Quaternion.Slerp(body.localRotation, shipMode.localRotation, rotationSpeed * Time.deltaTime);
            if (body.localPosition != shipMode.localPosition)
                body.localPosition = Vector3.Lerp(body.localPosition, shipMode.localPosition, rotationSpeed * Time.deltaTime);
            yield return null;
        }
        activeCoroutine = null;
        yield return null;
    }

    IEnumerator TurnIntoMechCoroutine()
    {
        yield return null;
        while (body.localRotation != mechMode.localRotation && body.localPosition != mechMode.localPosition)
        {
            if (body.localRotation != mechMode.localRotation)
                body.localRotation = Quaternion.Slerp(body.localRotation, mechMode.localRotation, rotationSpeed * Time.deltaTime);
            if(body.localPosition != mechMode.localPosition)
                body.localPosition = Vector3.Lerp(body.localPosition, mechMode.localPosition, rotationSpeed * Time.deltaTime);
            yield return null;
        }
        activeCoroutine = null;
        yield return null;
    }
}
