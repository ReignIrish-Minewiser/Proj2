using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldenPlatformMover : MonoBehaviour
{
    public GameObject target; // The GameObject to move towards
    public float moveSpeed = 2f; // Speed of the movement
    private bool isMoving = false;

    private void Start()
    {
        // Ensure the platform's scale is fixed when it starts
        transform.localScale = Vector3.one; // Ensure the platform size doesn't change
    }

    void Update()
    {
        // If the platform is moving and the target is assigned
        if (isMoving && target != null)
        {
            // Move the platform towards the target's position (use target.transform.position)
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, moveSpeed * Time.deltaTime);

            // If the platform reaches the target, stop moving
            if (Vector2.Distance(transform.position, target.transform.position) < 0.1f)
            {
                isMoving = false;
            }
        }
    }

    // This function is called when another collider enters the trigger collider
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Start moving the platform when the player steps on it
            isMoving = true;
        }
    }

    // Optional: This function can be used to stop the platform if the player leaves
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Stop the platform's movement when the player leaves
            isMoving = false;
        }
    }

    // This is the SetTarget function that allows you to set a target GameObject to move towards
    public void SetTarget(GameObject newTarget)
    {
        if (newTarget != null)
        {
            target = newTarget; // Assign the target GameObject to the platform mover
            isMoving = true; // Start moving the platform to the new target
        }
    }
}
