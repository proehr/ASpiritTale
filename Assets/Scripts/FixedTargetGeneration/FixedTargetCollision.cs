using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedTargetCollision : MonoBehaviour
{
    [SerializeField] private GameObject Enemy = null; // The Parent of all Parents, The fixed Boxing Enemy
    [SerializeField] private float hapticDuration = 1;
    private PlayerHitAllowanceHandler _playerHitAllowanceHandler = null;
    private String targetType;
    private bool canhit = false;

    private void Start()
    {
        targetType = gameObject.name;
        _playerHitAllowanceHandler = GameObject.Find("PlayerHitResetBox").GetComponent<PlayerHitAllowanceHandler>();

    }

    // Target was hit
    private void OnTriggerEnter(Collider other)
    {
        if (_playerHitAllowanceHandler.canHit && 
            ((targetType.Contains("Left") && other.name.Contains("Left")) || (targetType.Contains("Right") && other.name.Contains("Right"))))
        {
            _playerHitAllowanceHandler.canHit = false;
            // Maybe add a destruction animation
            StartHapticFeedback(other.name);
            OnTargetDestroy();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("throwable"))
            OnTargetDestroy();
    }

    void StartHapticFeedback(string controllerName)
    {
        OVRInput.SetControllerVibration(1, 1,
            controllerName.Contains("Left")
                ? OVRInput.Controller.LTouch
                : OVRInput.Controller.RTouch); // start haptic feedback
        Invoke(nameof(StopHapticFeedback), hapticDuration);
    }

    void StopHapticFeedback()
    {
        OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.LTouch); // Stop haptic feedback
        OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.RTouch); // Stop haptic feedback
    }

    void OnTargetDestroy()
    {
        // Notify parent Object (the enemy) that the target was hit
        //Enemy.SendMessage("EnemyGotHit");
        Enemy.GetComponent<FixedBoxingEnemyLife>().EnemyGotHit();

        GetComponent<AudioSource>().Play(); //play destruction/hit sound
        GetComponent<MeshRenderer>().enabled = false; // make the object invisible
        GetComponent<CapsuleCollider>().enabled = false; // turn off colliders

        // Target gets Destroyed
        Destroy(gameObject, 3f);
    }
}