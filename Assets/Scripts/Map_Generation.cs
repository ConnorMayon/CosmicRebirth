using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map_Generation : MonoBehaviour
{
    public GameObject staticPlatformPrefab;
    public GameObject floatingIslandPrefab;
    public GameObject movingPlatformPrefab;
    public GameObject crumblingPlatformPrefab;
    public GameObject checkpointPrefab;
    public GameObject player;
    public GameObject checkpointTriggerPrefab;


    public float maxVerticalOffset; // Maximum vertical difference for floating islands

    private Vector3 nextSpawnPoint = new Vector3(5.21f, -3.27f, -5f);
    private int platformsSpawned;

    private bool isCheckpointCreated = false;
    private bool isCheckpointReached = false;
    private bool lastPlatformWasMoving = false;
    private bool lastPlatformWasFloatingIsland = false;


    private const float StandardPlatformLength = 2* 7.7596f;

    // Start is called before the first frame update
    void Start()
    {
        InitializePlatforms();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isCheckpointCreated)
        {
            GeneratePlatform();
            CheckForCheckpointCreation();
        }
        else if (!isCheckpointReached && IsPlayerCloseToCheckpoint())
        {
            isCheckpointReached = true;
            ResumePlatformGeneration();
        }
    }

    void InitializePlatforms()
    {
        Instantiate(staticPlatformPrefab, nextSpawnPoint, Quaternion.identity);
        nextSpawnPoint.x += StandardPlatformLength; // Move right for the next platform
        platformsSpawned++;

        // Generate remaining initial platforms
        for (int i = 1; i < 10; i++) // Start from 1 since the first platform is already created
        {
            GeneratePlatform();
        }
    }

    void GeneratePlatform()
    {
        GameObject platformPrefab = ChoosePlatformPrefab();

        // Apply vertical offset only for floating islands
        if (platformPrefab == floatingIslandPrefab)
        {
            nextSpawnPoint.y += Random.Range((nextSpawnPoint.y + 5), (maxVerticalOffset + nextSpawnPoint.y));
        }
        else
        {
            nextSpawnPoint.y = -3.27f; // Reset Y for other platform types
        }

        Instantiate(platformPrefab, nextSpawnPoint, Quaternion.identity);

        float additionalMoveDistance = 0f;
        if (platformPrefab == movingPlatformPrefab)
        {
            // If it's a moving platform, add the move distance to the next spawn point calculation
            Moving_Platform movingPlatformScript = platformPrefab.GetComponent<Moving_Platform>();
            if (movingPlatformScript != null)
            {
                additionalMoveDistance = movingPlatformScript.moveDistance;
            }
        }

        nextSpawnPoint.x += StandardPlatformLength + additionalMoveDistance; // Move right for the next platform

        platformsSpawned++;

        lastPlatformWasFloatingIsland = platformPrefab == floatingIslandPrefab;
        lastPlatformWasMoving = platformPrefab == movingPlatformPrefab;
    }

    GameObject ChoosePlatformPrefab()
    {
        int platformChoice;
        do
        {
            platformChoice = Random.Range(0, 4); // 4 different platform types

            // Check to prevent back-to-back floating islands or moving platforms
            if ( // Assuming '1' corresponds to floatingIslandPrefab
                (lastPlatformWasMoving && platformChoice == 2))           // Assuming '2' corresponds to movingPlatformPrefab
            {
                continue; // Re-roll if the same type of platform is selected again
            }

            break;
        } while (true);

        switch (platformChoice)
        {
            case 0:
                return staticPlatformPrefab;
            case 1:
                return floatingIslandPrefab;
            case 2:
                return movingPlatformPrefab;
            case 3:
                return crumblingPlatformPrefab;
            default:
                return staticPlatformPrefab; // Default case
        }
    }

    void CheckForCheckpointCreation()
    {
        if (platformsSpawned % 20 == 0)
        {
            CreateCheckpoint();
            isCheckpointReached = false;
        }
    }

    void CreateCheckpoint()
    {
        // Instantiate the checkpoint prefab at the next spawn point
        GameObject checkpointInstance = Instantiate(checkpointPrefab, nextSpawnPoint, Quaternion.identity);
        isCheckpointCreated = true;

        // If you have a separate CheckpointTrigger prefab, instantiate it
        if (checkpointTriggerPrefab != null)
        {
            // Create an offset vector, for example, 1 unit above the checkpoint
            Vector3 triggerOffset = new Vector3(0, 0, 0);

            // Instantiate the CheckpointTrigger at the adjusted position
            Instantiate(checkpointTriggerPrefab, checkpointInstance.transform.position + triggerOffset, Quaternion.identity, checkpointInstance.transform);
        }

        // Update the next spawn point for the next platform or checkpoint
        nextSpawnPoint += new Vector3(StandardPlatformLength, -3.27f, -5f); // Adjust based on your game's requirements
    }


    bool IsPlayerCloseToCheckpoint()
    {
        if (player != null)
        {
            float distanceToCheckpoint = Mathf.Abs(nextSpawnPoint.x - player.transform.position.x);
            return distanceToCheckpoint < 50f;
        }
        return false;
    }

    void ResumePlatformGeneration()
    {
        isCheckpointCreated = false;
        // Reset or adjust any necessary variables
    }
}
