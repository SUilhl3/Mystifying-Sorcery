using UnityEngine;

//enemy that just patrols, doesn't attack player or anything
public class Patrolling_Enemy : MonoBehaviour
{
    public float speed = 2f;
    Rigidbody2D rb;
    public LayerMask groundLayer;
    public Transform groundCheck;
    bool isFacingRight = true;
    RaycastHit2D hit;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        hit = Physics2D.Raycast(groundCheck.position, -transform.up, 1f, groundLayer);
    }

    private void FixedUpdate()
    {
        if (hit.collider != false)
        {
            if (isFacingRight)
            {
                rb.linearVelocity = new Vector2(speed, rb.linearVelocity.y);
            } else
            {
                rb.linearVelocity = new Vector2(-speed, rb.linearVelocity.y);
            }
        } else
        {
            isFacingRight = !isFacingRight;
            transform.localScale = new Vector3(-transform.localScale.x, 1f, 1f);
        }
    }
}
