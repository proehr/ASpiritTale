using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCombo : MonoBehaviour
{
    public bool active;

    private byte pos;

    //true = x
    //false = y
    private bool[] x_y_map =
    {
        true, false, true, true, false, true, true, false
    };
    // Start is called before the first frame update
    void Start()
    {
        pos = 0;
        active = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (UIEnvironment.paused)
        {
            return;
        }
        if (!active)
        {
            if (AnyKey())
            {
                if (x_y_map[pos])
                {
                    if (OVRInput.Get(OVRInput.Button.Three))
                    {
                        pos++;
                        checkForLastInput();
                    }
                    else
                    {
                        pos = 0;
                    }
                }
                else
                {
                    if (OVRInput.Get(OVRInput.Button.Four))
                    {
                        pos++;
                        checkForLastInput();
                    }
                    else
                    {
                        pos = 0;
                    }
                }
            }
        }
    }

    bool AnyKey()
    {
        return OVRInput.Get(OVRInput.Button.Any);
    }

    void checkForLastInput()
    {
        if ((pos + 1) == x_y_map.Length)
        {
            active = true;
            transformAllObjects();
            Debug.Log("FunMode is Active");
        }
    }

    void transformObject(GameObject obj)
    {
        if (obj.GetComponent<MeshCollider>())
        {
            obj.GetComponent<MeshCollider>().convex = true;
        }
        obj.AddComponent<BoxCollider>();
        obj.AddComponent<Rigidbody>();
        obj.AddComponent<ObjectHandler>();
    }

    void transformAllObjects()
    {
        GameObject[] fun_objs;
        fun_objs = GameObject.FindGameObjectsWithTag("Fun_throwable");
        foreach (GameObject fun_obj in fun_objs)
        {
            transformObject(fun_obj);
        }
    }
}
