using System;
using System.Collections;
using System.Collections.Generic;
using SymbolDrawing;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalBoss : MonoBehaviour
{
    [SerializeField] private GameObject head;

    public static bool symbolWasDestroyed = false;

    public bool isFinalBoss = false;
    // Start is called before the first frame update
    


    // Update is called once per frame
    public void showMask()
    {
        if (gameObject.name.Contains("Boxing"))
            head.transform.GetChild(0).GetComponent<HeadTargetGenerator>().isLastEnemy = true;
        head.transform.GetChild(1).gameObject.SetActive(true);
        isFinalBoss = true;
    }

    private void OnDestroy()
    {
        if (isFinalBoss)
        {
            if (symbolWasDestroyed)
            {
                UIEnvironment.victory = true;
                Progress progress = GameObject.Find("Progress").GetComponent<Progress>();
                if (progress != null && progress._data.ProgressNr <
                    SceneManager.GetActiveScene().buildIndex)
                {
                    progress._data.ProgressNr =
                        SceneManager.GetActiveScene().buildIndex;
                    progress.saveData();
                }

                symbolWasDestroyed = false;
            }
            else
            {
                UIEnvironment.dead = true;
            }
        }
}

}