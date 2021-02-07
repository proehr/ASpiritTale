using System;
using System.Collections;
using System.Collections.Generic;
using SymbolDrawing;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PoseTracking : MonoBehaviour
{
    public GameObject leftHand;
    public GameObject rightHand;
    public GameObject leftHandTarget;
    public GameObject rightHandTarget;
    public SymbolGenerator symbolGenerator = null;
    
    protected internal GameObject player;
    //protected StoryController storyController;

    private GameObject leftHandPlayer;
    private GameObject rightHandPlayer;
    
    private bool attacking;

    private LeftHandPose lhPose;
    private RightHandPose rhPose;

    public GameObject leftRig;
    public GameObject rightRig;

    private bool leftHandSet;
    private bool rightHandSet;
    private bool playerFound;
    private bool playingAttack;

    private bool notGeneratedSymbol;
    //private EnemySpawner enemySpawner;
    private double distance = 0.2;
    private int poseNumber = 0;
    public PoseEnemyMovementController movementController;
    
    //Score System
    public int pointValue = 10;
    public delegate void ScoreMustBeAdded(int scored);
    public static event ScoreMustBeAdded OnPoseCorrect;
    
    // Start is called before the first frame update
    void Start()
    {
        //enemySpawner = GameObject.Find("VariousEnemySpawner").GetComponent<EnemySpawner>();
        //storyController = GameObject.Find("StoryController").GetComponent<StoryController>();
       
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerFound)
        {
            //player = GameObject.Find("GamePlayerController 1");
            if (player != null && player.activeSelf)
            {
                lhPose = leftHandTarget.GetComponent<LeftHandPose>();
                rhPose = rightHandTarget.GetComponent<RightHandPose>();
                leftHandPlayer = player.transform.GetChild(0).GetChild(0).GetChild(4).gameObject;
                rightHandPlayer = player.transform.GetChild(0).GetChild(0).GetChild(5).gameObject;
                playerFound = true;
            }
        }
        else
        {
            if (lhPose.reached1 && lhPose.reached2 && rhPose.reached1 && rhPose.reached2)
            {
                Vector2 leftHandXY = new Vector2();
                Vector2 rightHandXY = new Vector2();
                Vector2 leftHandPlayerXY = new Vector2();
                Vector2 rightHandPlayerXY = new Vector2();
                if (lhPose.getDirection() == 0 || lhPose.getDirection() == 2)
                {
                    leftHandXY = new Vector2(leftHand.transform.position.x, leftHand.transform.position.y);
                    rightHandXY = new Vector2(rightHand.transform.position.x, rightHand.transform.position.y);
                    leftHandPlayerXY =
                        new Vector2(leftHandPlayer.transform.position.x, leftHandPlayer.transform.position.y);
                    rightHandPlayerXY =
                        new Vector2(rightHandPlayer.transform.position.x, rightHandPlayer.transform.position.y);
                }
                else if (lhPose.getDirection() == 1 || lhPose.getDirection() == 3)
                {
                    leftHandXY = new Vector2(leftHand.transform.position.z, leftHand.transform.position.y);
                    rightHandXY = new Vector2(rightHand.transform.position.z, rightHand.transform.position.y);
                    leftHandPlayerXY =
                        new Vector2(leftHandPlayer.transform.position.z, leftHandPlayer.transform.position.y);
                    rightHandPlayerXY =
                        new Vector2(rightHandPlayer.transform.position.z, rightHandPlayer.transform.position.y);
                }

                if (!playingAttack)
                {
                    leftHandSet = Vector2.Distance(leftHandXY, rightHandPlayerXY) <= distance;
                    rightHandSet = Vector2.Distance(rightHandXY, leftHandPlayerXY) <= distance;
                }
                else
                {
                    leftHandSet = true;
                    rightHandSet = true;
                }
            }
        }
        
        if (leftHandSet && rightHandSet)
        {
            
            if (poseNumber < 2)
            {
                if (!playingAttack)
                {
                    OnPoseCorrect?.Invoke(pointValue);
                    leftRig.GetComponent<Rig>().weight = 0;
                    rightRig.GetComponent<Rig>().weight = 0;
                    movementController.playAttack();
                    playingAttack = true;
                }
                
                if(movementController.finishedAttack())
                {
                    //Debug.Log(movementController.finishedAttack());
                    movementController.stopAttack();
                    leftRig.GetComponent<Rig>().weight = 1;
                    rightRig.GetComponent<Rig>().weight = 1;
                    lhPose.resetPosition();
                    rhPose.resetPosition();
                    leftHandSet = false;
                    rightHandSet = false;
                    poseNumber++;
                    playingAttack = false;
                }
                
            }
            else if (!notGeneratedSymbol)
            {
                OnPoseCorrect?.Invoke(pointValue);
                symbolGenerator.GenerateSymbol(transform);
                notGeneratedSymbol = true;
            }
        }
    }

}