
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 7;
    public bool isJumping;

    private float moveX;

    public bool canDash = true;
    public bool isDashing;
    public float dashingPower = 24f;
    public float dashingTime = 0.2f;
    public float dashingCooldown = 1;
    [SerializeField] private TrailRenderer tr;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Update()
    {
        if (isDashing)
        {
            return;
        }
        moveX = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2( moveX * speed, rb.velocity.y);

        if (Input.GetKey(KeyCode.Space) && isJumping == false)
            rb.velocity = new Vector2(rb.velocity.x, speed);
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }

    }

    private void OnCollisionEnter2D(Collision2D floor)
    {
        if (floor.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }

    private void OnCollisionExit2D(Collision2D floor)
    {
        if (floor.gameObject.CompareTag("Ground"))
        {
            isJumping = true;
        }
    }
    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(moveX * dashingPower, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
}
