using System.Collections;
using UnityEngine;

public class HitBySpear : MonoBehaviour
{
    public Vector2 knockbackDirection = new Vector2(-1, 0); // Adjust this value as needed
    public float knockbackDistance = 1f; // Adjust this value as needed
    public float knockbackDuration = 0.2f; // Duration of the knockback effect
    
    private Player playerMovement; // Reference to the Player script
    private bool isKnockedBack = false;
    private CameraShake cameraShake;

    void Start()
    {
        playerMovement = GetComponent<Player>();
        cameraShake = Camera.main.GetComponent<CameraShake>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("spear") && !isKnockedBack)
        {
            // Parent the spear to the player
            other.transform.SetParent(transform);

            // Stop the spear's movement
            Rigidbody2D spearRB = other.GetComponent<Rigidbody2D>();
            spearRB.velocity = Vector2.zero;
           
            if (cameraShake != null)
            {
                cameraShake.ShakeCamera();
            }
            // Start the knockback coroutine
            StartCoroutine(ApplyKnockback());
        }
    }

    IEnumerator ApplyKnockback()
    {
        isKnockedBack = true;
        playerMovement.enabled = false; // Disable regular movement

        Vector3 startPosition = transform.position;
        Vector3 endPosition = startPosition + (Vector3)(knockbackDirection.normalized * knockbackDistance);
        float elapsedTime = 0f;

        while (elapsedTime < knockbackDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / knockbackDuration;
            
            // Use easing function for smoother movement
            float easedT = Mathf.Sin(t * Mathf.PI * 0.5f);
            
            transform.position = Vector3.Lerp(startPosition, endPosition, easedT);
            yield return null;
        }

        // Ensure final position is exactly where we want it
        transform.position = endPosition;

        playerMovement.enabled = true; // Re-enable regular movement
        isKnockedBack = false;
    }
}