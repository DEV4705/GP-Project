using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody2D body;
    public float speed;
    private BoxCollider2D boxCollider;
    public LayerMask groundlayer;
    public LayerMask walllayer;
    private float walljumpCooldown;
    private float horizontalinput;
    private int wallSide;

    
    public float dashSpeed = 20f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;
    private bool isDashing;
    private float dashTime;
    private float dashCooldownTimer;
    private float dashDirection;


    Animator animator;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        horizontalinput = Input.GetAxis("Horizontal");
        walljumpCooldown += Time.deltaTime;
        dashCooldownTimer += Time.deltaTime;

        if (horizontalinput > 0.01f)
        {
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
        }
        else if (horizontalinput < -0.01f)
        {
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
        }


        if (isDashing)
        {
            body.velocity = new Vector2(dashDirection * dashSpeed, 0);
            dashTime += Time.deltaTime;

            if (dashTime >= dashDuration)
            {
                isDashing = false;
                body.gravityScale = 3;
            }
            return;
        }

        
        if (Input.GetKeyDown(KeyCode.LeftShift) && dashCooldownTimer >= dashCooldown)
        {
            StartDash();
        }

        
        if (walljumpCooldown > 0.2f)
        {
            body.velocity = new Vector2(horizontalinput * speed, body.velocity.y);

            if (onWall() && !isGrounded())
            {
                body.gravityScale = 0;
                body.velocity = Vector2.zero;
            }
            else
            {
                body.gravityScale = 3;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                jump();
            }
        }

        float xVelocity = Mathf.Abs(body.velocity.x);
        float yVelocity = body.velocity.y;
        bool jumping = !isGrounded();

        animator.SetFloat("xVelocity", xVelocity);
        animator.SetFloat("yVelocity", yVelocity);
        animator.SetBool("isJumping", jumping);

        Debug.Log($"onWall: {onWall()}, wallSide: {wallSide}, grounded: {isGrounded()}");
    }

    private void StartDash()
    {
        isDashing = true;
        dashTime = 0f;
        dashCooldownTimer = 0f;
        body.gravityScale = 0;

        
        dashDirection = horizontalinput != 0 ? Mathf.Sign(horizontalinput) : 1f;

        body.velocity = new Vector2(dashDirection * dashSpeed, 0);
    }

    private void jump()
    {
        if (isGrounded())
        {
            body.velocity = new Vector2(body.velocity.x, speed);
            
        }
        else if (onWall() && !isGrounded())
        {
            if (horizontalinput == 0)
            {
                body.velocity = new Vector2(-wallSide * 10, 0);
                transform.localScale = new Vector3(wallSide, transform.localScale.y, transform.localScale.z);
                
            }
            else
            {
                body.velocity = new Vector2(-wallSide * 3, 6);
                
            }
            walljumpCooldown = 0;
        }
        animator.SetBool("isJumping", !isGrounded());
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, 0.1f, groundlayer);
        return raycastHit.collider != null;
    }

    private bool onWall()
    {
        float castDistance = 0.1f;

        RaycastHit2D hitLeft = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size * 0.95f, 0f, Vector2.left, castDistance, walllayer);
        RaycastHit2D hitRight = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size * 0.95f, 0f, Vector2.right, castDistance, walllayer);

        if (hitLeft.collider != null)
        {
            wallSide = -1;
            return true;
        }
        else if (hitRight.collider != null)
        {
            wallSide = 1;
            return true;
        }

        wallSide = 0;
        return false;
    }

}
