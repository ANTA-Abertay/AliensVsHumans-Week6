using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _rb;

    private float _movementX;
    private float _movementY;

    public float speed = 10;

    public GameObject bulletPrefab;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // This function is called when a move input is detected.
    void OnMove(InputValue movementValue)
    {
        // Convert the input value into a Vector2 for movement.
        Vector2 movementVector = movementValue.Get<Vector2>();

        // Store the X and Y components of the movement.
        _movementX = movementVector.x;
        _movementY = movementVector.y;
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
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.AddForce(new Vector2(_movementX, _movementY), ForceMode2D.Impulse);
    }

    // FixedUpdate is called once per fixed frame-rate frame.
    private void FixedUpdate()
    {
        // Create a 3D movement vector using the X and Y inputs.
        Vector3 movement = new Vector3(_movementX, 0.0f, _movementY);

        // Apply force to the Rigidbody to move the player.
        _rb.AddForce(movement * speed);
    }
}
