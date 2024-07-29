using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWaypointPath", menuName = "ScriptableObjects/WaypointPath", order = 1)]
public class WaypointPath : ScriptableObject
{
    [SerializeField] List<GameObject> enemyPrefabs; // List of enemy prefabs that will follow this path
    public List<Vector2> viewportWaypoints; // List of waypoints in viewport coordinates
    public float moveSpeed = 12f; // Speed at which the object moves along the path
    [SerializeField] float spawnRate = 1f; // Rate at which enemies spawn
    [SerializeField] float spawnVariance = 0.5f; // Variance in spawn rate
    [SerializeField] float minimumSpawnRate = 0.2f; // Minimum spawn rate


    public int GetEnemyCount() { return enemyPrefabs.Count; }

    public GameObject GetEnemyPrefab(int index)
    {
        if (index < enemyPrefabs.Count)
        {
            return enemyPrefabs[index];
        }
        return null;
    }
    
    
    public float GetMoveSpeed() { return moveSpeed; }
    public List<Vector2> GetWaypoints() { return viewportWaypoints; }   

    public Vector2 GetStartingWaypoint()
    {
        return viewportWaypoints[0];
    }

    public float GetSpawnRate()
    {
        float spawnTime = Random.Range(spawnRate - spawnVariance, spawnRate + spawnVariance);
        return spawnTime < minimumSpawnRate ? minimumSpawnRate : spawnTime;
    }
}
