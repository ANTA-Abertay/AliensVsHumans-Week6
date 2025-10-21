
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    Rigidbody m_Rigidbody;
    Vector3 m_EulerAngleVelocity;
    private Rigidbody _rb;
    
    private Vector2 _movement;

    public float speed = 10;

    public GameObject bulletPrefab;
    public Transform target;
    public float rotationSpeed;

   

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
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
        // Apply force if using Rigidbody
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
            rb.AddForce(new Vector2(_movement.x, _movement.y), ForceMode.Impulse);

       
    }

    // FixedUpdate is called once per fixed frame-rate frame.
    private void FixedUpdate()
    {
        // Create a 3D movement vector using the X and Y inputs.
        Vector3 movement = new Vector3(_movement.x, 0.0f, _movement.y);
        movement.Normalize();
        // Apply force to the Rigidbody to move the player.
        _rb.AddForce(movement * speed);


    }
    


    
}



