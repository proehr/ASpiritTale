using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckTargetGeneratingRadius : MonoBehaviour
{
    [SerializeField] GameObject Player = null;

    private Transform playerTransform;
    private Transform enemyTransform;
    
    private Vector3 playerPos;
    private Vector3 enemyPos;

    private Vector3 Playerdirection;
    private float distanceToPlayer;
    
    [SerializeField] private float stopRadius = 2;    // Distance from Player, at which the Enemy stops walking towards the Player

    private bool targetsWereGenerated = false;

    // Update is called once per frame
    void Update()
    {
        // Player and Enemy Transform
        playerTransform = Player.transform;
        enemyTransform = transform;
        
        // Player and Enemy Position
        playerPos = playerTransform.position;
        enemyPos = enemyTransform.position;

        // Distance between Player and Enemy
        distanceToPlayer = Vector3.Distance(enemyPos, playerPos);
        
        // Check if enemy close enough and if targets haven't been generated yet
        if(!targetsWereGenerated && distanceToPlayer <= stopRadius)
        {
            targetsWereGenerated = true;
            
            // Enemy's X and Z pos after stopping his movement
            float[] temp = { enemyPos.x, enemyPos.z};
            gameObject.transform.parent.SendMessage("StartGeneratingTargets", temp);
        } 
        
    }
}
