using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Game manager for handling gameplay, scoring, timer, and game state
public class GameManagerX : MonoBehaviour
{
    // UI Elements
    public TextMeshProUGUI scoreText;           // Text to display the score
    public TextMeshProUGUI timeText;            // Text to display the remaining time
    public TextMeshProUGUI gameOverText;        // Text to display when game is over
    public GameObject titleScreen;              // Title screen object to show before game starts
    public Button restartButton;                // Button to restart the game

    // List of target prefabs to spawn
    public List<GameObject> targetPrefabs;

    // Game variables
    private int score;                          // Player's score
    private float spawnRate = 1.5f;             // Rate at which targets spawn
    public bool isGameActive;                   // Tracks if the game is currently active

    // Timer variables
    public float timeValue;                     // Timer value for countdown
    private float timeRemaining = 60;           // Initial countdown time (60 seconds)

    // Game board position settings
    private float spaceBetweenSquares = 2.5f;
    private float minValueX = -3.75f;
    private float minValueY = -3.75f;

    // Start the game, adjust spawnRate based on difficulty, reset score, and hide the title screen
    public void StartGame(int difficulty)
    {
        spawnRate /= difficulty;               // Adjust spawn rate based on difficulty level
        isGameActive = true;                   // Set game as active
        StartCoroutine(SpawnTarget());         // Start spawning targets
        score = 0;                             // Reset score
        timeValue = 60;                        // Reset timer to 60 seconds
        UpdateScore(0);                        // Update score display
        titleScreen.SetActive(false);          // Hide the title screen
    }

    // Update is called once per frame for managing the countdown timer and checking game-over condition
    void Update()
    {
        if (isGameActive)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;  // Decrease remaining time

                // Update time display if timeText is not null
                if (timeText != null)
                {
                    timeText.text = "Time: " + Mathf.Round(timeRemaining);  // Display time as whole number
                }
                else
                {
                    Debug.LogWarning("timeText is not assigned in the Inspector!");
                }
            }
            else
            {
                GameOver();  // Trigger game over when time runs out
            }
        }
    }

    // Spawn targets while the game is active
    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);  // Wait before spawning the next target
            int index = Random.Range(0, targetPrefabs.Count);  // Get random target index

            if (isGameActive)
            {
                Instantiate(targetPrefabs[index], RandomSpawnPosition(), targetPrefabs[index].transform.rotation);  // Instantiate target at random position
            }
        }
    }

    // Generate a random spawn position on the game board
    Vector3 RandomSpawnPosition()
    {
        float spawnPosX = minValueX + (RandomSquareIndex() * spaceBetweenSquares);  // Calculate random X position
        float spawnPosY = minValueY + (RandomSquareIndex() * spaceBetweenSquares);  // Calculate random Y position
        return new Vector3(spawnPosX, spawnPosY, 0);  // Return spawn position vector
    }

    // Generate a random square index from 0 to 3
    int RandomSquareIndex()
    {
        return Random.Range(0, 4);  // Return a random integer between 0 and 3
    }

    // Update the score and display it
    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;                            // Add to current score
        scoreText.text = "Score: " + score;             // Display the updated score
    }

    // Secondary update function for timer logic
    public void update()
    {
        if (isGameActive == true)
        {
            TimeLeft();  // Call TimeLeft() to handle time decrement
        }
        if (timeValue < 0)
        {
            GameOver();  // End game if time runs out
        }
    }

    // Handles decrementing the timer value and updating timeText
    public void TimeLeft()
    {
        timeValue -= Time.deltaTime;                     // Decrease time
        timeText.text = "Time: " + Mathf.Round(timeValue);  // Display remaining time as a whole number
    }

    // End the game, show the game-over screen and restart button
    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);         // Show game-over text
        restartButton.gameObject.SetActive(true);        // Show restart button
        isGameActive = false;                            // Set game as inactive
    }

    // Restart the game by reloading the current scene
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);  // Reload the active scene
    }
}

