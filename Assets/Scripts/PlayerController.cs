
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    Rigidbody m_Rigidbody;
    Vector3 m_EulerAngleVelocity;
    private Rigidbody _rb;
    
    private Vector2 _movement;

    public float speed = 10;
    private Vector3 oldPos;
    public GameObject bulletPrefab;
    public Transform target;
    public float rotationSpeed;
   

    [SerializeField] private int test = 0;


    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        oldPos = gameObject.transform.position;
    }

    // This function is called when a move input is detected.
    void OnMove(InputValue movementValue)
    {
        // Convert the input value into a Vector2 for movement.
        Vector2 movementVector = movementValue.Get<Vector2>();

        // Store the X and Y components of the movement.
        _movement = new Vector2(-movementVector.x, movementVector.y);

        transform.rotation = Quaternion.LookRotation(_movement, Vector3.up);
        
    }

    void OnJump()
    {
        Shoot();
    }

    void Shoot()
    {
        // Spawn a bullet and store a reference to it so you can manipulate its values.
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        if (_rb != null)
            _rb.AddForce(Vector3.up * 100f,ForceMode.Force);
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
        // Create a 3D movement vector using the X and Y inputs.
        Vector3 movement = new Vector3(_movement.x, 0.0f, 0.0f);
        // Apply force to the Rigidbody to move the player.
        _rb.AddForce(movement * speed);
        
        Vector3 newPos = gameObject.transform.position;
        

        if ((newPos[0] - oldPos[0])!=0)
        {
            GetComponent<Animation>().Play("walk");
            
        }
          oldPos = newPos;

    }

    
}


    




