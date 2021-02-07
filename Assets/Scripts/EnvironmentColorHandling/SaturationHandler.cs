using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class SaturationHandler : MonoBehaviour
{
    [SerializeField] protected  internal EnemySpawner enemySpawner;
    public abstract void IncreaseSaturation();

}