using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class GameManager : MonoBehaviour
{
    // --- Public --- //
    
    // makes game manager instance
    public static GameManager Instance;
    
    // min/max positions for platforms to spawn
    [Header("Platform Positions")]
    [Range(-100, 100)] public float xMax = 100;
    [Range(0, 20)] public int levelSpacing = 10;

    // min/max number of platforms per level
    [Header("Platform Count")]
    [Range(1, 10)] public int minPlatforms = 1;
    [Range(1, 10)] public int maxPlatforms = 2;
    
    // Gizmos z width. Not used in actual generator
    [Header("Gizmos")]
    [Range(1, 10)] public int gizmosZWidth = 4;
    
    // --- Private --- //
    
    //gets the enemy count
    private readonly int _enemiesCount = EnemyManager.Instance.Count;
    
    // the current level
    private int _currentLevel = 1;
    
    // the positions of all platforms
    private Vector<Vector<Vector3>> _platforms;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // if there is an instance, and it is this one, don't delete
        }
        else
        {
            Destroy(gameObject); // Only one manager exists
        }
    }

    private void Update()
    {
        if(_enemiesCount <= 0)
        {
            //draw.level[_counter] complete;
            _currentLevel = +1;
            //call function that has the switch board of the platform placement 
            // spawn enemies 
            // draw level [_counter]
        }
    }
    
    private void _GeneratePlatforms()
    {
        /* TODO:
         * - generate base platforms based on level number
         * - calculate possible x positions for platforms
         * - for every platform level
         * -
         */
    }
}
