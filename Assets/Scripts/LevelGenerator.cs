using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class LevelGenerator : MonoBehaviour
{
    // --- Public --- //
    
    // min/max positions for platforms to spawn
    [Header("Platform Positions")]
    [Range(-100, 100)] public float xMax = 100;

    // min/max number of levels that platforms should spawn on
    [Header("Platform Levels")]
    [Range(1, 10)] public int minLevels = 3;
    [Range(1, 10)] public int maxLevels = 5;
    [Range(0, 20)] public int levelSpacing = 10;

    // min/max number of platforms per level
    [Header("Platform Count")]
    [Range(1, 10)] public int minPlatforms = 1;
    [Range(1, 10)] public int maxPlatforms = 2;
    
    // Gizmos z width. Not used in actual generator
    [Header("Gizmos")]
    [Range(1, 10)] public int gizmosZWidth = 4;
    
    // --- Private --- //

    private Vector<Vector<Vector3>> _platforms;

    void Start()
    {

    }

    void _GeneratePlatforms()
    {
        
    }

    // draw gizmo in editor for clarity
    void OnDrawGizmosSelected()
    {
        // draw horizontal planes for minimum levels
        for (var i = 1; i <= maxLevels; i++)
        {
            var levelCenter = transform.position + new Vector3(xMax / 2, i * levelSpacing, 0);
            var levelSize = new Vector3(xMax, 0.1f, gizmosZWidth);
            
            Gizmos.color = i > minLevels ? Color.orange : Color.orangeRed;
            Gizmos.DrawWireCube(levelCenter, levelSize);
        }
        
        // draw vertical planes for platform numbers
        for (var i = 1; i <= maxPlatforms; i++)
        {
            var platformCenter = transform.position + new Vector3(xMax / (maxPlatforms+1) * i, maxLevels * levelSpacing / 2.0f, 0);
            var platformSize = new Vector3(0.1f, maxLevels * levelSpacing, gizmosZWidth);
            
            Gizmos.color = i > minPlatforms ? Color.greenYellow: Color.green;
            Gizmos.DrawWireCube(platformCenter, platformSize);
        }
    }
}
