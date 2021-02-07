using System.Collections;
using System.Collections.Generic;
using SymbolDrawing;
using UnityEngine;

public class TimeLimit : MonoBehaviour
{
    private float destroyTime = 50.0f;
    
    public int punishValue = -50;
    
    public delegate void ScoreMustBeAdded(int scored);
    public static event ScoreMustBeAdded OnTimeLimitExceeded;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("destroyEnemy", destroyTime);
    }

    private void destroyEnemy()
    {
        Symbol symbol = gameObject.GetComponentInChildren<Symbol>();
        if (gameObject.activeSelf && ((symbol != null && !symbol.destroyed) || symbol == null))
        {
            OnTimeLimitExceeded?.Invoke(punishValue);
            //something about the score
            GameObject.Find("VariousEnemySpawner").GetComponent<EnemySpawner>().SpawnNextEnemy();
            Destroy(gameObject);
        }
    }
}