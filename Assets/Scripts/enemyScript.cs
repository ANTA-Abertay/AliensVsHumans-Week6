using UnityEngine;
using UnityEngine.AI;
public class enemyScript : MonoBehaviour
{
    NavMeshAgent enemy;
    GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemy = GetComponent<NavMeshAgent>();
        
    }
    

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindWithTag("Player");
        enemy.SetDestination(player.transform.position);
    }
}
