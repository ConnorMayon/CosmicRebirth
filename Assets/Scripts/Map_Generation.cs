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

    public GameObject winterStaticPlatformPrefab;
    public GameObject factoryStaticPlatformPrefab;
    public GameObject cityStaticPlatformPrefab;

    public float maxVerticalOffset; // Maximum vertical difference for floating islands

    private Vector3 nextSpawnPoint = new Vector3(5.21f, -3.27f, -5f);
    private int platformsSpawned;
    private List<GameObject> spawnedPlatforms = new List<GameObject>();

    private bool isCheckpointCreated = false;
    private bool isCheckpointReached = false;
    private bool lastPlatformWasMoving = false;
    private bool lastPlatformWasCrumbling = false;
    private bool lastPlatformWasFloatingIsland = false;
    private bool lastTwoPlatformsWereFloatingIsland = false;

    private GameObject lastCheckpoint;
    private const float StandardPlatformLength = 2 * 7.7596f;

    void Start()
    {
        InitializePlatforms();
    }

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
            Themes.themes.updateCurrentTheme();
            ResumePlatformGeneration();
        }
    }

    void InitializePlatforms()
    {
        for (int i = 0; i < 10; i++)
        {
            GameObject newPlatform = Instantiate(staticPlatformPrefab, nextSpawnPoint, Quaternion.identity);
            spawnedPlatforms.Add(newPlatform);
            nextSpawnPoint.x += StandardPlatformLength;
            platformsSpawned++;
        }
    }

    void GeneratePlatform()
    {
        GameObject platformPrefab = ChoosePlatformPrefab();

        if (platformPrefab == floatingIslandPrefab)
        {
            nextSpawnPoint.y += Random.Range((nextSpawnPoint.y + 5), (maxVerticalOffset + nextSpawnPoint.y));
        }
        else
        {
            nextSpawnPoint.y = -3.27f;
        }

        GameObject newPlatform = Instantiate(platformPrefab, nextSpawnPoint, Quaternion.identity);

        spawnedPlatforms.Add(newPlatform);

        float additionalMoveDistance = 0f;
        if (platformPrefab == movingPlatformPrefab)
        {
            Moving_Platform movingPlatformScript = platformPrefab.GetComponent<Moving_Platform>();
            if (movingPlatformScript != null)
            {
                additionalMoveDistance = movingPlatformScript.moveDistance;
            }
        }

        nextSpawnPoint.x += StandardPlatformLength + additionalMoveDistance;
        platformsSpawned++;

        lastPlatformWasFloatingIsland = platformPrefab == floatingIslandPrefab;
        lastTwoPlatformsWereFloatingIsland = lastPlatformWasFloatingIsland && (platformPrefab == floatingIslandPrefab);
        lastPlatformWasMoving = platformPrefab == movingPlatformPrefab;
        lastPlatformWasCrumbling  = platformPrefab == crumblingPlatformPrefab;
    }

    GameObject ChoosePlatformPrefab()
    {
        int platformChoice;
        do
        {
            platformChoice = Random.Range(0, 4);

            if (((lastPlatformWasMoving || lastPlatformWasCrumbling ) && platformChoice == 2))
            {
                continue;
            }
            if (lastTwoPlatformsWereFloatingIsland && platformChoice == 1)
            {
                continue;
            }

            break;
        } while (true);

        switch (platformChoice)
        {
            case 0:
                switch (Themes.getTheme())
                {
                    case 0: return staticPlatformPrefab;
                    case 1: return winterStaticPlatformPrefab;
                    case 2: return factoryStaticPlatformPrefab;
                    case 3: return cityStaticPlatformPrefab;
                }

                return staticPlatformPrefab;

            case 1: return floatingIslandPrefab;
            case 2: return movingPlatformPrefab;
            case 3: return crumblingPlatformPrefab;
            default: return staticPlatformPrefab;
        }
    }

    void CheckForCheckpointCreation()
    {
        if (platformsSpawned % 20 == 0)
        {
            CreateCheckpoint();
            Themes.switchTheme();
            isCheckpointReached = false;
        }
    }

    void CreateCheckpoint()
    {
        lastCheckpoint = Instantiate(checkpointPrefab, nextSpawnPoint, Quaternion.identity);
        isCheckpointCreated = true;

        if (checkpointTriggerPrefab != null)
        {
            Instantiate(checkpointTriggerPrefab, lastCheckpoint.transform.position, Quaternion.identity, lastCheckpoint.transform);
        }

        nextSpawnPoint += new Vector3(StandardPlatformLength, -3.27f, -5f);
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


    }

    public void DespawnPreviousPlatforms(float checkpointXPosition)
    {
        foreach (var platform in spawnedPlatforms)
        {
            if (platform != null && platform.transform.position.x < checkpointXPosition)
            {
                Destroy(platform);
            }
        }

        // Remove all destroyed platforms from the list
        spawnedPlatforms.RemoveAll(item => item == null || item.transform.position.x < checkpointXPosition);
    }


}
