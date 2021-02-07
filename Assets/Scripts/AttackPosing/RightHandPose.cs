using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightHandPose : MonoBehaviour
{
    public GameObject rightHand;
    private int direction;
    private int positionIndex;
    private int lastPosition;
    private Vector3[] positions = { new Vector3(-1.11f, 2.42f, 0.0f),
                                    new Vector3(-1.0f, 1.83f, 0.0f),
                                    new Vector3(-0.98f, 1.0f, 0.0f),
                                    new Vector3(-0.97f, 0.543f, 0.0f),
                                    new Vector3(-0.86f, 0.16f, 0.0f)};
    private Transform rightHandTransform;
    private GameObject enemy;
    
    public bool reached1;
    public bool reached2;
    public PoseEnemyMovementController enemyController;
    
    
    // Start is called before the first frame update
    void Start()
    {
        positionIndex = Random.Range(0, positions.Length);
        enemyController = transform.root.GetComponentInChildren<PoseEnemyMovementController>();
        enemy = transform.root.gameObject;
        
        if ((enemy.transform.position.x == 0) && (enemy.transform.position.z < 0))
        {
            direction = 0;
            for (int i = 0; i < positions.Length; i++)
            {
                positions[i].x = -positions[i].x;
            }
        }
        else if((enemy.transform.position.x < 0) && (enemy.transform.position.z == 0) )
        {
            direction = 1;
        }
        else if((enemy.transform.position.x == 0) && (enemy.transform.position.z > 0) )
        {
            direction = 2;
            //positions[positionIndex].x = -positions[positionIndex].x;
        }
        else if((enemy.transform.position.x > 0) && (enemy.transform.position.z == 0) )
        {
            direction = 3;
            for (int i = 0; i < positions.Length; i++)
            {
                positions[i].x = -positions[i].x;
            }
        }
        else
        {
            direction = -1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        setPosition();   
    }

    private void setPosition()
    {
        rightHandTransform = transform;
        Vector3 newPosition = transform.position;

        if (enemyController.ReturnStatus())
        {
            switch (direction)
            {
                 case 0:
                     newPosition.z = 0.0f;
                     if (!reached1)
                     {
                         if (rightHandTransform.position.x < positions[positionIndex].x)
                         {
                             newPosition.x += Time.deltaTime;
                             if (newPosition.x > positions[positionIndex].x)
                             {
                                 newPosition.x = positions[positionIndex].x;
                                 reached1 = true;
                             }
                         }
                         else if (rightHandTransform.position.x > positions[positionIndex].x)
                         {
                             newPosition.x -= Time.deltaTime;
                             if (newPosition.x < positions[positionIndex].x)
                             {
                                 newPosition.x = positions[positionIndex].x;
                                 reached1 = true;
                             }
                         }
                     }
                     break;
                 case 1:
                     newPosition.x = 0.0f;
                     if (!reached1)
                     {
                         if (rightHandTransform.position.z < positions[positionIndex].x)
                         {
                             newPosition.z += Time.deltaTime;
                             if (newPosition.z > positions[positionIndex].x)
                             {
                                 newPosition.z = positions[positionIndex].x;
                                 reached1 = true;
                             }
                         }
                         else if (rightHandTransform.position.z > positions[positionIndex].x)
                         {
                             newPosition.z -= Time.deltaTime;
                             if (newPosition.z < positions[positionIndex].x)
                             {
                                 newPosition.z = positions[positionIndex].x;
                                 reached1 = true;
                             }
                         }
                     }
                     break;
                 case 2: 
                     newPosition.z = 0.0f;
                     if (!reached1)
                     {
                    
                         if (rightHandTransform.position.x < positions[positionIndex].x)
                         {
                             newPosition.x += Time.deltaTime;
                             if (newPosition.x > positions[positionIndex].x)
                             {
                                 newPosition.x = positions[positionIndex].x;
                                 reached1 = true;
                             }
                         }
                         else if (rightHandTransform.position.x > positions[positionIndex].x)
                         {
                             newPosition.x -= Time.deltaTime;
                             if (newPosition.x < positions[positionIndex].x)
                             {
                                 newPosition.x = positions[positionIndex].x;
                                 reached1 = true;
                             }
                         }
                     }
                     break;
                 case 3:
                     newPosition.x = 0.0f;
                     if (!reached1)
                     {   
                    
                         if (rightHandTransform.position.z < positions[positionIndex].x)
                         {
                             newPosition.z += Time.deltaTime;
                             if (newPosition.z > positions[positionIndex].x)
                             {
                                 newPosition.z = positions[positionIndex].x;
                                 reached1 = true;
                             }
                         }
                         else if (rightHandTransform.position.z > positions[positionIndex].x)
                         {
                             newPosition.z -= Time.deltaTime;
                             if (newPosition.z < positions[positionIndex].x)
                             {
                                 newPosition.z = positions[positionIndex].x;
                                 reached1 = true;
                             }
                         }
                     } 
                     break;
            }
            if (!reached2)
            {
                if (rightHandTransform.position.y < positions[positionIndex].y)
                {
                     newPosition.y += Time.deltaTime;
                     if (newPosition.y > positions[positionIndex].y)
                     {
                         newPosition.y = positions[positionIndex].y;
                         reached2 = true;
                     }
                }
                else if (rightHandTransform.position.y > positions[positionIndex].y)
                {
                    newPosition.y -= Time.deltaTime;
                    if (newPosition.y < positions[positionIndex].y)
                    {
                        newPosition.y = positions[positionIndex].y;
                        reached2 = true;
                    }
                }
            }
            rightHandTransform.position = newPosition;
        } 
    }
    public void resetPosition()
    {
        transform.position = rightHand.transform.position;
        lastPosition = positionIndex;
        positionIndex = Random.Range(0, positions.Length);
        while (lastPosition == positionIndex)
        {
            positionIndex = Random.Range(0, positions.Length);
        }
        reached1 = false;
        reached2 = false;
    }
}
