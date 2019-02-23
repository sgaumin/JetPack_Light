using UnityEngine;

public class Obstacle : MonoBehaviour, IKill
{
    [SerializeField] private float rotationSpeed;
    [SerializeField] private ParticleSystem playerKilledParticules;

    public void Kill() {

        // Show Player Killed Particules
        Instantiate(playerKilledParticules, transform.position, Quaternion.identity);
    }

    private void Update()
    {
        // Rotate gameobject
        transform.Rotate(Vector3.right, rotationSpeed);
    }
}
