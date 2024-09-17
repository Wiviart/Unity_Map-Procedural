using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;

    public float speed = 5f;
    public float jumpForce = 10f;
    bool isJumping = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Move();
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            isJumping = true;
        }
    }

    void FixedUpdate()
    {
        if (isJumping)
        {
            Jump();
            isJumping = false;
        }
    }

    void Move()
    {
        float x = Input.GetAxis("Horizontal") * speed;
        float y = rb.velocity.y;

        rb.velocity = new Vector2(x, y);
    }

    void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
}