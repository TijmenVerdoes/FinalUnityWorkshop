using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour
{
    public Vector2 startPosition; // Start position of the platform
    public Vector2 endPosition; // End position of the platform
    public float speed = 1.0f; // Speed at which the platform moves
    public float pauseDuration = 1.0f; // Duration of the pause at each end

    private Vector2 currentTarget;
    private bool isMoving = true;
    private float pauseTimer = 0f;

    private void Start()
    {
        transform.position = startPosition; // Initialize position
        currentTarget = endPosition; // Set the initial target
    }

    private void Update()
    {
        if (isMoving)
        {
            MovePlatform();
        }
        else
        {
            PauseMovement();
        }
    }

    private void MovePlatform()
    {
        // Move the platform towards the current target
        transform.position = Vector2.MoveTowards(transform.position, currentTarget, speed * Time.deltaTime);

        // Check if the platform has reached the target
        if (Vector2.Distance(transform.position, currentTarget) < 0.001f)
        {
            isMoving = false; // Stop moving
            pauseTimer = pauseDuration; // Reset pause timer
            // Switch target
            currentTarget = (currentTarget == endPosition) ? startPosition : endPosition;
        }
    }

    private void PauseMovement()
    {
        pauseTimer -= Time.deltaTime; // Decrement the pause timer

        if (pauseTimer <= 0f)
        {
            isMoving = true; // Resume movement after pausing
        }
    }
}