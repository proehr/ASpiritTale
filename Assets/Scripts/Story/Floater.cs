using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floater : MonoBehaviour
{
    internal Vector3 startPos = new Vector3();
    
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(startPos.x, startPos.y + Mathf.Sin(Time.fixedTime * Mathf.PI * 0.4f)*0.05f, startPos.z);
    }
}
