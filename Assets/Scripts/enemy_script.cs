using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public NavMeshAgent navAgent;
    public Transform player;
    public LayerMask groundLayer, playerLayer;
    public float health;
    public float timeBetweenAttacks;
    public float sightRange;
    public float attackRange;
    public int damage;



    private bool _alreadyAttacked;
    private bool _takeDamage;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        player = GameObject.Find("Player").transform;
        navAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        bool playerInSightRange = Physics.CheckSphere(transform.position, sightRange, playerLayer);
        bool playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);

       
        if (playerInSightRange && !playerInAttackRange)
        {
            ChasePlayer();
        }
        else if (playerInAttackRange && playerInSightRange)
        {
            AttackPlayer();
        }
        else if (!playerInSightRange && _takeDamage)
        {
            ChasePlayer();
        }
    }

    

   private void ChasePlayer()
{
    navAgent.SetDestination(player.position);
    navAgent.isStopped = false; // Add this line
}


  private void AttackPlayer()
{
    navAgent.SetDestination(transform.position);

    if (!_alreadyAttacked)
    {
        transform.LookAt(player.position);
        _alreadyAttacked = true;
        Invoke(nameof(ResetAttack), timeBetweenAttacks);

        //RaycastHit hit;
        //if (Physics.Raycast(transform.position, transform.forward, out hit, attackRange))
        {
            
                //YOU CAN USE THIS TO GET THE PLAYER HUD AND CALL THE TAKE DAMAGE FUNCTION

            //PlayerHUD playerHUD = hit.transform.GetComponent<PlayerHUD>();
            //if (playerHUD != null)
            {
               //playerHUD._takeDamage(damage);
            }
             
        }
    }
}


    private void ResetAttack()
    {
        _alreadyAttacked = false;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        hitEffect.Play();
        StartCoroutine(TakeDamageCoroutine());

        if (health <= 0)
        {
            Invoke(nameof(DestroyEnemy), 0.5f);
        }
    }

    private IEnumerator TakeDamageCoroutine()
    {
        _takeDamage = true;
        yield return new WaitForSeconds(2f);
        _takeDamage = false;
    }

    private void DestroyEnemy()
    {
        StartCoroutine(DestroyEnemyCoroutine());
    }

    private IEnumerator DestroyEnemyCoroutine()
    {
        yield return new WaitForSeconds(1.8f);
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}

//https://github.com/sopermanspace/Enemy