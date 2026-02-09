using UnityEngine;

public class PlaneMovement : MonoBehaviour
{
    private float speed;
    public float despawnDistance = 15f; // Distance from origin before despawn
    
    public void Initialize(float moveSpeed)
    {
        speed = moveSpeed;
    }
    
    void Update()
    {
        // Move in the direction the plane is facing
        transform.Translate(transform.up * speed * Time.deltaTime, Space.World);
        
        // Despawn when it goes too far from origin
        if (Vector2.Distance(transform.position, Vector2.zero) > despawnDistance)
        {
            Destroy(gameObject);
        }
    }
}