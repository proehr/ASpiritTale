using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LvlPickerController : MonoBehaviour
{
    public int _sceneNr;
    public int _progressReq;
    public bool isChoosen;
    public bool isBlocked;

    public Sprite[] sprites;
    // Start is called before the first frame update
    void Start()
    {
        isChoosen = false;

        if (_progressReq > getProgressNr())
        {
            isBlocked = true;
        }
        changeMat();
    }
    public void changeMat()
    {
        if (isBlocked && isChoosen)
        {
            gameObject.GetComponent<Image>().sprite = sprites[3];
        }
        else if (isBlocked)
        {
            gameObject.GetComponent<Image>().sprite = sprites[2];
        }
        else if(isChoosen)
        {
            gameObject.GetComponent<Image>().sprite = sprites[1];
        }
        else
        {
            gameObject.GetComponent<Image>().sprite = sprites[0];
        }
    }
    int getProgressNr()
    {
        return GameObject.Find("Progress").GetComponent<Progress>()._data.ProgressNr;
    }

    public void chooseThis()
    {
        gameObject.transform.parent.GetComponent<LvlMenuController>().changeLvlSelection(gameObject);
    }
}
