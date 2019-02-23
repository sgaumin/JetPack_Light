using UnityEngine;

public abstract class Collectibles : MonoBehaviour
{
    // Value of the collectible
    public int value;

    // Method can be updated by child class
    public virtual void Interactions() {
        Destroy(gameObject);
    }
}