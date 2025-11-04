using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    NavMeshAgent _enemy;
    GameObject _player;
    int health = 10;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _enemy = GetComponent<NavMeshAgent>();

    }


    // Update is called once per frame
    void Update()
    {
        _player = GameObject.FindWithTag("Player");
        _enemy.SetDestination(_player.transform.position);
        var posDif = (_enemy.transform.position - _player.transform.position);
        if (posDif.magnitude < 2)
        {
            //deal damage 
            GameObject.Find("Player").GetComponent<PlayerController>().health -= 2;

        }
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag("Bullet"))
        {
            health = health - 2;

        }
    }
}
