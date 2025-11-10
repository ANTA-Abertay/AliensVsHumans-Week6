using UnityEngine;
using UnityEngine.InputSystem;




public class PlayerController : MonoBehaviour
{
    GameObject _player;
    public GameObject bulletSpawnBox;
    Rigidbody _mRigidbody;
    Vector3 _mEulerAngleVelocity;
    
    private Rigidbody _rb;
    private Vector2 _movement;
    private Vector3 _oldPos;
    private CapsuleCollider _collider;
    private float _enemyTimer;
    private float _jumpTimer;
    
    public LayerMask layerMask;
    public GameObject bulletPrefab;       
    public Transform target;
    public static int CurrentHealth = 10;
    public int health = 10;
    public float speed = 10;
    public float rotationSpeed;
    private int _level = 1;


    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _oldPos = gameObject.transform.position;
        _collider = GetComponent<CapsuleCollider>();
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
        _rb.AddForce(Vector3.up * 100f, ForceMode.Force);
        if (_jumpTimer <= 0.0f)
        {
            Shoot();
            _jumpTimer = 1.0f;
        }

    }

    void Shoot()
    {
        // Spawn a bullet and store a reference to it so you can manipulate its values.
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnBox.transform.position, bulletSpawnBox.transform.rotation);
        // Apply force if using Rigidbody
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
            rb.AddForce(new Vector2(_movement.x, _movement.y), ForceMode.Impulse);
    }

    // FixedUpdate is called once per fixed frame-rate frame.
    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(_movement.x, 0.0f, 0.0f); // only moves in x-axis
        _rb.AddForce(movement * speed); // times the x-axis with speed so player moves
        

        // Cast a sphere wrapping character controller 10 meters forward
        // to see if it is about to hit anything.
        //if (Physics.SphereCast(transform.position, _Collider.height*0.5f, transform.forward, out hit, 2.0f, layerMask))
        Collider[] colliders = Physics.OverlapSphere(transform.position, _collider.height * 0.3f, layerMask);

        if (colliders.Length > 0)
        {
            if (_enemyTimer <= 0.0f)
            {
                health -= 2;
                _enemyTimer = 2.0f;
            }
        }

    }

    void Update()
    {
        _enemyTimer -= Time.deltaTime;
        _jumpTimer -= Time.deltaTime;

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

        if (GameManager.Instance.currentLevel != _level)
        {
            _level = GameManager.Instance.currentLevel;
            health = 10;
        }

    }
}
