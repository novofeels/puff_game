using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class BubbleShooter : MonoBehaviour
{
    public GameObject bubblePrefab;
    public Transform mouth;
    public float delay = 0.5f; // Delay before applying velocity
    public float bubbleSpeed = 5f; // Speed of the bubble
    AudioPlayer audioPlayer;

 void Awake() {
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    void OnFire()
    {
       ShootBubble();
        
    }

    void ShootBubble()
    {
        // Instantiate the bubble at the mouth position and with the same rotation as Puff
        GameObject bubble = Instantiate(bubblePrefab, mouth.position, mouth.rotation);
        bubble.transform.parent = mouth; // Parent the bubble to the mouth

        // Start the coroutine to add velocity after a delay
        StartCoroutine(AddVelocityAfterDelay(bubble));
    }

    IEnumerator AddVelocityAfterDelay(GameObject bubble)
    {
        yield return new WaitForSeconds(delay);

        bubble.transform.parent = null; // Unparent the bubble

        // Add velocity to the bubble to make it fly across the screen
        Rigidbody2D rb = bubble.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            audioPlayer.PlayShootingClip();
            rb.velocity = new Vector2(bubbleSpeed, 0f); // Adjust the speed as needed
        }
    }
}
