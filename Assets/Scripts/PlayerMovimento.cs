using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMovimento : MonoBehaviour
{
   



    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    private Rigidbody2D rb;
    private float moveInput;
    private Animator animator;
    private bool isRunning = false;
    private bool isJumping =false;
    public float playerHealth = 100;

    public Slider lifeSlider;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        animator.SetBool("isRunning", false);
        animator.SetBool("isJumping", false);
        animator.SetBool("inDamage", false);
        
        Debug.Log("Life do Player: " + playerHealth);
    }

    void Update()
    {
        // Captura input do teclado (A/D ou setas)
        moveInput = Input.GetAxis("Horizontal");
        if (moveInput != 0 )
        {
            isRunning = true;
            animator.SetBool("isJumping", false);
        }else
        {
            isRunning=false;
        }

        animator.SetBool("isRunning", isRunning);

        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            Jump();
        }
        if (Input.GetButtonDown("Fire1"))
        {
            animator.SetTrigger("Attack");
        }

        lifeSlider.value = (playerHealth * 0.01f);
        
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
        if(Mathf.Abs(rb.linearVelocity.y) > 0.01f)
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
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            animator.SetBool("isJumping", true);
        }
    }

    public void TakeDamage(int damage)
    {
        playerHealth -= damage;
        animator.SetBool("inDamage", true);
        Debug.Log("Player tomou" + damage + " de dano. Saúde restante: " + playerHealth);

        StartCoroutine(ResetDamageAnimation());

        if(playerHealth <= 0)
        {

            Debug.Log("Player Morreu!");
            SceneManager.LoadScene(2);
        }
    }
    private IEnumerator ResetDamageAnimation()
    {
        yield return new WaitForSeconds(1f);
        animator.SetBool("inDamage", false);
        
    }

}

