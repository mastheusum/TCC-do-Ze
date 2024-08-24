
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Player2Move : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 7;
    public bool isJumping;

    public int maxJumps = 2;
    public int jumpCount = 0;
    public float jumpSpeed;

    private float moveX;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Update()
    {
        moveX = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2( moveX * speed, rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (jumpCount < maxJumps)
            {
                isJumping = true;
                rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
                jumpCount++;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D floor)
    {
        if (floor.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
            jumpCount = 0;
        }
    }
}
