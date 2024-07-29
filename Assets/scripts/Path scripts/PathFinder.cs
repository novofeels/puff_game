using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    WaypointPath waypointPath;
    private List<Vector2> waypoints;
    private int waypointIndex = 0;
    private Camera mainCamera;
    EnemySpawner enemySpawner;
    private float randomThreshold = 0.1f;

    void Awake(){
        enemySpawner = FindObjectOfType<EnemySpawner>();
       
    }
    void Start()
    {
        waypointPath = enemySpawner.GetCurrentWave();
        mainCamera = Camera.main;
        waypoints = waypointPath.GetWaypoints();
        SetInitialPosition();
        SetRandomThreshold();
    }

    void Update()
    {
        FollowPath();
    }

    void SetInitialPosition()
    {
        Vector3 initialPosition = mainCamera.ViewportToWorldPoint(new Vector3(waypoints[waypointIndex].x, waypoints[waypointIndex].y, mainCamera.nearClipPlane));
        transform.position = new Vector2(initialPosition.x, initialPosition.y);
    }

       void SetRandomThreshold()
    {
        randomThreshold = Random.Range(0.2f, 3f); // Set a random threshold between 0.1 and 1.0
    }

    void FollowPath()
    {
        if (waypointIndex < waypoints.Count)
        {
            Vector3 targetPosition3D = mainCamera.ViewportToWorldPoint(new Vector3(waypoints[waypointIndex].x, waypoints[waypointIndex].y, mainCamera.nearClipPlane));
            Vector2 targetPosition = new Vector2(targetPosition3D.x, targetPosition3D.y);
            float delta = waypointPath.GetMoveSpeed() * Time.deltaTime;

            transform.position = Vector2.MoveTowards(transform.position, targetPosition, delta);
            
            if (Vector2.Distance(transform.position, targetPosition) < randomThreshold)
            {
                SetRandomThreshold();
                waypointIndex++;
                
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
