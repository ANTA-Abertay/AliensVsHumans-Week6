using UnityEngine;
using UnityEngine.InputSystem;
using System; 


public class PlayerController : MonoBehaviour
{
    Rigidbody _mRigidbody;
    Vector3 _mEulerAngleVelocity;
    private Rigidbody _rb;
    private float _timer;
    private Vector2 _movement;
    public int health = 10;
    public static  int CurrentHealth = 10; 
    public event Action OnHealthChange;
    public float speed = 10;
    private Vector3 _oldPos;
    public GameObject bulletPrefab;
    public Transform target;
    public float rotationSpeed;
    
    GameObject _player;
    
    
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _oldPos = gameObject.transform.position;
        
    }

    // This function is called when a move input is detected.
    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>(); // since movement is a 2d vector 
        _movement = new Vector2(-movementVector.x, movementVector.y); // since we don't want to move on the z axis

        // If moving left/right, flip the player visually.
        if (_movement.x != 0) // if they are moving
        {
            float yRotation = _movement.x > 0 ? 90f : -90f; // facing right or left 
            transform.rotation = Quaternion.Euler(0, yRotation, 0); // rotates the asset to face left or right
        }
    }


    void OnJump()
    {
        
       if(_timer <= 0)
       {
           Shoot();
           _timer = 150;
       }
        
    }

    void Shoot()
    {
        // Spawn a bullet and store a reference to it so you can manipulate its values.
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        if (_rb != null)
            _rb.AddForce(Vector3.up * 80f,ForceMode.Force);
        Debug.Log(_rb.linearVelocity);
        // Apply force if using Rigidbody
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
            rb.AddForce(new Vector2(_movement.x, _movement.y), ForceMode.Impulse);
        Debug.Log($"Movement input: {_movement}");
        

       
    }

    // FixedUpdate is called once per fixed frame-rate frame.
    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(_movement.x, 0.0f, 0.0f); // only moves in x-axis
        _rb.AddForce(movement * speed); // times the x-axis with speed so player moves
        _timer -= Time.deltaTime;
    }

    void Update()
    {
        Vector3 newPos = transform.position; // gets current position 

        if ((newPos.x - _oldPos.x) != 0) // checks if positions are different
        {
            GetComponent<Animation>().Play("walk"); // plays the walk animation
        }

        _oldPos = newPos; // updates old position 

        if (health <= 0)
        {
            Destroy(gameObject);
        }

        
        
        EnemyScript[] enemies = FindObjectsByType<EnemyScript>(FindObjectsSortMode.InstanceID);
        foreach (var enemy in enemies)
        {


            var posDif = (enemy.transform.position - _player.transform.position);
            if (posDif.magnitude < 5)
            {
                if (_timer <= 0)
                {
                    health -= 2;
                    if (CurrentHealth != health)
                    {
                        CurrentHealth = health;
                        OnHealthChange?.Invoke();
                    }

                    _timer = 300;
                }
            }

            _timer -= Time.deltaTime;
        }
        
    }

   
    
}


    




