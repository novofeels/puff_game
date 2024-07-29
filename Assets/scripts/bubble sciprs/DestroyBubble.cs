using UnityEngine;

public class Bubble : MonoBehaviour
{
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void FixedUpdate()
    {
        CheckBounds();
    }

    void CheckBounds()
    {
        Vector3 screenPosition = mainCamera.WorldToViewportPoint(transform.position);
        if (screenPosition.x < 0 || screenPosition.x > 1 || screenPosition.y < 0 || screenPosition.y > 1)
        {
            Destroy(gameObject);
        }
    }
}
