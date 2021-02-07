using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] private GameObject playerController = null;
    [SerializeField] private GameObject leftHandController = null;
    [SerializeField] private GameObject rightHandController = null;
    [SerializeField] private GameObject uiHelpers = null;
    
    
    [SerializeField] private GameObject darkEnvironmentPrefab = null;
    
    public void StartGame()
    {
        int currLvl = GameObject.Find("LvlButtons").GetComponent<LvlMenuController>().currLvl
            .GetComponent<LvlPickerController>()._sceneNr;
        if (GameObject.Find("LvlButtons").GetComponent<LvlMenuController>().currLvl.GetComponent<LvlPickerController>()
            .isBlocked)
        {
        }
        else
        {
            Instantiate(darkEnvironmentPrefab, new Vector3(2000, 0, 0), Quaternion.identity);
            playerController.transform.position = new Vector3(2000, 0, 0);
            leftHandController.SetActive(false);
            rightHandController.SetActive(false);
            uiHelpers.SetActive(false);
            StartCoroutine(LoadYourAsyncScene(currLvl));
        }
    }

    public void QuitGame()
    {
        Application.Quit();
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
    }
}