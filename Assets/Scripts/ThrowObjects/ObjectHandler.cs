using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ObjectHandler : MonoBehaviour
{
    public string state;
    
    void Start()
    {
        state = "free";
    }

    private void Update()
    {
        checkforError();
        Rigidbody rb = GetComponent<Rigidbody>();
        //Debug.Log(state);
        switch (state)
        {
            case "free":
                rb.useGravity = true;
                rb.constraints = RigidbodyConstraints.None;
                break;
            case "drag":
                rb.useGravity = false;
                rb.constraints = RigidbodyConstraints.FreezeRotation;
                break;
            case "hold":
                rb.useGravity = false;
                rb.constraints = RigidbodyConstraints.None;
                break;
            case "throw":
                rb.useGravity = true;
                rb.constraints = RigidbodyConstraints.None;
                Destroy(gameObject, 10f);
                break;
        }
    }

    public void changeState(string state)
    {
        if (state.Equals("free") ||
            state.Equals("drag") ||
            state.Equals("hold") ||
            state.Equals("throw"))
        {
            this.state = state;
        }
        else
        {
            Debug.Log(state + " is not a accepted state");
        }
    }

    private void checkforError()
    {
        if (transform.position.y < 0)
        {
            transform.position = Vector3.one;
            state = "free";
        }
    }
}
