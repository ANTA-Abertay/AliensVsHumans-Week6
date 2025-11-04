using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class PlatformController : MonoBehaviour
{
    // min/max positions for platforms to spawn
    [Header("Platform Positions")]
    [Range(-100, 100)] public float xMax = 100;

    // min/max number of levels that platforms should spawn on
    [Header("Platform Levels")]
    [Range(1, 10)] public int minLevels = 3;
    [Range(1, 10)] public int maxLevels = 5;
    [Range(0, 100)] public int levelSpacing = 50;

    // min/max number of platforms per level
    [Header("Platform Count")]
    [Range(1, 3)] public int minPlatforms = 1;
    [Range(1, 3)] public int maxPlatforms = 2;
    
    // Gizmos z width. Not used in actual generator
    [Header("Gizmos")]
    [Range(1, 10)] public int gizmosZWidth = 4;

    void Start()
    {

    }

    // draw gizmo in editor for clarity
    void OnDrawGizmosSelected()
    {
        // platform spawn bounding box
        // var bbCenter = transform.position + new Vector3(xMax / 2, (maxLevels * levelSpacing) / 2.0f, 0);
        // var bbSize = new Vector3(xMax, maxLevels * levelSpacing, gizmosZWidth);
        // Gizmos.color = Color.blue;
        // Gizmos.DrawWireCube(bbCenter, bbSize);
        
        // draw notches for minimum levels
        for (var i = 1; i <= maxLevels; i++)
        {
            var levelCenter = transform.position + new Vector3(xMax / 2, i * levelSpacing, 0);
            var levelSize = new Vector3(xMax, 0.1f, gizmosZWidth);
            Gizmos.color = i > minLevels ? Color.orange : Color.orangeRed;
            Gizmos.DrawWireCube(levelCenter, levelSize);
        }
    }
}
    
    
