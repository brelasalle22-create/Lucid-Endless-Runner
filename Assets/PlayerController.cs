using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // How fast the player moves forward
    public float forwardSpeed = 5f;

    // How strong the jump is
    public float jumpForce = 10f;

    // The two colors the player can be
    public Color pinkColor = new Color(1f, 0.78f, 0.86f);
    public Color tealColor = new Color(0.6f, 0.95f, 0.93f);

    // Tracks the player's current color (true = pink, false = teal)
    public bool isPink = true;

    // Reference to the Rigidbody2D component
    private Rigidbody2D rb;

    // Reference to the SpriteRenderer (for changing color)
    private SpriteRenderer sr;

    // Tracks whether the player is touching the ground
    private bool isGrounded = true;

    // Tracks whether the player is dead
    private bool isDead = false;

    // Start is called once when the game begins
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        // Set the player's starting color
        UpdatePlayerColor();
    }

    // Update is called once per frame
    void Update()
    {
        // Only run movement and input if the player is alive
        if (isDead == false)
        {
            // Move the player to the right every frame
            transform.Translate(Vector3.right * forwardSpeed * Time.deltaTime);

            // Jump and flip color when player presses Space AND is grounded
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                isGrounded = false;

                // Flip the color from pink to teal or teal to pink
                if (isPink == true)
                {
                    isPink = false;
                }
                else
                {
                    isPink = true;
                }

                UpdatePlayerColor();
            }
        }
    }

    // Updates the player's sprite color based on isPink
    void UpdatePlayerColor()
    {
        if (isPink == true)
        {
            sr.color = pinkColor;
        }
        else
        {
            sr.color = tealColor;
        }
    }

    // Runs when the player physically collides with something
    void OnCollisionEnter2D(Collision2D collision)
    {
        // If we touched the ground, we can jump again
        if (collision.gameObject.name == "Ground")
        {
            isGrounded = true;
        }
    }

    // Runs when the player enters a trigger collider (like a spike)
    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if we touched an obstacle
        if (other.gameObject.tag == "Obstacle")
        {
            // Get the obstacle's script to check its color
            Obstacle obstacle = other.GetComponent<Obstacle>();

            // If the obstacle's color does not match ours, game over
            if (obstacle.isPink != isPink)
            {
                GameOver();
            }
        }
    }

    // Handles what happens when the player dies
    void GameOver()
    {
        isDead = true;
        Debug.Log("Game Over!");
    }
}