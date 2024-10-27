using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Manages target behavior, scoring, explosion effect, and removal from the screen
public class TargetX : MonoBehaviour
{
    // References and public variables
    private Rigidbody rb;                       // Rigidbody component for the target
    private GameManagerX gameManagerX;          // Reference to the GameManagerX script
    public int pointValue;                      // Points awarded when target is hit
    public GameObject explosionFx;              // Explosion effect prefab

    // Timing variables
    public float timeOnScreen = 1.0f;           // Time before the target moves forward (indicating missed hit)

    // Game board position settings
    private float minValueX = -3.75f;
    private float minValueY = -3.75f;
    private float spaceBetweenSquares = 2.5f;

    // Initialize target position and removal coroutine
    void Start()
    {
        rb = GetComponent<Rigidbody>();         // Get the Rigidbody component
        gameManagerX = GameObject.Find("Game Manager").GetComponent<GameManagerX>();  // Find GameManagerX instance

        transform.position = RandomSpawnPosition();  // Set random spawn position
        StartCoroutine(RemoveObjectRoutine());       // Start coroutine to remove or move the object after timeOnScreen
    }

    // Triggered when the target is clicked
    private void OnMouseDown()
    {
        if (gameManagerX.isGameActive)              // Only respond if game is active
        {
            Destroy(gameObject);                    // Destroy target object
            gameManagerX.UpdateScore(pointValue);   // Add points to score
            Explode();                              // Trigger explosion effect
        }       
    }

    // Generates a random spawn position on the game board
    Vector3 RandomSpawnPosition()
    {
        float spawnPosX = minValueX + (RandomSquareIndex() * spaceBetweenSquares);  // Calculate random X position
        float spawnPosY = minValueY + (RandomSquareIndex() * spaceBetweenSquares);  // Calculate random Y position
        return new Vector3(spawnPosX, spawnPosY, 0);  // Return spawn position as vector
    }

    // Generate a random index from 0 to 3 for position calculation
    int RandomSquareIndex()
    {
        return Random.Range(0, 4);  // Return a random integer between 0 and 3
    }

    // Detects collision with other objects
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);  // Destroy target on collision

        // If collided with the "Sensor" and target is not "Bad", end the game
        if (other.gameObject.CompareTag("Sensor") && !gameObject.CompareTag("Bad"))
        {
            gameManagerX.GameOver();  // Trigger game over
        } 
    }

    // Instantiates an explosion effect at the target's position
    void Explode()
    {
        Instantiate(explosionFx, transform.position, explosionFx.transform.rotation);  // Create explosion effect
    }

    // Coroutine to handle target removal or movement after appearing for timeOnScreen duration
    IEnumerator RemoveObjectRoutine()
    {
        yield return new WaitForSeconds(timeOnScreen);  // Wait before executing the next steps
        if (gameManagerX.isGameActive)                  // Only proceed if game is active
        {
            transform.Translate(Vector3.forward * 5, Space.World);  // Move target forward (indicating missed hit)
        }
    }
}


