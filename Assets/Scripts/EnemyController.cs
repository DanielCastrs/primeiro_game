using UnityEditor.Tilemaps;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public Transform waypoinA;
    public Transform waypoinB;
    public float movementSpeed = 2f;
    private Animator animator;
    private bool isWalking = false;

    private Transform currentTarget;
    private Rigidbody2D rb;
    private Vector3 scale;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        currentTarget = waypoinA;
        scale = transform.localScale;
    }

       void Update()
    {
        MoveTowardsTarget();
    }

    private void MoveTowardsTarget()
    {
        Vector3 curTargetHorizontal = new Vector2(currentTarget.position.x, transform.position.y);
        Vector2 direction = (curTargetHorizontal - transform.position).normalized;

        transform.position += (Vector3)direction * movementSpeed * Time.deltaTime;

        if (Vector2.Distance(curTargetHorizontal, transform.position) <= 0.2f)
        {
            SwitchTarget();

        }

        UpdateAnimation();
    }
    
    private void SwitchTarget()
    {
        if (currentTarget == waypoinA)
        {
            currentTarget = waypoinB;
            Flip();
        }
        else
        {
            currentTarget=waypoinA;
            transform.localScale = scale;
        }
    }

    private void UpdateAnimation()
    {
        isWalking = (Vector2.Distance(transform.position, currentTarget.position) > 0.1f);
        animator.SetBool("isWalking", isWalking);
    }

    private void Flip()
    {
        Vector3 flippedScale = scale;
        flippedScale.x *= -1;
        transform.localScale = flippedScale;
    }
}
