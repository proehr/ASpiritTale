using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LvlPickerHandler : MonoBehaviour
{
    public int _sceneNr;
    public int _progressReq;
    public bool isChoosen;
    public bool isBlocked;

    public Material[] mats;
    
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

    // Update is called once per frame
    public void changeMat()
    {
        if (isBlocked && isChoosen)
        {
            gameObject.GetComponent<MeshRenderer>().material = mats[3];
        }
        else if (isBlocked)
        {
            gameObject.GetComponent<MeshRenderer>().material = mats[2];
        }
        else if(isChoosen)
        {
            gameObject.GetComponent<MeshRenderer>().material = mats[1];
        }
        else
        {
            gameObject.GetComponent<MeshRenderer>().material = mats[0];
        }
    }

    int getProgressNr()
    {
        return GameObject.Find("Progress").GetComponent<Progress>()._data.ProgressNr;
    }
}
