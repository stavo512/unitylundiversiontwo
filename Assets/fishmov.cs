using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    void Update()
    {
        // Get input from arrow keys
        float moveInputX = Input.GetAxis("Horizontal"); // -1 for left, 1 for right
        float moveInputY = Input.GetAxis("Vertical");   // -1 for down, 1 for up
        
        // Create movement vector
        Vector2 movement = new Vector2(moveInputX, moveInputY);
        
        // Move the character
        transform.Translate(movement * moveSpeed * Time.deltaTime);
        
        // Flip the character based on horizontal movement direction
        if (moveInputX > 0) // Moving right
        {
            transform.localScale = new Vector3(-3, 3, 3); // Face right (normal)
        }
        else if (moveInputX < 0) // Moving left
        {
            transform.localScale = new Vector3(3, 3, 3); // Face left (flipped)
        }
    }
}