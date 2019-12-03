using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartAction : MonoBehaviour
{

    void Start()
    {
        Image img = GameObject.Find("StartMenu").GetComponent<Image>();
        img.color = UnityEngine.Color.red;
        Debug.Break();

        GameObject.Find("StartButton").GetComponent<Button>().onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {

    }
}
