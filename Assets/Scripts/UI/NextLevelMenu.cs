using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelMenu : MonoBehaviour
{

    public static bool isPaused = false;
    public GameObject nextLevelMenu;
    public static NextLevelMenu Instance { get; private set; }

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        nextLevelMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isPaused && OVRInput.GetDown(OVRInput.RawButton.A))
        {
            Resume();

            if (SceneManager.sceneCountInBuildSettings > SceneManager.sceneCount + 1)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else
            {
                SceneManager.LoadScene(0);
            }
        }
    }

    public void Resume()
    {
        nextLevelMenu.SetActive(false);
        Time.timeScale = 1;
        isPaused = false;
        //Debug.Log("PauseMenu.cs: Resumed Game");
    }

    public void Pause()
    {
        nextLevelMenu.SetActive(true);
        Time.timeScale = 0;
        isPaused = true;
        //Debug.Log("PauseMenu.cs: Paused Game");
    }

    public void LoadStartMenu()
    {
        Debug.Log("PauseMenu.cs: Loading Start Menu");
    }
}
