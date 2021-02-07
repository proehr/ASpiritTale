using System;
using SymbolDrawing;
using UnityEngine;

public class FixedBoxingEnemyLife : MonoBehaviour
{
    [SerializeField] private float stopRadius = 0.5f; // Distance from Player, at which the Enemy stops walking towards the Player 
    [SerializeField] private float targetSpawnDistanceFromPlayer = 50f; //target spawn distance from player
    [SerializeField] GameObject Player = null;
    [SerializeField] GameObject enemyChild = null;  
    [SerializeField] EnemySpawner enemySpawner = null;  
    [SerializeField] float spawnNextEnemyDelay = 3f;
    [SerializeField] SymbolGenerator symbolGenerator = null;
    [SerializeField] protected SaturationHandler saturationHandler = null;
    
    private int amountOfSpawnedTargets = 0; // In case that the amount of targets isn't fixed, and AddToAmountOfTargets is used
    
    
    private Vector3 playerPos;
    private Vector3 enemyChildObjectPos;

    private Transform playerTransform;
    private Transform enemyChildObjectTransform;
    private float distanceToPlayer;

    private bool targetsWereGenerated = false;

    [SerializeField] GameObject env = null;
    
    
    //from Nima
    public int pointValue = 10;
    public int bonusEnemyDead = 50;
    public delegate void ScoreMustBeAdded(int scored);

    public static event ScoreMustBeAdded OnEnemyHit;
    
    // Start is called before the first frame update
    void Start()
    {
        enemyChildObjectTransform = gameObject.transform.GetChild(0);
        enemyChild = enemyChildObjectTransform.gameObject;
        //enemySpawner = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
       // saturationHandler = GameObject.Find("Environment").GetComponent<SaturationHandler>();
        enemyChild.SendMessage("SetStopRadius", stopRadius); // Set stopRadius in EnemyMovementController
    }

    // Update is called once per frame
    void Update()
    { 
        CheckIfPlayerInTargetGeneratingDistance();
    }
    
    public void AddToAmountOfTargets()
    { 
        amountOfSpawnedTargets += 1;
    }

    // Reduce the amount of Targets by 1, every tie the Enemy gets hit (called in the TargetCollision Script)
    public void EnemyGotHit()
    {
        //invokes the event to send the points to the HUDController
        OnEnemyHit?.Invoke(pointValue);
        
        // Enemy Target got hit
        if (amountOfSpawnedTargets > 1)
        {
            amountOfSpawnedTargets -= 1;
        }
        // Last target was hit, enemy gets destroyed
        else
        {
            // enemySpawner.SpawnNextEnemy();    // spawn new enemy with a chosen delay
            
          // saturationHandler.IncreaseSaturation();
            //Destroy(gameObject);
            enemyChild.GetComponent<Animator>().Play("Idle2");
            symbolGenerator.GenerateSymbol(transform);
        }
    }
    
    void CheckIfPlayerInTargetGeneratingDistance()
    {
        if (!targetsWereGenerated)
        {
            // Player and Enemy Position
            playerTransform = Player.transform;
            playerPos = playerTransform.position;
            enemyChildObjectTransform = gameObject.transform.GetChild(0);
            enemyChildObjectPos = enemyChildObjectTransform.position;

            // Distance between Player and Enemy
            distanceToPlayer = Vector3.Distance(enemyChildObjectPos, playerPos);

            // Check if enemy close enough and if targets haven't been generated yet
            if (distanceToPlayer <= targetSpawnDistanceFromPlayer)
            {
                targetsWereGenerated = true;

                BroadcastMessage("StartGeneratingFixedTargets");
            }
        }
    }
}
