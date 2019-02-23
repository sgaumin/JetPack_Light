using UnityEngine;

public class DestroyAfterLoad : MonoBehaviour
{
    [SerializeField] private float timeToDestroy;

    // Destroy object after x seconds
    void Start()
    {
        Destroy(gameObject, timeToDestroy);
    }
}
