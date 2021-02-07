using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowObjects : MonoBehaviour
{
    //if false, it disable this whole functions
    [SerializeField] private bool active;
    //speed we use to rotate
    [SerializeField] private float rotationSpeed;
    //speed we use to drag the obj
    [SerializeField] private float dragSpeed;
    //the Force which we use to throw
    [SerializeField] private float throwForce;
    [SerializeField] private GameObject controllerR;

    [SerializeField] private float innerCircleRadius;
    [SerializeField] private float outerCircleRadius;
    
    [SerializeField] private GameObject FunMode;
    private GameObject _obj;
    //3 Lists. pos 0 has highest prio; pos 2 hast lowest prio
    private List<Ray>[] raycasts = new List<Ray>[3];
    // Update is called once per frame
    private void Start()
    {
        //initialize lists
        for (byte i = 0; i < raycasts.Length; i++)
        {
            raycasts[i] = new List<Ray>();
        }
    }

    void Update()
    {
        if (UIEnvironment.paused)
        {
            return;
        }
        if (active)
        {
            /*Debug    
                drawLine(controllerR.transform.position,controllerR.transform.position + controllerR.transform.forward);
                drawLine(controllerR.transform.position,controllerR.transform.position + controllerR.transform.forward + controllerR.transform.up * outerCircleRadius);
                drawLine(controllerR.transform.position,controllerR.transform.position + controllerR.transform.forward - controllerR.transform.up * outerCircleRadius);
                drawLine(controllerR.transform.position,controllerR.transform.position + controllerR.transform.forward + controllerR.transform.right * outerCircleRadius);
                drawLine(controllerR.transform.position,controllerR.transform.position + controllerR.transform.forward - controllerR.transform.right * outerCircleRadius);
                drawLine(controllerR.transform.position,controllerR.transform.position + controllerR.transform.forward + 0.5f * outerCircleRadius * (controllerR.transform.right + controllerR.transform.up));
                drawLine(controllerR.transform.position,controllerR.transform.position + controllerR.transform.forward + 0.5f * outerCircleRadius * (controllerR.transform.right - controllerR.transform.up));
                drawLine(controllerR.transform.position,controllerR.transform.position + controllerR.transform.forward - 0.5f * outerCircleRadius * (controllerR.transform.right + controllerR.transform.up));
                drawLine(controllerR.transform.position,controllerR.transform.position + controllerR.transform.forward - 0.5f * outerCircleRadius * (controllerR.transform.right - controllerR.transform.up));
            */
            //If we press A
            if (OVRInput.Get(OVRInput.Button.One))
            {
                if(_obj is null)
                    _obj = getObject();
                else
                {
                    assignStateAction(_obj.GetComponent<ObjectHandler>().state);
                }
            }
            else
            {
                if (!(_obj is null))
                {
                    //if we hold it and dont press A
                    if (_obj.GetComponent<ObjectHandler>().state.Equals("hold"))
                    {
                        //throw it
                        DoThrow();
                    }
                    //else if we still drag it and stop pressing A
                    else if(_obj.GetComponent<ObjectHandler>().state.Equals("drag"))
                    {
                        _obj.GetComponent<ObjectHandler>().changeState("free");
                    }
                    _obj = null;
                }
            }
        }
    }
    /**
    * Rotates a given object with the Primary Thumstick with the rotationSpeed
    *
    * @param {GameObject} The object we want to rotate.
    */
    void rotateObject(GameObject obj, float speed)
    {
        //pT = PrimaryThumbstick Vector3
        Vector3 pT = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
        obj.transform.Rotate( (new Vector3(pT.y,0,-pT.x) * (speed * Time.deltaTime)));
    }
    GameObject getObject()
    {
        
        RaycastHit hit;
        updateRays();
        for (byte i = 0; i < raycasts.Length; i++)
        {
            foreach(Ray ray in raycasts[i])
            {
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.gameObject.CompareTag("throwable"))
                    {
                        return hit.collider.gameObject;
                    }
                    /*if (checkforFunMode() && hit.collider.gameObject.CompareTag("Fun_throwable"))
                    {
                        return hit.collider.gameObject;
                    }*/
                }
            }
        }
        return null;
    }
    /**
    * Returns a Vector3 where we want to drag our objects dependent of the position of the right controller.
    * @return {Vector3} the Position where we want to drag our objcets
    */
    Vector3 GETDragPosPlayer()
    {
        return controllerR.transform.forward * 0.2f + controllerR.transform.position;
    }
    /**
    * Drag a obj to a point 
    *
    * @param {GameObject} obj The object we want to drag.
    * @param {Vector3} point The object we want to drag.
    * @param {float} speed the speed we want to drag.
    */
    void DragtoPoint(GameObject obj, Vector3 point, float speed)
    {
        obj.transform.position = Vector3.MoveTowards(obj.transform.position, point, speed);
    }
    /**
    * Handles what to do if we are in certain states
    *
    * @param {GameObject} obj The object we want to drag.
    */
    void assignStateAction(string state)
    {
        switch (state)
        {
            case "free":
                //if the obj is free we drag it to us
                _obj.GetComponent<ObjectHandler>().changeState("drag");
                break;
            case "drag":
                //if we reach the final drag position position, we change to gold
                if (Vector3.Distance(_obj.transform.position, GETDragPosPlayer()) < 0.1f)
                {
                    _obj.GetComponent<ObjectHandler>().changeState("hold");
                }
                DragtoPoint(_obj,GETDragPosPlayer(), dragSpeed);
                break;
            case "hold":
                //holds the obj and wait for release
                rotateObject(_obj, rotationSpeed);
                _obj.transform.position = GETDragPosPlayer();
                break;
        }
    }
    
    //throws the object
    void DoThrow()
    {
        _obj.GetComponent<ObjectHandler>().changeState("throw");
        _obj.GetComponent<Rigidbody>().velocity = controllerR.transform.forward * throwForce;
    }
    void drawLine(Vector3 p1, Vector3 p2)
    {
        GameObject myLine = new GameObject();
        myLine.AddComponent<LineRenderer>();
        LineRenderer lr; lr = myLine.GetComponent<LineRenderer>();
        lr.SetPosition(0, p1);
        lr.SetPosition(1, p2);
        lr.SetWidth(0.005f, 0.005f);
        GameObject.Destroy(myLine,0.018f);
    }

    void updateRays()
    {
        raycasts[0].Add(new Ray(controllerR.transform.position, controllerR.transform.forward));
        for (byte i = 1; i <= 2; i++)
        {
            float circle = (i == 1) ? innerCircleRadius : outerCircleRadius;

            raycasts[i].Add(new Ray(controllerR.transform.position, controllerR.transform.forward + controllerR.transform.up * circle));
            raycasts[i].Add(new Ray(controllerR.transform.position, controllerR.transform.forward - controllerR.transform.up * circle));
            raycasts[i].Add(new Ray(controllerR.transform.position, controllerR.transform.forward + controllerR.transform.right * circle));
            raycasts[i].Add(new Ray(controllerR.transform.position, controllerR.transform.forward - controllerR.transform.right * circle));
            raycasts[i].Add(new Ray(controllerR.transform.position, controllerR.transform.forward + 0.5f * circle * (controllerR.transform.up + controllerR.transform.right)));
            raycasts[i].Add(new Ray(controllerR.transform.position, controllerR.transform.forward - 0.5f * circle * (controllerR.transform.up + controllerR.transform.right)));
            raycasts[i].Add(new Ray(controllerR.transform.position, controllerR.transform.forward + 0.5f * circle * (controllerR.transform.up - controllerR.transform.right)));
            raycasts[i].Add(new Ray(controllerR.transform.position, controllerR.transform.forward - 0.5f * circle * (controllerR.transform.up - controllerR.transform.right)));
        }
    }
    
    /*bool checkforFunMode()
    {
        return FunMode.GetComponent<KeyCombo>().active;
    }*/
}
