using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // How fast the player moves foward
    public float forwardSpeed = 5f;

    public float jumpForce = 10f;

    private Rigidbody2D rb;

    private bool isGrounded = true;

    void Start()
    {

        rb = GetComponent <Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.Translate(Vector3.right * forwardSpeed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isGrounded = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    { 
        if (collision.gameObject.name == "Ground") 
        { 
            isGrounded = true;
        }
    }
}
