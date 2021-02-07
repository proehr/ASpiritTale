using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LvlMenuController : MonoBehaviour
{
    public GameObject currLvl;
    void Start()
    {
        currLvl = gameObject.transform.GetChild(0).gameObject;
    }
    
    public void changeLvlSelection(GameObject nextLvl)
    {
        currLvl.GetComponent<LvlPickerController>().isChoosen = false;
        currLvl.GetComponent<LvlPickerController>().changeMat();
        currLvl = nextLvl;
        currLvl.GetComponent<LvlPickerController>().isChoosen = true;
        currLvl.GetComponent<LvlPickerController>().changeMat();
        gameObject.transform.parent.parent.parent.GetComponent<MainMenuScript>().StartGame();
    }
}
