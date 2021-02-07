using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatDissolver : MonoBehaviour
{
    private SkinnedMeshRenderer smr;  // the object's renderer

    private Material mat1;
    private Material mat2;
    
    float dissolvingValue = 0.0f;
    
    private float amount = 0;     // how much we want the material to dissolve. 0 = not at all
    float periodLength = 3;       // how long the dissolving should take
    int matAmount = 0;            // how many materials the Renderer has
    
    
    // Start is called before the first frame update
    void Start()
    {
        smr = GetComponent<SkinnedMeshRenderer>();
        matAmount = smr.materials.Length;
        
        // first material
        mat1 = smr.materials[0];
        
        // if the Object has more than one materials
        if(matAmount > 1)
            mat2 = smr.materials[1];
            
    }

    private void Update()
    {
        var increment = (amount) / periodLength;    // how much the material gets dissolved in a second
        
        dissolvingValue += increment * Time.deltaTime;
        
        mat1.SetFloat("_DissolveAmount", dissolvingValue); // change the dissolving amount of the material
        
        if(matAmount > 1)
            mat2.SetFloat("_DissolveAmount", dissolvingValue);
    }

    internal void DissolveEnemy()
    {
        amount = 1;    // 1 = completely dissolve the Material
    }
}
