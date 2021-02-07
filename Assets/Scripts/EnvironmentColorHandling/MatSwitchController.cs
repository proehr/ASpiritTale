using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class MatSwitchController : SaturationHandler
{
    [SerializeField] GameObject[] itemGroups = null;
    private int currentGroup = 0;
    private Transform firework = null;

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
            
        foreach (var rend in children)
        {
            if (rend.transform.GetComponent<MaterialHolder>())
            {
                rend.material = rend.transform.GetComponent<MaterialHolder>().mat;
            }
        }

        currentGroup++;
    }
}
