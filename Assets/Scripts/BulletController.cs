using UnityEngine;

public class BulletController : MonoBehaviour
{ 
    void OnTriggerEnter(Collider col) 
    {
        if (col.CompareTag("Enemy") || col.CompareTag("Terrain")) // if in contact with an enemy or the ground
        {
            Destroy(gameObject); // delete

        }
    }
}
