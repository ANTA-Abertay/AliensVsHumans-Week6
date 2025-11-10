using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;
    public List<GameObject> _enemies = new List<GameObject>();

    void Awake()
    {
        // make a singleton
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

    public void Register(GameObject enemy) //adds enemies
    {
        _enemies.Add(enemy);
    }

    public void Unregister(GameObject enemy)//deletes enemies
    {
        _enemies.Remove(enemy);
    }

    public int Count => _enemies.Count;//counts enemies
}