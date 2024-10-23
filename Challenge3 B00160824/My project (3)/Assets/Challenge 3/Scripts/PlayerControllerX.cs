using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public bool gameOver;  // Tracks if the game is over
    public float floatForce;  // Force applied when floating
    private float gravityModifier = 1.5f;  // Gravity modifier
    private Rigidbody playerRb;  // Reference to Rigidbody

    public ParticleSystem explosionParticle;  // Particle system for explosion
    public ParticleSystem fireworksParticle;  // Particle system for fireworks

    private AudioSource playerAudio;  // Audio source for sound effects
    public AudioClip moneySound;  // Sound when collecting money
    public AudioClip explodeSound;  // Sound when exploding
    public AudioClip bounceSound;  // Sound when bouncing off the ground
	
    private bool isLowEnough = true;  // Ensures player can't go too high

    // Start is called before the first frame update
    void Start()
    {
        // Get Rigidbody and AudioSource components
        playerRb = GetComponent<Rigidbody>();
        playerAudio = GetComponent<AudioSource>();

        // Modify gravity only once, by setting it directly
        Physics.gravity = new Vector3(0, -9.81f * gravityModifier, 0);

        // Apply a small upward force at the start of the game
        playerRb.AddForce(Vector3.up * 5, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        // Allow floating if the player is low enough and the game is not over
        if (Input.GetKey(KeyCode.Space) && isLowEnough && !gameOver)
        {
            playerRb.AddForce(Vector3.up * floatForce);
        }

        // Check if the player is too high
        if (transform.position.y > 13)
        {
            isLowEnough = false;  // Stop floating if player goes too high
            playerRb.velocity = new Vector3(playerRb.velocity.x, 0, playerRb.velocity.z);  // Reset vertical speed
        }
        else
        {
            isLowEnough = true;  // Allow floating if player is within bounds
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        // If the player collides with a bomb, trigger explosion and end the game
        if (other.gameObject.CompareTag("Bomb"))
        {
            explosionParticle.Play();
            playerAudio.PlayOneShot(explodeSound, 1.0f);
            gameOver = true;
            Debug.Log("Game Over!");
            Destroy(other.gameObject);  // Destroy the bomb
        }
        // If the player collides with money, trigger fireworks
        else if (other.gameObject.CompareTag("Money"))
        {
            fireworksParticle.Play();
            playerAudio.PlayOneShot(moneySound, 1.0f);
            Destroy(other.gameObject);  // Destroy the money
        }
        // If the player hits the ground, bounce them back up (unless game is over)
        else if (other.gameObject.CompareTag("Ground") && !gameOver)
        {
            playerRb.AddForce(Vector3.up * 10, ForceMode.Impulse);  // Bounce upwards
            playerAudio.PlayOneShot(bounceSound, 1.5f);  // Play bounce sound
        }
    }
}
