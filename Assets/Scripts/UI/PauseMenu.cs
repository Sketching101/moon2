using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public static bool isPaused = false;
    public GameObject pauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Debug.Log("PauseMenu.cs: Toggling Pause Menu");
            if (isPaused)
            {
                Resume();

            }
            else
            {
                Pause();
            }
        }
        if (isPaused && OVRInput.GetDown(OVRInput.RawButton.A))
        {
            Resume();
            SceneManager.LoadScene(0);
        }
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        isPaused = false;
        //Debug.Log("PauseMenu.cs: Resumed Game");
    }

    void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        isPaused = true;
        //Debug.Log("PauseMenu.cs: Paused Game");
    }

    public void LoadStartMenu()
    {
        Debug.Log("PauseMenu.cs: Loading Start Menu");
    }
}
