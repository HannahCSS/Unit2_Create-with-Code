using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Automatically destroys the GameObject this script is attached to after a set duration
public class DestroyObjectX : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 5);  // Destroy this GameObject after 5 seconds
    }
}

