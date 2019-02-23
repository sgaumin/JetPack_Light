using UnityEngine;

public class Coin : Collectibles
{
    [SerializeField] private ParticleSystem collectEffect;

    public override void Interactions()
    {
        // Show Particule coin collected
        Instantiate(collectEffect, transform.position, Quaternion.identity);

        // Destroy GameObject
        base.Interactions();
    }
}
