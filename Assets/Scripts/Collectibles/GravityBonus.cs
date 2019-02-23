using UnityEngine;

public class GravityBonus : Collectibles
{
    [SerializeField] private ParticleSystem collectEffect;

    private PlayerMovement playerMovement;

    private void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    public override void Interactions()
    {
        // Show Particules
        Instantiate(collectEffect, transform.position, Quaternion.identity);

        // Change Player State
        playerMovement.PlayerStateChange();

        // Destroy GameObject
        base.Interactions();
    }
}
