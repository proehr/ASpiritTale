using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ItemGroupSaturationChangeHandler : SaturationHandler
{
    [SerializeField] private GameObject[] itemGroups = null;
    [SerializeField] private Material[] fullSatMats = null;
    private Transform firework = null;
  
    private int currentGroup = 0;

    public override void IncreaseSaturation()
    {
        if (currentGroup == itemGroups.Length - 1)
        {
            enemySpawner.spawnLastEnemy = true;
        }

        if (currentGroup >= itemGroups.Length)
        {
            return;
        }
        
        var children = itemGroups[currentGroup].GetComponentsInChildren<Renderer>();
        
        
        firework = itemGroups[currentGroup].transform.GetChild(0);
        firework.GetComponent<AudioSource>().Play();
        firework.GetComponent<ParticleSystem>().Play();
        
        if (currentGroup != itemGroups.Length - 1)
        {
            for (int i = 1; i < children.Length; i++)
            {
                children[i].material = fullSatMats[0];
            }

        }
        else
        {
            for (int i = 1; i < children.Length; i++)
            {
                string name = children[i].gameObject.name;
                if (name.Equals("mount"))
                {
                    children[i].material = fullSatMats[1];
                }
                else if (name.Contains("mount"))
                {
                    children[i].material = fullSatMats[2];
                }
                else if (name.Equals("Water"))
                {
                    children[i].material = fullSatMats[3];
                }
                else if (name.Contains("Plane"))
                {
                    children[i].material = fullSatMats[4];
                }
                else
                {
                    children[i].material = fullSatMats[0];
                }

            }

        }
        
        currentGroup++;
    }


}
