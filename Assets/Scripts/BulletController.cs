using UnityEngine;

public class BulletController : MonoBehaviour
{
    // called when collision is detected
    void OnCollisionEnter(Collision collision)
    {
        // if the collision detected is with the terrain
        if (collision.gameObject.CompareTag("Terrain"))
        {
            Destroy(gameObject);
        }
    }
    
    void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag("Player"))
        {
            Destroy(gameObject);

        }
    }
    
}
