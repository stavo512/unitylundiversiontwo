using UnityEngine;

public class PlaneSpawner : MonoBehaviour
{
    public GameObject planePrefab; // Drag your plane prefab here
    public float spawnInterval = 1.5f; // Time between spawns
    public float spawnYPosition = -6f; // Y position where planes spawn (below screen)
    public float minSpawnX = -8f; // Minimum X position for spawning
    public float maxSpawnX = 8f; // Maximum X position for spawning
    public float baseSpeed = 3f; // Starting speed of planes
    public float speedIncreaseRate = 0.1f; // How much speed increases over time
    
    private float spawnTimer;
    private float currentSpeed;
    private float gameTime;

    void Start()
    {
        currentSpeed = baseSpeed;
    }

    void Update()
    {
        // Track game time for speed increase
        gameTime += Time.deltaTime;
        currentSpeed = baseSpeed + (gameTime * speedIncreaseRate);
        
        // Spawn timer
        spawnTimer += Time.deltaTime;
        
        if (spawnTimer >= spawnInterval)
        {
            SpawnPlane();
            spawnTimer = 0f;
        }
    }
    
    void SpawnPlane()
    {
        // Random X position
        float randomX = Random.Range(minSpawnX, maxSpawnX);
        Vector2 spawnPosition = new Vector2(randomX, spawnYPosition);
        
        // Spawn the plane
        GameObject plane = Instantiate(planePrefab, spawnPosition, Quaternion.identity);
        
        // Add movement script to it
        plane.AddComponent<PlaneMovement>().Initialize(currentSpeed);
    }
}