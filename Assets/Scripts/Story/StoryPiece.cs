using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StoryPiece : MonoBehaviour
{
    protected internal EnemySpawner enemySpawner;
    public void SetText(string text)
    {
        TMP_Text storyText = transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>();
        storyText.text = text;
    }

    private void OnDestroy()
    {
        enemySpawner.SpawnNextEnemy();
    }
}
