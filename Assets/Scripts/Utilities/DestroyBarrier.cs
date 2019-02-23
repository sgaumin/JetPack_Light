using UnityEngine;

public class DestroyBarrier : MonoBehaviour
{
    // Destroy all objects when collision
    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
    }
}
