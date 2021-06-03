using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnThyPlatforms : MonoBehaviour
{
    // Prefabs for the two platform types
    public GameObject normalPlatform;
    public GameObject snappyPlatform;
    public GameObject startingPlatform;

    public DooDoo_Jumper player;
    
    public Vector2 startLoc;

    Vector2 lastSpawnPos;

    public int numPlatformLevelAtStart;

    public int maxDistanceBetweenPlatform;

    public int maxDistanceBeforeDespawn;

    public int maxNumOfPlatformPerLevel;

    public GameObject rightWall;
    public GameObject leftWall;

    public float rWX;
    public float lWX;

    public float disBetweenPlatformAndWall;

    bool gameStart = true;

    Queue<GameObject> platformQueue;

    /// <summary>
    /// Spawn platforms every 3-5 units increment since the last platform spawned. 
    /// Spawn 1-3 platforms at that height
    /// After spawning each platform, put them in the queue 
    /// Check the head of the queue and check the distance between the player and the platform. 
    /// If the distance is over the maxDistance then dequeue the head of the queue and destroy said platform. Keep checking until the distance to the head is not over maxDistance
    /// 
    /// </summary>

    // Start is called before the first frame update
    void Start()
    {
        Vector2 startLoc = player.transform.position;

        rWX = rightWall.transform.position.x - (rightWall.transform.localScale.x / 2) - disBetweenPlatformAndWall - normalPlatform.transform.localScale.x / 2;
        lWX = leftWall.transform.position.x + (rightWall.transform.localScale.x / 2) + disBetweenPlatformAndWall + normalPlatform.transform.localScale.x / 2;

        platformQueue = new Queue<GameObject>();

        SpawnPlatforms();
    }

    private void Update()
    {
        if(player.transform.position.y - platformQueue.Peek().transform.position.y > 10)
        {
            CheckAndDespawnPlatform();
            SpawnPlatforms();
        }
    }

    public void SpawnPlatforms()
    {
        int numPlatform = UnityEngine.Random.Range(1, maxNumOfPlatformPerLevel);
        
        if(gameStart)
        {

            Vector2 spawnLoc = new Vector2(startLoc.x, (startLoc.y - (player.transform.localScale.y / 2)) - (startingPlatform.transform.localScale.y / 2));

            platformQueue.Enqueue(Instantiate(startingPlatform, spawnLoc, new Quaternion()));
            lastSpawnPos = spawnLoc;



            spawnLoc = new Vector2(UnityEngine.Random.Range(rWX, lWX), lastSpawnPos.y + 1 +  UnityEngine.Random.Range(1, maxDistanceBetweenPlatform));

            var tmp = UnityEngine.Random.Range(0, 1);

            if (tmp - 1 < 0.5)
            {
                platformQueue.Enqueue(Instantiate(normalPlatform, spawnLoc, new Quaternion()));
            }
            else
            {
                platformQueue.Enqueue(Instantiate(snappyPlatform, spawnLoc, new Quaternion()));
            }

            lastSpawnPos = spawnLoc;

            for (int i = 0; i < numPlatformLevelAtStart - 1; i++)
            {
                for(int j = 0; j < numPlatform; j++) 
                {
                    spawnLoc = new Vector2(UnityEngine.Random.Range(rWX, lWX), lastSpawnPos.y + UnityEngine.Random.Range(2, maxDistanceBetweenPlatform));

                    tmp = UnityEngine.Random.Range(0, 1);

                    if (tmp - 1 < 0.5)
                    {
                        platformQueue.Enqueue(Instantiate(normalPlatform, spawnLoc, new Quaternion()));
                    }
                    else
                    {
                        platformQueue.Enqueue(Instantiate(snappyPlatform, spawnLoc, new Quaternion()));
                    }

                    lastSpawnPos = spawnLoc;
                }
            }

            gameStart = false;
        }
        else
        {
            for (int j = 0; j < numPlatform; j++)
            {
                Vector2 spawnLoc = new Vector2(UnityEngine.Random.Range(rWX, lWX), lastSpawnPos.y + UnityEngine.Random.Range(1, maxDistanceBetweenPlatform));

                var tmp = UnityEngine.Random.Range(0, 1);

                if (tmp - 1 < 0.5)
                {
                    platformQueue.Enqueue(Instantiate(normalPlatform, spawnLoc, new Quaternion()));
                }
                else
                {
                    platformQueue.Enqueue(Instantiate(snappyPlatform, spawnLoc, new Quaternion()));
                }
                lastSpawnPos = spawnLoc;
             }
            CheckAndDespawnPlatform();
                
        }
    }

    void CheckAndDespawnPlatform()
    {
        Vector2 pos = platformQueue.Peek().transform.position;

        if(player.transform.position.y - pos.y > maxDistanceBeforeDespawn)
        {
            Destroy(platformQueue.Dequeue());
            CheckAndDespawnPlatform();
            return;
        }

        return;
    }
}
