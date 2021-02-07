using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int playerHealth = 10;

    private AudioSource _audioSource;
    // Start is called before the first frame update

    
    public delegate void HealthMustBeReduced(int healthAmount);

    public static event HealthMustBeReduced OnPlayerGotHit;
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        
        //Update HUDController with the healthAmount for the first time
        OnPlayerGotHit?.Invoke(playerHealth);
    }

    private void OnTriggerEnter(Collider other)
    {
        // At the beginning the controllers both trigger the box collider once (tested on pc, might differ while using the vr headset)
        if (other.name.Contains("LowManRightForeArm") || other.name.Contains("LowManLeftForeArm"))  
        
        //if (other.name.Contains("Enemy")) // shouldn't work either but at least the player won't die without a reason anymore
        {
            
            _audioSource.Play();
            if (playerHealth > 1)
            {
                playerHealth -= 1;
            }
            else
            {
                PlayerDied();
            }
         
            OnPlayerGotHit?.Invoke(playerHealth);
        }
    }

    void PlayerDied()
    {
        UIEnvironment.dead = true;
    }
}
