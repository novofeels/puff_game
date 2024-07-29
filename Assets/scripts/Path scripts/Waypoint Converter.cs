using UnityEngine;
using System.Collections.Generic;

public class WaypointConverter : MonoBehaviour
{
    public Camera mainCamera;
    public List<PathData> paths;

    void Start()
    {
        foreach (PathData path in paths)
        {
            path.waypointPath.viewportWaypoints = new List<Vector2>();

            foreach (Transform waypoint in path.parentPath)
            {
                Vector2 viewportPosition = mainCamera.WorldToViewportPoint(waypoint.position);
                path.waypointPath.viewportWaypoints.Add(new Vector2(viewportPosition.x, viewportPosition.y));
                Debug.Log($"Waypoint {waypoint.name} Viewport Position: {viewportPosition}");
            }

#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(path.waypointPath);
            UnityEditor.AssetDatabase.SaveAssets();
#endif
        }
    }
}

[System.Serializable]
public class PathData
{
    public Transform parentPath; // Reference to the parent path GameObject
    public WaypointPath waypointPath; // Reference to the ScriptableObject
}
