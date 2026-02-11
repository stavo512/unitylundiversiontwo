using UnityEngine;

public class Blackhole : MonoBehaviour
{
    [Header("Blackhole Settings")]
    [SerializeField] private float pullRadius = 10f;
    [SerializeField] private float pullForce = 5f;
    [SerializeField] private float killRadius = 1f;
    
    private Transform playerTransform;
    private Rigidbody playerRb;
    
    void Start()
    {
        // Find the player - adjust the tag if needed
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
            playerRb = player.GetComponent<Rigidbody>();
        }
    }
    
    void FixedUpdate()
    {
        if (playerTransform == null || playerRb == null) return;
        
        float distance = Vector3.Distance(transform.position, playerTransform.position);
        
        // Check if player is within pull radius
        if (distance < pullRadius)
        {
            // Calculate direction to blackhole
            Vector3 direction = (transform.position - playerTransform.position).normalized;
            
            // Apply force that gets stronger as player gets closer
            float strength = pullForce * (1f - distance / pullRadius);
            playerRb.AddForce(direction * strength);
        }
        
        // Kill player if they get too close
        if (distance < killRadius)
        {
            KillPlayer();
        }
    }
    
    void KillPlayer()
    {
        // Add your death logic here
        Debug.Log("Player sucked into blackhole!");
        // Examples:
        // playerTransform.gameObject.SetActive(false);
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    // Visual helper in editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, pullRadius);
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, killRadius);
    }
}