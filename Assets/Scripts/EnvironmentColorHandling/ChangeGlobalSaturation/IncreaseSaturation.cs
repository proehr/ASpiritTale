using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseSaturation : MonoBehaviour
{
    Renderer renderer;
    float H, S, V;        // H S V color values
    
    
    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();

        Color.RGBToHSV(renderer.material.color, out H, out S, out V); // convert RGB to HSV
        
        S = 0; // makes the object (black, white, grey)
        
        renderer.material.color = Color.HSVToRGB(H, S, V);

    }

    // Increase the saturation of the material by the given implement
    void IncrementSaturation(float increment)
    {
        S += increment;
        renderer.material.color = Color.HSVToRGB(H, S, V);
    }
}
