using UnityEngine;

public class DamageGiver : MonoBehaviour
{
    public float damage = 1f;
    public LayerMask targetLayers;
    public AudioClip damageSound;
    
    private AudioSource audioSource;
    
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        
        // If no AudioSource exists, add one
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & targetLayers) == 0)
            return;
            
        if (other.TryGetComponent(out Health health))
        {
            health.TakeDamage(damage);
            
            // Play sound when damage is dealt
            if (damageSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(damageSound);
            }
        }
    }
}