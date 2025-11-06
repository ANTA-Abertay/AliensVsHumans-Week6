using UnityEngine;

public class BulletController : MonoBehaviour
{ 
    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Enemy") || col.CompareTag("Terrain"))
        {
            Destroy(gameObject);

        }
    }
}
