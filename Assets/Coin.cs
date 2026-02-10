using System.Collections;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Collider2D coinCollider;
    
    public Sprite[] animationSprites; // Drag all your sparkle sprites here
    public float frameRate = 10f; // How fast the animation plays
    
    // Define the boundaries where coins can spawn
    public Vector2 spawnMinBounds = new Vector2(-8f, -4f);
    public Vector2 spawnMaxBounds = new Vector2(8f, 4f);
    
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        coinCollider = GetComponent<Collider2D>();
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Disable collider
            coinCollider.enabled = false;
            
            // Play animation
            StartCoroutine(PlayAnimationAndRespawn());
        }
    }
    
    IEnumerator PlayAnimationAndRespawn()
    {
        // Play through all the animation frames
        foreach (Sprite sprite in animationSprites)
        {
            spriteRenderer.sprite = sprite;
            yield return new WaitForSeconds(1f / frameRate);
        }
        
        // Move to random position
        transform.position = GetRandomPosition();
        
        // Reset to first sprite
        if (animationSprites.Length > 0)
        {
            spriteRenderer.sprite = animationSprites[0];
        }
        
        // Re-enable collider
        coinCollider.enabled = true;
    }
    
    Vector3 GetRandomPosition()
    {
        float randomX = Random.Range(spawnMinBounds.x, spawnMaxBounds.x);
        float randomY = Random.Range(spawnMinBounds.y, spawnMaxBounds.y);
        return new Vector3(randomX, randomY, 0f);
    }
}