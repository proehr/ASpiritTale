using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BroadcastSaturationIncrease : MonoBehaviour
{
    [SerializeField] private float increment = 0.1f;
    
    // The parent object of all environmental objects, tells its childs to increase their saturation by the increment
    public void IncreaseSaturation()
    {
        BroadcastMessage("IncrementSaturation", increment);
    }
}
