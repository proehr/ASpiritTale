using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultMatItemGroupSaturationChangeHandler : MonoBehaviour
{
    [SerializeField] private GameObject[] itemGroups = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private int currentGroup = 0;

    public void IncreaseMultMatItemGroupSaturation()
    {
        var children = itemGroups[currentGroup].GetComponentsInChildren<Renderer>();
        
        
        foreach (var rend in children)
        {
            foreach (var mat in rend.materials)
            {
                float H, S, V;        // H S V color values
                Color.RGBToHSV(mat.color, out H, out S, out V); // convert RGB to HSV
                S = 1;
                mat.color = Color.HSVToRGB(H, S, V);

            }
        }

        
        
        
        if(currentGroup < itemGroups.Length){ currentGroup++;}
    }
}
