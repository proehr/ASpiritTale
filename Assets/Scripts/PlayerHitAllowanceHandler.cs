using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitAllowanceHandler : MonoBehaviour
{
    public bool canHit = true;
    // Start is called before the first frame update
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Contains("Left") || other.name.Contains("Right"))
        {
            canHit = true;
        }

    }
}
