using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    void Update()
    {
        // Get input from arrow keys
        float moveInput = Input.GetAxis("Horizontal"); // -1 for left, 1 for right, 0 for no input
        
        // Move the character
        transform.Translate(Vector2.right * moveInput * moveSpeed * Time.deltaTime);
        
        // Flip the character based on movement direction
        if (moveInput > 0) // Moving right
        {
            transform.localScale = new Vector3(-1, 1, 1); // Face right (normal)
        }
        else if (moveInput < 0) // Moving left
        {
            transform.localScale = new Vector3(1, 1, 1); // Face left (flipped)
        }
    }
}