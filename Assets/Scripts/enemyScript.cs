using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    NavMeshAgent _enemy;
    GameObject _player;
    public int health = 10;
    private Vector3 _oldPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _enemy = GetComponent<NavMeshAgent>();
        _player = GameObject.FindWithTag("Player");
        _oldPos = gameObject.transform.position;
    }

    // void OnEnable()
    // {
    //     EnemyManager.Instance.Register(gameObject);
    // }
    //
    // void OnDisable()
    // {
    //     EnemyManager.Instance.Unregister(gameObject);
    // }
    
    
    // Update is called once per frame
    void Update()
    {
        
        _enemy.SetDestination(_player.transform.position);
        
        var posDif = (_enemy.transform.position - _player.transform.position);
        if (posDif.magnitude < 5)
        {
            //deal damage 
            GameObject.Find("Player").GetComponent<PlayerController>().health -= 2;

        }

        
        
        
        Vector3 newPos = transform.position; // gets current position 

        if ((newPos.x - _oldPos.x) != 0) // checks if positions are different
        {
            GetComponent<Animation>().Play("walk"); // plays the walk animation
        }

        _oldPos = newPos; // updates old position 
        
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag("Bullet"))
        {
            health = health - 2;

        }
        if (health <= 0)
        {
            Destroy(gameObject);
        }        
    }
}


   
