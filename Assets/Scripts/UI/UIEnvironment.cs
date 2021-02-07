using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIEnvironment : MonoBehaviour
{
    [SerializeField] private GameObject playerController = null;
    [SerializeField] private GameObject centerEyeAnchor = null;
    [SerializeField] private GameObject customLeftHand = null;
    [SerializeField] private GameObject customRightHand = null;

    [SerializeField] private Canvas pauseCanvas = null;
    [SerializeField] private Canvas victoryCanvas = null;
    [SerializeField] private Canvas gameOverCanvas = null;

    public static bool paused = false;
    public static bool dead = false;
    public static bool victory = false;
    public static bool victoryDone = false;

    private void Start()
    {
        ResetStateValues();
    }

    private void ResetStateValues()
    {
        paused = false;
        dead = false;
        victory = false;
        victoryDone = false;
        Time.timeScale = 1;
    }

    void Update()
    {
        if (dead && !paused)
        {
            GoToUiEnvironment();
            gameOverCanvas.gameObject.SetActive(true);
        }
        else if (victory && !paused)
        {
            GoToUiEnvironment();
            victoryCanvas.gameObject.SetActive(true);
            victory = false;
            victoryDone = true;
        }
        else if (OVRInput.GetUp(OVRInput.Button.Start))
        {
            if (paused)
            {
                ResumeGame();
            }
            else if (victoryDone)
            {
                GoToUiEnvironment();
                victoryCanvas.gameObject.SetActive(true);
            }
            else
            {
                GoToUiEnvironment();
                pauseCanvas.gameObject.SetActive(true);
            }
        }
    }

    private void GoToUiEnvironment()
    {
        centerEyeAnchor.GetComponent<AudioListener>().enabled = false;
        Time.timeScale = 0;
        paused = true;
        playerController.transform.position = new Vector3(2000, 0, 0);
        Transform ceeTransform = centerEyeAnchor.transform;
        Vector3 forward = ceeTransform.forward;
        float length = (float) Math.Sqrt(forward.x * forward.x + forward.z * forward.z);
        var position = ceeTransform.position;
        transform.GetChild(1).position =
            new Vector3(position.x + forward.x / length,
                position.y, position.z + forward.z / length);
        transform.GetChild(1).forward =
            new Vector3(forward.x, 0, forward.z);

        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }

        // deactivate light in second level
        if (centerEyeAnchor.transform.childCount > 0)
        {
            centerEyeAnchor.transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    public void ResumeGame()
    {
        centerEyeAnchor.GetComponent<AudioListener>().enabled = true;
        Time.timeScale = 1;
        paused = false;
        playerController.transform.position = Vector3.zero;
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }

        pauseCanvas.gameObject.SetActive(false);
        victoryCanvas.gameObject.SetActive(false);

        // reactivate light in second level
        if (centerEyeAnchor.transform.childCount > 0)
        {
            centerEyeAnchor.transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    public void GoToMainMenu()
    {
        centerEyeAnchor.GetComponent<AudioListener>().enabled = true;
        for (int i = 1; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }

        customLeftHand.SetActive(false);
        customRightHand.SetActive(false);

        StartCoroutine(LoadYourAsyncScene(0));
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }

    public void NextScene()
    {
        centerEyeAnchor.GetComponent<AudioListener>().enabled = true;
        if (!paused)
        {
            playerController.transform.position = new Vector3(2000, 0, 0);
            transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            for (int i = 1; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }

        customLeftHand.SetActive(false);
        customRightHand.SetActive(false);
        int sceneNumber = SceneManager.GetActiveScene().buildIndex + 1;
        if (sceneNumber < SceneManager.sceneCountInBuildSettings)
        {
            StartCoroutine(LoadYourAsyncScene(sceneNumber));
        }
        else
        {
            StartCoroutine(LoadYourAsyncScene(0));
        }
    }

    // taken from https://docs.unity3d.com/ScriptReference/SceneManagement.SceneManager.LoadSceneAsync.html
    IEnumerator LoadYourAsyncScene(int sceneInt)
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneInt);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        ResetStateValues();
    }

    public void RetryLevel()
    {
        centerEyeAnchor.GetComponent<AudioListener>().enabled = true;
        for (int i = 1; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }

        customLeftHand.SetActive(false);
        customRightHand.SetActive(false);

        StartCoroutine(LoadYourAsyncScene(SceneManager.GetActiveScene().buildIndex));
    }
}