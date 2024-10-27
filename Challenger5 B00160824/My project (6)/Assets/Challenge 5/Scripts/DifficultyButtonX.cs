using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Manages difficulty button behavior for starting the game at selected difficulty
public class DifficultyButtonX : MonoBehaviour
{
    private Button button;                     // Button component of this GameObject
    private GameManagerX gameManagerX;         // Reference to the GameManagerX script
    public int difficulty;                     // Stores difficulty level (1 = Easy, 2 = Medium, 3 = Hard)

    // Initialize references and button listener
    void Start()
    {
        gameManagerX = GameObject.Find("Game Manager").GetComponent<GameManagerX>();  // Find GameManagerX instance
        button = GetComponent<Button>();  // Get Button component attached to this GameObject
        button.onClick.AddListener(SetDifficulty);  // Add SetDifficulty as a listener to the button click event
    }

    // Called when a difficulty button is clicked, starting the game at selected difficulty
    void SetDifficulty()
    {
        Debug.Log(button.gameObject.name + " was clicked");  // Log button name when clicked for debugging
        gameManagerX.StartGame(difficulty);  // Pass difficulty level to StartGame in GameManagerX
    }
}


