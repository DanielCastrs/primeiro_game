using UnityEngine;

public class PlayerMovimento : MonoBehaviour
{
   



    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    private Rigidbody2D rb;
    private float moveInput;
    private Animator animator;
    private bool isRunning = false;
    private bool isJumping =false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        animator.SetBool("isRunning", false);
        animator.SetBool("isJumping", false);
    }

    void Update()
    {
        // Captura input do teclado (A/D ou setas)
        moveInput = Input.GetAxis("Horizontal");
        if (moveInput != 0 )
        {
            isRunning = true;
        }else
        {
            isRunning=false;
        }

        animator.SetBool("isRunning", isRunning);

        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            Jump();
        }
    }

    void FixedUpdate()
    {
        // Aplica o movimento no eixo X
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
        if(moveInput > 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }else if(moveInput < 0)
            {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        if(Mathf.Abs(rb.velocity.y) > 0.01f)
        {
            isJumping = true;
            animator.SetBool("isJumping", true);
        }
        else
        {
            isJumping=false;
            animator.SetBool("isJumping", false);
        }
    }

    void Jump()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.0f);
        if(hit.collider != null)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            animator.SetBool("isJumping", true);
        }
    }
}

