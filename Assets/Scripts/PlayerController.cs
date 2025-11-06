
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    Rigidbody _mRigidbody;
    Vector3 _mEulerAngleVelocity;
    private Rigidbody _rb;
    private int timmer
    private Vector2 _movement;
    public int health = 10;
    public float speed = 10;
    private Vector3 _oldPos;
    public GameObject bulletPrefab;
    public Transform target;
    public float rotationSpeed;
    

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
       if(time <= 0)
        {
            Shoot();
            timmer = 300;
        }
        
    }

    Shoot()
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
        timmer -= 1.f * Time.deltatime;
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
    }


    
}


    




