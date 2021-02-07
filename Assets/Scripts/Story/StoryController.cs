using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StoryController : MonoBehaviour
{
    [SerializeField] private int periodLength = 2;
    [SerializeField] private StoryPiece paperPrefab = null;
    [SerializeField] private List<TextAsset> storyTexts = new List<TextAsset>();
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private GameObject centerEyeAnchor;

    private static int enemyCounter;
    private static int storyCounter;

    private static StoryPiece activeStory;
    private bool destroyedPiece;

    // Start is called before the first frame update
    void Start()
    {
        enemyCounter = 0;
        storyCounter = 0;

        SpawnStoryPiece();
        Invoke("destroyStoryPiece", 20.0f);
    }

    public void IncrementCounter()
    {
        enemyCounter++;
        if (enemyCounter % periodLength == 1 || periodLength == 1)
        {
            storyCounter++;
            if (storyCounter < storyTexts.Count)
            {
                destroyedPiece = false;
                SpawnStoryPiece();
                Invoke("destroyStoryPiece", 20.0f);
            }
            else
            {
                enemySpawner.SpawnNextEnemy(5);
            }
        }
        else
        {
            enemySpawner.SpawnNextEnemy(5);  // spawn new enemy with a chosen delay
        }
    }

    private void SpawnStoryPiece()
    {
        activeStory = Instantiate(paperPrefab);
        activeStory.enemySpawner = enemySpawner;
        Transform ceeTransform = centerEyeAnchor.transform;
        Vector3 forward = ceeTransform.forward;
        float length = (float) Math.Sqrt(forward.x * forward.x + forward.z * forward.z);
        var position = ceeTransform.position;
        var activeStoryTransform = activeStory.transform;
        activeStoryTransform.position = new Vector3(position.x + 0.4f * forward.x / length,
            position.y, position.z + 0.4f * forward.z / length);
        activeStoryTransform.forward = new Vector3(-forward.x, 0, -forward.z);
        activeStory.SetText(storyTexts[storyCounter].text);
    }

    private void Update()
    {
        if (UIEnvironment.paused)
        {
            return;
        }

        if (activeStory != null &&
            Vector3.Distance(activeStory.transform.position, centerEyeAnchor.transform.position) > 0.7)
        {
            Transform ceeTransform = centerEyeAnchor.transform;
            Vector3 forward = ceeTransform.forward;
            float length = (float) Math.Sqrt(forward.x * forward.x + forward.z * forward.z);
            var position = ceeTransform.position;
            activeStory.gameObject.GetComponent<Floater>().startPos =
                new Vector3(position.x + 0.4f * forward.x / length,
                    position.y, position.z + 0.4f * forward.z / length);
            activeStory.transform.forward = new Vector3(-forward.x, 0, -forward.z);
        }

        if (activeStory != null && activeStory.isActiveAndEnabled && OVRInput.Get(OVRInput.Button.Two))
        {
            destroyedPiece = true;
            activeStory.gameObject.GetComponent<MatDissolver>().DissolveEnemy();
            Destroy(activeStory.transform.GetChild(0).gameObject);
            Destroy(activeStory.gameObject, 2.8f);
            activeStory = null;
        }
    }
    
    private void destroyStoryPiece()
    {
        if (activeStory != null && activeStory.isActiveAndEnabled && !destroyedPiece)
        {
            activeStory.gameObject.GetComponent<MatDissolver>().DissolveEnemy();
            Destroy(activeStory.transform.GetChild(0).gameObject);
            Destroy(activeStory.gameObject, 2.8f);
            activeStory = null;
        }
    }
}