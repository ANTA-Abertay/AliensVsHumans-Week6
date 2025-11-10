using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class GameManager : MonoBehaviour
{
    // --- Public --- //
    
    // makes game manager instance
    public static GameManager Instance;
    
    // the current level
    public int currentLevel = 1;
    
    // platforms!
    [Header("Platforms")]
    
    // the platform prefab to spawn
    public GameObject platformPrefab;
    
    // min/max positions for platforms to spawn
    [Space]
    [Range(-10, 10)] public float zLock;
    [Range(-10, 10)] public float levelXSpacing;
    [Range(0, 10)] public float levelYOffset;
    [Range(0, 10)] public float levelSpacing = 5;

    // min/max number of platforms per level
    [Space]
    [Range(1, 10)] public int minPlatforms = 1;
    [Range(1, 10)] public int maxPlatforms = 2;
    [Range(0, 1)] public float platformSpawnProbability = 0.5f;
    
    // enemy spawning
    [Header("Enemies")]
    
    // the enemy prefab to spawn
    public GameObject enemyPrefab;
    
    // enemy spawn parameters
    [Space]
    [Range(0, 2)] public float enemyYOffset;
    [Range(0, 1)] public float enemySpawnProbability = 0.5f;
    // --- Private --- //
    
    // the positions of all platforms
    private List<List<GameObject>> _platforms;
    
    private GameObject _player;
    
    private NavMeshSurface _surface;

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

        _platforms = new List<List<GameObject>>();
        _GeneratePlatforms();
    }

    private void Start()
    {
        // find and rebuild the navmesh
        _surface = GameObject.Find("NavMesh").GetComponent<NavMeshSurface>();
        _surface.BuildNavMesh();
        
        // spawn the enemies
        _SpawnEnemies();
        
        // find the players
        _player = GameObject.Find("Player");
    }

    private void Update()
    {
        if(EnemyManager.Instance.Count <= 0)
        { 
            currentLevel += 1;
            _ResetLevel();
        }
    }
    
    private void _GeneratePlatforms()
    {
        // calculate all possible x positions for the platforms
        var platColsCount = currentLevel * 2 + 1;
        var platColsX = new List<float>();
        for (var i = 0; i < platColsCount; i++)
        {
            //platColsX.Add(xMax / platColsCount * i);
            platColsX.Add(-(levelXSpacing * i));
        }

        // generate the bottom layer of platforms
        for (var level = 0; level < currentLevel; level++)
        {
            _platforms.Add(new List<GameObject>());

            while (_platforms[level].Count < minPlatforms)
            {
                for (var colIndex = 0; colIndex < platColsX.Count; colIndex++)
                {
                    // stop the loop if we have reached max platforms
                    if (_platforms[level].Count >= maxPlatforms) break;

                    // decide randomly if a platform should be spawned. if not? continue the loop!
                    if (!(Random.value < platformSpawnProbability)) continue;

                    // calculate position for platform
                    var pos = new Vector3(platColsX[colIndex], levelSpacing * level + levelYOffset, zLock);
  
                    // spawn the platform
                    var platform = Instantiate(platformPrefab, pos, Quaternion.Euler(Vector3.zero));
                    
                    // remember the position of this platform
                    _platforms[level].Add(platform);
                }
            }
        }
    }

    private void _SpawnEnemies()
    {
        // for every level
        foreach (var level in _platforms)
        {
            // for every platform
            foreach (var platform in level)
            {
                // decide randomly if an enemy should be spawned. if not? continue the loop!
                if (!(Random.value < enemySpawnProbability)) continue;
                
                // calculate the position to spawn the enemy
                var platPos =  platform.transform.position;
                var pos = new Vector3(platPos.x, platPos.y + enemyYOffset, platPos.z);
                
                // spawn the enemy
                var enemy = Instantiate(enemyPrefab, pos, Quaternion.Euler(Vector3.zero));
                
                EnemyManager.Instance.Register(enemy);
            }
        }
    }
    
    private void _DestroyPlatforms()
    {
        // delete the platforms
        foreach (var level in _platforms)
        {
            foreach (var platform in level)
            {
                Destroy(platform);
            }
        }
        
        // clear platform list
        _platforms.Clear();
    }

    private void _ResetLevel()
    {
        // clear the platforms
        _DestroyPlatforms();
        
        // generate platforms
        _GeneratePlatforms();
        
        // build the navmesh with the platforms and then spawn the enemies
        _surface.BuildNavMesh();
        _SpawnEnemies();
        
        // bring the player back to the spawn
        _player.transform.position = Vector3.zero;
    }
}
