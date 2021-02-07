  using UnityEngine;
  using UnityEngine.SceneManagement;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float distanceToPlayer = 0;
    [SerializeField] private GameObject[] enemyType;
    [SerializeField] private float spawnDelay = 0;
    [SerializeField] private GameObject player;

    public bool spawnLastEnemy;

    private bool endlessMode;

    private float movementSpeed = 7;

    private float movementSpeedMax = 15;
    // Start is called before the first frame update
    void Start()
    {
        //SpawnEnemy(); // spawn first enemy
    }

    void SpawnEnemy()
    {
        float enemyYPos = 1.269f;
        float enemyRot = 0; // rotation, so that the enemy directly faces the player when spawned

        // Create a position variable for the enemy
        Vector3 enemyPosition = Vector3.zero;
        int index = Random.Range(0, 4);

        // This spawns one enemy either east, west, south or north of the player
        // Assuming that the Player's initial pos in the map stays (0,something,0)
        switch (index)
        {
            case 0:
                enemyPosition = new Vector3(distanceToPlayer, enemyYPos, 0);
                enemyRot = 90;
                break;
            case 1:
                enemyPosition = new Vector3(0, enemyYPos, -distanceToPlayer);
                enemyRot = 180;
                break;
            case 2:
                enemyPosition = new Vector3(-distanceToPlayer, enemyYPos, 0);
                enemyRot = 270;
                break;
            default:
                enemyPosition = new Vector3(0, enemyYPos, distanceToPlayer);
                enemyRot = 0;
                break;
        }

        // initialise the new enemy
        int random = Random.Range(0, enemyType.Length+1);
        int weightedRandom;
        if (enemyType.Length > 1)
        {
            weightedRandom = random < 1 ? 1 : 0; 
        }
        else
        {
            weightedRandom = 0;
        }
        var newEnemy = Instantiate(enemyType[weightedRandom], enemyPosition, Quaternion.Euler(00f, enemyRot, 0f));
        if (spawnLastEnemy)
        {
            newEnemy.GetComponent<FinalBoss>().showMask();
            endlessMode = true;
            spawnLastEnemy = false;
        }
        if (endlessMode && (movementSpeed < movementSpeedMax))
        {
            movementSpeed += 0.1f;
                
        }
        if (newEnemy.CompareTag("PoseEnemy"))
        {
            newEnemy.GetComponent<PoseTracking>().player = player;
            newEnemy.GetComponentInChildren<PoseEnemyMovementController>().setMovementSpeed(movementSpeed);
        }
        else
        {
            newEnemy.GetComponentInChildren<EnemyMovementController>().setMovementSpeed(movementSpeed);
        }
    }

    public void SpawnNextEnemy() // called using SendMessage
    {
        Invoke(nameof(SpawnEnemy), spawnDelay); // spawn new enemy with a chosen delay
    }

    public void SpawnNextEnemy(float delay)
    {
        Invoke(nameof(SpawnEnemy), delay);
    }
    
}