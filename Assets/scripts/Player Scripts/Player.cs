using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    Vector2 rawInput;
    [SerializeField] float moveSpeed = 0.52f;

    Vector2 minBounds;
    Vector2 maxBounds;
    Camera mainCamera;
    Vector3 previousCameraPosition;
    [SerializeField] float padding = 0.5f;

    void Start()
    {
        mainCamera = Camera.main;
        InitBounds();
        previousCameraPosition = mainCamera.transform.position;
    }

    void Update()
    {
        // Check if the camera position has changed
        if(!enabled) return;
        if (mainCamera.transform.position != previousCameraPosition)
        {
            InitBounds();
            previousCameraPosition = mainCamera.transform.position;
        }
        Move();
    }

    void InitBounds()
    {
        minBounds = mainCamera.ViewportToWorldPoint(new Vector2(0, 0));
        maxBounds = mainCamera.ViewportToWorldPoint(new Vector2(1, 1));
    }

    void Move()
    {
        Vector2 delta = rawInput * moveSpeed * Time.deltaTime;
        Vector2 newPos = new Vector2();
        newPos.x = Mathf.Clamp(transform.position.x + delta.x, minBounds.x + padding, maxBounds.x - padding);
        newPos.y = Mathf.Clamp(transform.position.y + delta.y, minBounds.y + padding, maxBounds.y - padding);
        transform.position = newPos;
    }

    void OnMove(InputValue value)
    {
        rawInput = value.Get<Vector2>();
    }
}
