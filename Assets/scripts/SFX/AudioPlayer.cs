using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [Header("BubbleShooting")]
    [SerializeField] AudioClip bubbleShootSFX;
    [SerializeField] [Range(0, 1)] float bubbleShootVolume = 0.5f;

    [Header("BubbleImpacts")]
    [SerializeField] AudioClip bubbleImpactSFX;
    [SerializeField] [Range(0, 1)] float bubbleImpactVolume = 0.5f;

    static AudioPlayer instance;

    void Awake() {
        ManageSingleton();
    }

    void ManageSingleton()
    {
       if(instance != null)
       {
            gameObject.SetActive(false);
           Destroy(gameObject);
       }
       else
       {
            instance = this;
           DontDestroyOnLoad(gameObject);
       }
    }


    public void PlayShootingClip()
        {
            if(bubbleShootSFX != null)
            {
                AudioSource.PlayClipAtPoint(bubbleShootSFX, Camera.main.transform.position, bubbleShootVolume);
            }

        }
    
    public void PlayBubbleImpact()
        {
            if(bubbleImpactSFX != null)
            {
                AudioSource.PlayClipAtPoint(bubbleImpactSFX, Camera.main.transform.position, bubbleImpactVolume);
            }
        }
}
