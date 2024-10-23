using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeftX : MonoBehaviour
{
    public float speed = 5f;  // Speed of the object moving left
    private PlayerControllerX playerControllerScript;
    
    [SerializeField] private float leftBound = -10f;  // Boundary for destroying objects

    // Start is called before the first frame update
    void Start()
    {
        // Safely find the PlayerControllerX script from the Player GameObject
        GameObject player = GameObject.Find("Player");

        if (player != null)
        {
            playerControllerScript = player.GetComponent<PlayerControllerX>();
        }
        else
        {
            Debug.LogError("Player object not found in the scene. Please check.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Move object left if game is not over
        if (playerControllerScript != null && !playerControllerScript.gameOver)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);
        }

        // Destroy the object if it passes the left boundary (except if it's the background)
        if (transform.position.x < leftBound && !gameObject.CompareTag("Background"))
        {
            Destroy(gameObject);
        }
    }
}
