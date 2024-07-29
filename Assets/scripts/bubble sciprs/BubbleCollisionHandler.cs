using System.Collections;
using UnityEngine;

public class BubbleCollisionHandler : MonoBehaviour
{
    public ParticleSystem bubbleExpandEffect; // Particle system to instantiate
    public GameObject trappedDiverPrefab; // Prefab with the diver trapped in a bubble
    AudioPlayer audioPlayer;
    ScoreKeeper scoreKeeper;
    private int newScore;

    void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Diver"))
        {
            // Instantiate the particle system at the diver's position
            Instantiate(bubbleExpandEffect, other.transform.position, Quaternion.identity);

            audioPlayer.PlayBubbleImpact();
            // Instantiate the trapped diver in a bubble at the diver's position
            GameObject trappedDiver = Instantiate(trappedDiverPrefab, other.transform.position, Quaternion.identity);

            // Destroy the diver object
            Destroy(other.gameObject);

            // Destroy the bubble object
            Destroy(gameObject);

            trappedDiver.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 2.5f);

            scoreKeeper.ModifyScore(50);
        
          

            // Start moving the trapped diver upwards
           
        }
    }


}
