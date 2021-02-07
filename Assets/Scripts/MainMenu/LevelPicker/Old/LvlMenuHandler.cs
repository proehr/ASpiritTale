using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class LvlMenuHandler : MonoBehaviour
{
    public float menuSpeed;
    public int currLvl;
    void Start()
    {
        currLvl = 0;
        InvokeRepeating("checkforInput", menuSpeed, menuSpeed);
        changeLvlSelection(currLvl);
    }
    void checkforInput()
    {
        if (!(getDirection() == 0))
        {
            int nextLvl;
            if (getDirection() == -1)
            {
                nextLvl = currLvl - 1;
                if (nextLvl < 0)
                {
                    nextLvl = transform.childCount - 1;
                }
            }
            else
            {
                nextLvl = currLvl + 1;
                if (nextLvl >= transform.childCount)
                {
                    nextLvl = 0;
                }
            }
            changeLvlSelection(nextLvl);
        }
    }

    float getDirection()
    {
        float A2dPTx = math.round(OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).x);
        return A2dPTx;
    }

    void changeLvlSelection(int nextLvl)
    {
        
        transform.GetChild(currLvl).GetComponent<LvlPickerHandler>().isChoosen = false;
        transform.GetChild(currLvl).GetComponent<LvlPickerHandler>().changeMat();
        currLvl = nextLvl;
        Debug.Log(currLvl);
        transform.GetChild(currLvl).GetComponent<LvlPickerHandler>().isChoosen = true;
        transform.GetChild(currLvl).GetComponent<LvlPickerHandler>().changeMat();
    }
}
