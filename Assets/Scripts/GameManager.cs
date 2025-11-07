using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class GameManager : MonoBehaviour
{
    // --- Public --- //
    
    // makes game manager instance
    public static GameManager Instance;
    
    // the platform prefab to create
    [Header("Platforms")]
    public GameObject platformPrefab;
    
    // min/max positions for platforms to spawn
    [Space]
    [Range(-10, 10)] public float zLock = 0;
    [Range(-10, 10)] public float levelXSpacing = 0;
    [Range(0, 10)] public float levelYOffset = 0;
    [Range(0, 10)] public int levelSpacing = 5;

    // min/max number of platforms per level
    [Space]
    [Range(1, 10)] public int minPlatforms = 1;
    [Range(1, 10)] public int maxPlatforms = 2;
    [Range(0, 1)] public float platformSpawnProbability = 0.5f;
    
    [Header("Debug")]
    [Range(0, 10)] public int debugCurrentLevel = 1;
    
    // --- Private --- //
    
    //gets the enemy count
    // private int _enemiesCount = EnemyManager.Instance.Count;
    
    // the current level
    private int _currentLevel = 1;
    
    // the positions of all platforms
    private List<List<Vector3>> _platforms;
    
    // platform raycast detect mask
    private LayerMask layerMask;

    void Awake()
    {
        // make singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // if there is an instance, and it is this one, don't delete
        }
        else
        {
            Destroy(gameObject); // Only one manager exists
        }
        
        // create platform raycast detect mask
        layerMask = LayerMask.GetMask("Terrain", "Player", "Bullet", "Enemy");
    }

    void Start()
    {
        _GeneratePlatforms();
    }

    private void Update()
    {
        // if(_enemiesCount <= 0)
        // {
        //     //draw.level[_counter] complete;
        //     _currentLevel = +1;
        //     //call function that has the switch board of the platform placement 
        //     // spawn enemies 
        //     // draw level [_counter]
        // }
    }
    
    private void _GeneratePlatforms()
    {
        /*  TODO:
         *  [ ] generate base platforms based on level number
         *      [ ] calculate possible x positions for platforms
         *      [ ] for every platform level:
         *          [ ] calculate valid platform positions
        */
        
        if (debugCurrentLevel > 0)
            _currentLevel = debugCurrentLevel;
        
        // calculate all possible x positions for the platforms
        var platColsCount = _currentLevel * 2 + 1;
        var platColsX = new List<float>();
        for (var i = 0; i < platColsCount; i++)
        {
            //platColsX.Add(xMax / platColsCount * i);
            platColsX.Add(-(levelXSpacing * i));
        }
        
        // clear all platforms (if any exist)
        _platforms = new List<List<Vector3>>();

        // generate the bottom layer of platforms
        for (var level = 0; level < _currentLevel; level++)
        {
            _platforms.Add(new List<Vector3>());

            while (_platforms[level].Count < minPlatforms)
            {
                for (var colIndex = 0; colIndex < platColsX.Count; colIndex++)
                {
                    // stop if we have reached max platforms
                    if (_platforms[level].Count >= maxPlatforms)
                        break;

                    // decide randomly if a platform should be spawned. if not? continue!
                    if (!(Random.value < platformSpawnProbability)) continue;
                    
                    // calculate position for platform
                    var pos = new Vector3(platColsX[colIndex], levelSpacing * level + levelYOffset, zLock);
                    
                    // a platform should not be spawned if there is one directly below
                    // RaycastHit hit;
                    // if (Physics.Raycast(pos, transform.TransformDirection(Vector3.down), out hit, levelSpacing * 1.5f, layerMask))
                    //     continue;
                    //
                    // Debug.DrawRay(pos, Vector3.down * levelSpacing * 1.5f, Color.white, 100);
                        
                    // remember the position of this platform
                    _platforms[level].Add(pos);

                    // spawn the platform
                    Instantiate(platformPrefab, pos, Quaternion.Euler(Vector3.zero));
                }
            }
        }
    }
}
