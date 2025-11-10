using UnityEngine;

public class zLock : MonoBehaviour
{
    public float z = 8; // set z axis

    void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, z); //enforce z axis
    }
}
