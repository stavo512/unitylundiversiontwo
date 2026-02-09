using UnityEngine;

public class SharkChase : MonoBehaviour
{
    public Transform player; // Drag your player character here in the Inspector
    public float moveSpeed = 3f;
    public float dashSpeed = 10f;
    public float heightThreshold = 0.5f; // How close to player's height before dashing
    
    private bool isDashing = false;

    void Update()
    {
        if (player != null)
        {
            float heightDifference = Mathf.Abs(transform.position.y - player.position.y);
            
            if (!isDashing)
            {
                // Phase 1: Align to player's height
                if (heightDifference > heightThreshold)
                {
                    // Move vertically to match player's height
                    float targetY = player.position.y;
                    transform.position = Vector2.MoveTowards(
                        transform.position, 
                        new Vector2(transform.position.x, targetY), 
                        moveSpeed * Time.deltaTime
                    );
                    
                    // Flip to face player
                    if (player.position.x > transform.position.x)
                        transform.localScale = new Vector3(-1, 1, 1);
                    else
                        transform.localScale = new Vector3(1, 1, 1);
                }
                else
                {
                    // Height aligned - start dashing!
                    isDashing = true;
                }
            }
            else
            {
                // Phase 2: Dash horizontally toward player
                transform.position = Vector2.MoveTowards(
                    transform.position, 
                    new Vector2(player.position.x, transform.position.y), 
                    dashSpeed * Time.deltaTime
                );
                
                // Check if reached player's horizontal position
                if (Mathf.Abs(transform.position.x - player.position.x) < 0.5f)
                {
                    isDashing = false; // Reset to chase again
                }
            }
        }
    }
}