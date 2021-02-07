using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyMovementController : MonoBehaviour
{
    [SerializeField] GameObject Player = null;

    [SerializeField] Animator anim = null;

    private Transform playerTransform;
    private Transform enemyTransform;

    private Vector3 playerPos;
    private Vector3 enemyPos;

    private Vector3 Playerdirection;
    private float distanceToPlayer;

    private bool isAttacking = true;

    private float moveSpeed = 7; // Enemy movement speed, gets overwritten when using an EnemySpawner
    [SerializeField] private float followRadius = 20; // Distance from Player, at which the Enemy starts walking towards the Player
    private float stopRadius = 0; // Distance from Player, at which the Enemy stops walking towards the Player, is set in TargetGenerator
    private static readonly int StoppedWalking = Animator.StringToHash("stoppedWalking");

    // Update is called once per frame
    void Update()
    {
        // Player and Enemy Transform
        playerTransform = Player.transform;
        enemyTransform = transform;

        // Player and Enemy Position
        playerPos = playerTransform.position;
        enemyPos = enemyTransform.position;

        // Enemy looks at Player
        transform.LookAt(playerTransform);

        // Distance between Player and Enemy
        distanceToPlayer = Vector3.Distance(enemyPos, playerPos);

        // Check if Enemy is following distance to the Player
        if(distanceToPlayer <= followRadius && distanceToPlayer >= stopRadius){

            // Enemy walks towards the Player
            enemyTransform.position += enemyTransform.forward * (moveSpeed * Time.deltaTime);

            if(isAttacking){ // The animation is playing in a loop, starting it again mid way leads to abrupt movements sometimes
                isAttacking = !isAttacking;
                anim.Play("Walking");
            }

        }
        else
        {
            AttackPlayerAnimation();
        }
    }

    void AttackPlayerAnimation()
    {
        if (!isAttacking)
        {
            anim.SetBool(StoppedWalking, true); // starts the transition to the attack animations
            isAttacking = !isAttacking;
        }

    }
    void SetStopRadius(float radius)
    {
        stopRadius = radius;
    }

    public void setMovementSpeed(float speed)
    {
        moveSpeed = speed;
    }
}