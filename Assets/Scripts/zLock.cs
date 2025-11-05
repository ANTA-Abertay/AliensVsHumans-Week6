using UnityEngine;

public class zLock : MonoBehaviour
{
    public float z = 8;

    void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, z);
    }
}
