using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("Items Spawning Parameters")]
    [Tooltip("Intial speed value of spawn object.")]
    [Range(0, 10)]
    [SerializeField] private float spawnMoveSpeed = 5f;
    [Tooltip("Pourcentage of randomness for each sapwn time value.")]
    [Range(0, 2)]
    [SerializeField] private float RandomSpawnTimeFactor = 1f;
    [Tooltip("Y variations in absolute value.")]
    [Range(0, 5)]
    [SerializeField] private float verticalVariations = 3f;

    [Header("Difficulty")]
    [Tooltip("Mutilply intial speed value.")]
    [Range(1, 2)]
    [SerializeField] private float difficultyFactor = 1.1f;
    [Tooltip("Steps of difficulty.")]
    [Range(1, 10)]
    [SerializeField] private float timeDifficultyChange = 5f;

    [Header("Collectibles")]
    [SerializeField] private Collectibles[] collectibles;
    [Tooltip("Base spawning time for collectibles.")]
    [Range(0, 10)]
    [SerializeField] private float spawnTimeCollectibles = 2f;

    [Header("Obstacles")]
    [SerializeField] private Obstacle obstacle;
    [Tooltip("Base spawning time for obstacles.")]
    [Range(0, 10)]
    [SerializeField] private float spawnTimeObstacles = 3f;
    [Tooltip("Base rotation for obstacles in absolute value.")]
    [Range(0, 90)]
    [SerializeField] private float maxRotation = 45f;

    void Start()
    {
        // Spawning Objects
        StartCoroutine(SpawnCollectibles());
        StartCoroutine(SpawnObstacles());

        // Increase Speed by Time
        StartCoroutine(IncreaseSpeed());
    }

    IEnumerator SpawnCollectibles()
    {
        while (true)
        {
            // Define next spawning position
            Vector3 nextPos = new Vector3(transform.position.x, transform.position.y + Random.Range(-verticalVariations, verticalVariations), transform.position.z);

            // Instantiate Collectible
            Collectibles collectibleTemp = Instantiate(collectibles[Random.Range(0, collectibles.Length)], nextPos, Quaternion.identity, transform);
            collectibleTemp.GetComponent<Rigidbody>().velocity = -Vector3.right * spawnMoveSpeed;

            // Calculate next spawning time
            float timeNextSpawn = spawnTimeCollectibles + Random.Range(-spawnTimeCollectibles * RandomSpawnTimeFactor, spawnTimeCollectibles * RandomSpawnTimeFactor);

            yield return new WaitForSeconds(timeNextSpawn);
        }
    }

    IEnumerator SpawnObstacles()
    {
        while (true)
        {
            // Define next spawning position
            Vector3 nextPos = new Vector3(transform.position.x, transform.position.y + Random.Range(-verticalVariations, verticalVariations), transform.position.z);

            // Define next spawning rotation
            float angle = Random.Range(-maxRotation, maxRotation);

            // Instantiate Obstacle
            Obstacle obstacleTemp = Instantiate(obstacle, nextPos, Quaternion.identity, transform);
            obstacleTemp.GetComponent<Rigidbody>().velocity = -Vector3.right * spawnMoveSpeed;
            obstacleTemp.transform.Rotate(new Vector3(0f, 0f, angle));

            // Calculate next spawning time
            float timeNextSpawn = spawnTimeObstacles + Random.Range(0f, spawnTimeObstacles * RandomSpawnTimeFactor);

            yield return new WaitForSeconds(timeNextSpawn);
        }
    }

    // Increase Speed of spawn objects by Time
    IEnumerator IncreaseSpeed()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeDifficultyChange);
            spawnMoveSpeed *= difficultyFactor;
        }
    }
}
