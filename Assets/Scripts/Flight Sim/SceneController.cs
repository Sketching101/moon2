using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {

    public static SceneController Instance { get; private set; } // static singleton

    bool ResettingView = false;

    // Use this for initialization
    void Awake () {

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
	
	// Update is called once per frame
	void Update () {
		if(OVRInput.GetDown(OVRInput.RawButton.X, OVRInput.Controller.LTouch) && !ResettingView)
        {
            ResettingView = true;
            StartCoroutine(ResetOrientation());
        }
    }

    public void ToMainMenu()
    {
        StartCoroutine(LoadNewScene("StartMenu"));
        //SceneManager.LoadScene("StartMenu");
    }

    public void StartGame()
    {
        StartCoroutine(LoadNewScene("GameMap"));
        //SceneManager.LoadScene("GameMap");
    }

    public void StartTutorial()
    {
        StartCoroutine(LoadNewScene("TutorialMap"));
        //SceneManager.LoadScene("TutorialMap");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    IEnumerator LoadNewScene(string sceneName)
    {

        // This line waits for 3 seconds before executing the next line in the coroutine.
        // This line is only necessary for this demo. The scenes are so simple that they load too fast to read the "Loading..." text.
        yield return new WaitForSeconds(3);

        // Start an asynchronous operation to load the scene that was passed to the LoadNewScene coroutine.
        AsyncOperation async = SceneManager.LoadSceneAsync(sceneName);

        // While the asynchronous operation to load the new scene is not yet complete, continue waiting until it's done.
        while (!async.isDone)
        {
            yield return null;
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
            } else
            {
                break;
            }
        }

        if(i >= 10)
            OVRManager.display.RecenterPose();

        ResettingView = false;

        yield return null;

    }
}
