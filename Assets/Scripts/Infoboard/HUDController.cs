using System.Collections;
using System.Collections.Generic;
using Oculus.Platform.Samples.VrHoops;
using SymbolDrawing;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
public class HUDController : MonoBehaviour
{
    
    private int _currentScore = 0;
    public TextMeshPro scoreTextMesh;        //must be connected to a textMeshBox in UI

    public TextMeshPro highScoreTextMesh;    //must be connected to a textBox in UI

    public TextMeshPro healthTextMesh;       //must be connected 
    
    // Start is called before the first frame update
    void Start()
    {
        FixedBoxingEnemyLife.OnEnemyHit += GotScore;
        PoseTracking.OnPoseCorrect += GotScore;
        Symbol.OnSymbolDestroy += GotScore;
        CircularSymbol.OnCircularSymbolDestroy += GotScore;
        PlayerHealth.OnPlayerGotHit += HealthReduced;
        TimeLimit.OnTimeLimitExceeded += GotScore;
        
        scoreTextMesh.text = _currentScore.ToString();
        
        highScoreTextMesh.text = PlayerPrefs.GetInt("High Score", 0).ToString();
        
    }

    public void GotScore(int scored)
    {
        _currentScore += scored;
        scoreTextMesh.text = _currentScore.ToString();

        if (_currentScore > PlayerPrefs.GetInt("High Score", 0))
        {
            PlayerPrefs.SetInt("High Score", _currentScore);
            highScoreTextMesh.text = _currentScore.ToString();
        }
    }

    public void HealthReduced(int healthAmount)
    {
        healthTextMesh.text = healthAmount.ToString();
    }

    //this should be called automatically to sign out the observer as an enemy is destroyed  
    private void OnDisable()
    {
        FixedBoxingEnemyLife.OnEnemyHit -= GotScore;
        PoseTracking.OnPoseCorrect -= GotScore;
        PlayerHealth.OnPlayerGotHit -= HealthReduced;
        Symbol.OnSymbolDestroy -= GotScore;
        CircularSymbol.OnCircularSymbolDestroy -= GotScore;
        TimeLimit.OnTimeLimitExceeded -= GotScore;
    }
    
    
    //activate the methode in case to  reset highScore
    
    // public void Reset()
    // {
    //     PlayerPrefs.DeleteAll();
    //     _highScoreText.text = "0";
    // }
    
}
