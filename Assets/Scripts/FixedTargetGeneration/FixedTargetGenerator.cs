using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FixedTargetGenerator : MonoBehaviour
{
  [SerializeField] private GameObject Enemy = null;    // The Parent of all Parents, The fixed Boxing Enemy
  
  private void StartGeneratingFixedTargets()
  {
    List<GameObject> targets = new List<GameObject>();
    
    foreach (Transform child in transform)    // for all children of the current object
    {
      if (child.gameObject.CompareTag("Target")) // if the child is a Target
      {
        targets.Add(child.gameObject); // add Target to the List
        
      }
    }

    int temp = UnityEngine.Random.Range(0, targets.Count + 2 ); // random number between 0 and amount of targets  (excluded) 
    
    if (temp < targets.Count) // Unity website says that the max range from Random.Range is included, which doesn't seem to be the case, but in case it is, here is an if
    { 
      targets[temp].SetActive(true); // activate child Target
      
      Enemy.GetComponent<FixedBoxingEnemyLife>().AddToAmountOfTargets();
      //Enemy.SendMessage("AddToAmountOfTargets");                  // at the moment the amount of targets is fixed, increase the max range of temp by 1, and change  
    }                                                               // amountOfSpawnedTargets in FixedBoxingEnemyLife to 0, to change that
      
  }
}
