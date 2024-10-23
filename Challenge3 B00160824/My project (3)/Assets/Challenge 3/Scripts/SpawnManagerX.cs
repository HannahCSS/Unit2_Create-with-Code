using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerX : MonoBehaviour
{
    public GameObject[] objectPrefabs;  // Array to store the different objects to spawn
    private float spawnDelay = 2.0f;    // Initial delay before spawning starts
    private float spawnInterval = 1.5f; // Time between spawns

    private PlayerControllerX playerControllerScript;  // Reference to player controller

    // Start is called before the first frame update
    void Start()
    {
        // Start spawning objects at regular intervals
        InvokeRepeating("SpawnObject", spawnDelay, spawnInterval);

        // Find and reference the PlayerControllerX script on the Player GameObject
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerControllerX>();
    }

    // Method to spawn objects at random intervals
    void SpawnObject()
    {
        // If the game is still active, spawn new objects
        if (!playerControllerScript.gameOver)
        {
            // Set a random spawn location on the x-axis and y-axis
            Vector3 spawnLocation = new Vector3(30, Random.Range(5, 15), 0);

            // Select a random object from the array of prefabs
            int index = Random.Range(0, objectPrefabs.Length);

            // Spawn the randomly chosen object at the spawn location with the correct rotation
            Instantiate(objectPrefabs[index], spawnLocation, objectPrefabs[index].transform.rotation);
        }
    }
}
