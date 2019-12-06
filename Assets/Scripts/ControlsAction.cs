using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

public class ControlsAction : MonoBehaviour
{
    public GameObject ControlsCanvas;

    private void Update()
    {
        if (OVRInput.GetDown(OVRInput.RawButton.A))
        {
            LoadScene("enemy");
        } else if (OVRInput.GetDown(OVRInput.RawButton.B))
        {
            ControlsCanvas.SetActive(!ControlsCanvas.activeSelf);
        } else if(OVRInput.GetDown(OVRInput.RawButton.X) || OVRInput.GetDown(OVRInput.RawButton.Y))
        {
            QuitApplication();
        }
    }

    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void QuitApplication()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else        
        Application.Quit();

#endif
    }
}
