using System.Collections;
using UnityEngine;

public class DiverShooting : MonoBehaviour
{
    public GameObject spear; // Reference to the spear GameObject
    public float minShootTime = 0.5f;
    public float maxShootTime = 1f;
    private bool hasShot = false;

    void Start()
    {
        // Start the coroutine to handle shooting at a random time
        StartCoroutine(ShootAtRandomTime());
    }

    IEnumerator ShootAtRandomTime()
    {
        // Wait for a random amount of time between minShootTime and maxShootTime
        float waitTime = Random.Range(minShootTime, maxShootTime);
        yield return new WaitForSeconds(waitTime);

        // Shoot the spear if it hasn't been shot yet
        if (!hasShot)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // Detach the spear from the diver
        spear.transform.parent = null;

        // Ensure the spear has a Rigidbody2D component
        Rigidbody2D rb = spear.GetComponent<Rigidbody2D>();
   

       
        rb.velocity = new Vector2(-12f, -.05f); // Adjust the force as needed

        // Mark the spear as shot
        hasShot = true;
    }
}
