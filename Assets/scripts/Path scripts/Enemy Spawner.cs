using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaypointPath> waveConfigs;
    [SerializeField] float timeBetweenWaves = 0f;
    private Camera mainCamera;
    WaypointPath currentWave;
    public bool isLooping = true;

    void Start()
    {
        mainCamera = Camera.main;
        StartCoroutine(SpawnEnemyWaves());
    }

    public WaypointPath GetCurrentWave()
    {
        return currentWave;
    }
    IEnumerator SpawnEnemyWaves()
    {
        Vector2 startViewportPosition;
        Vector3 startWorldPosition;
        startWorldPosition.z = 0; // Ensure the z position is zero for 2D game
        do{
        for (int waveIndex = 0; waveIndex < waveConfigs.Count; waveIndex++)
        {
             currentWave = waveConfigs[waveIndex];
            startViewportPosition = currentWave.GetStartingWaypoint();
        startWorldPosition = mainCamera.ViewportToWorldPoint(new Vector3(startViewportPosition.x, startViewportPosition.y, mainCamera.nearClipPlane));
        startWorldPosition.z = 0; // Ensure the z position is zero for 2D game
             
                for (int i = 0; i < currentWave.GetEnemyCount(); i++){
            Instantiate(currentWave.GetEnemyPrefab(i), startWorldPosition, Quaternion.identity, transform);
            yield return new WaitForSeconds(currentWave.GetSpawnRate());
        }
    
            yield return new WaitForSeconds(timeBetweenWaves);
        }} while (isLooping);
    
        
    }
}
