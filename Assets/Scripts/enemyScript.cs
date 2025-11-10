using UnityEngine;
using UnityEngine.AI;


public class EnemyScript : MonoBehaviour
{
    NavMeshAgent _enemy;
    GameObject _player;
    public int health = 10;
    private Vector3 _oldPos;
    private float _timer;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // get components
        _enemy = GetComponent<NavMeshAgent>();
        _player = GameObject.FindWithTag("Player");
        
        // set its position
        _oldPos = gameObject.transform.position;
    }
    
    void OnDisable()
    {
        EnemyManager.Instance.Unregister(gameObject); //no longer counted
    }
    
    
    // Update is called once per frame
    void Update()
    {
        _timer -= Time.deltaTime; // set up timer
        _enemy.SetDestination(_player.transform.position); // set enemies target to player
        
        var posDif = (_enemy.transform.position - _player.transform.position); // gets distance between enemy and player
        if (posDif.magnitude < 0.5) // if the enemy is withing range to attack
        {
            if(_timer <= 0) // if cooldown finished
            {
                GameObject.Find("Player").GetComponent<PlayerController>().health -= 1; //deal damage 
                _timer = 3; // reset cooldown
            }
        }
        
        Vector3 newPos = transform.position; // gets current position 

        if ((newPos.x - _oldPos.x) != 0) // checks if positions are different
        {
            GetComponent<Animation>().Play("walk"); // plays the walk animation
        }
        _oldPos = newPos; // updates old position 
        
        if (health <= 0) // if dead
        {
            Destroy(gameObject); // delete itself
        }    
    }
    
    void OnTriggerEnter(Collider col) //if hit
    {
        if(col.CompareTag("Bullet"))  //if hit by a bullet
        {
            health = health - 2; //takes damage
        }
    }
}


   
