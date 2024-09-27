using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;

    public float speed = 10f;
    public float jumpForce = 10f;
    bool isJumping = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
    }

    void Update()
    {
        Move();

    }

    void Move()
    {
        float x = Input.GetAxis("Horizontal") * speed;
        float y = Input.GetAxis("Vertical") * speed;
        rb.velocity = new Vector2(x, y);
    }
}