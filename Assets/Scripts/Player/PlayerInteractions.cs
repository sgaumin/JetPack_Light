using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    // Detect type of object on Collision
    private void OnTriggerEnter(Collider other)
    {
        if (GameSystem.gameState == GameStates.Playing)
        {
            Collectibles collectible = other.GetComponent<Collectibles>();
            IKill killableObject = other.GetComponent<IKill>();

            if (collectible != null)
            {
                GameSystem.instance.CollectCoins(collectible.value);
                collectible.Interactions();
            }

            if (killableObject != null)
            {
                killableObject.Kill();
                GameSystem.instance.EndGame();
            }
        }
    }
}
