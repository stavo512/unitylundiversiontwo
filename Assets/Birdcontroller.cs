using UnityEngine;
using UnityEngine.SceneManagement;

public class BirdController : MonoBehaviour
{
    public float moveSpeed = 5f;

    void Update()
    {
        // Horizontal movement only
        float moveInputX = Input.GetAxis("Horizontal");
        
        // Move left/right
        transform.Translate(Vector2.right * moveInputX * moveSpeed * Time.deltaTime);
        
        // Flip bird based on movement
        if (moveInputX > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (moveInputX < 0)
            transform.localScale = new Vector3(-1, 1, 1);
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        // If bird hits a plane, go to homescreen
        if (collision.gameObject.CompareTag("Plane"))
        {
            SceneManager.LoadScene("homescreen");
        }
    }
}