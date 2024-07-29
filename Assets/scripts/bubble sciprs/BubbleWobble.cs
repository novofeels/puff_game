using System.Collections;
using UnityEngine;

public class BubbleWobble : MonoBehaviour
{
    private Rigidbody2D rb;
    public float wobbleAmplitude = 0.5f; // The height of the wobble
    public float wobbleFrequency = 2f; // The speed of the wobble

    private float initialY; // Initial Y position of the bubble
    private float timeCounter = 0f; // Counter to keep track of time
    private BubbleShooter bubbleShooter;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bubbleShooter = FindObjectOfType<BubbleShooter>(); // Find the BubbleShooter component
        StartCoroutine(SetInitialYAfterDelay());
    }

    IEnumerator SetInitialYAfterDelay()
    {
        yield return new WaitForSeconds(bubbleShooter.delay); // Use the delay from BubbleShooter
        initialY = transform.position.y; // Set initialY after the delay
        enabled = true; // Enable the script to start the wobble
    }

    void FixedUpdate()
    {
        if (enabled)
        {
            ApplyWobble();
        }
    }

    void ApplyWobble()
    {
        // Increment the time counter using deltaTime
        timeCounter += Time.deltaTime;

        // Calculate the new Y position using a sine wave and the time counter
        float newY = initialY + Mathf.Sin(timeCounter * wobbleFrequency) * wobbleAmplitude;

        // Apply the new Y position while keeping the X position
        rb.position = new Vector2(rb.position.x, newY);
    }
}
