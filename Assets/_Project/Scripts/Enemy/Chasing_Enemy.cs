using UnityEngine;

public class Chasing_Enemy : MonoBehaviour
{
    StateMachine stateMachine;
    public float speed = 2f;
    public float chaseSpeed = 3f;
    Rigidbody2D rb;
    public LayerMask groundLayer;
    public LayerMask playerLayer;
    public Transform groundCheck;
    [SerializeField] float detectionRange = 5f;
    private Vector2 detectionDirection;
    bool isFacingRight = true;
    RaycastHit2D hit;
    RaycastHit2D playerHit;
    [SerializeField] GameObject player;
    private float chaseDistance;

    void Start()
    {
        player = FindFirstObjectByType<PlayerController>().gameObject;
        stateMachine = new StateMachine();
        rb = GetComponent<Rigidbody2D>();
        var patrol = stateMachine.CreateState("Patrol");
        patrol.onStay = delegate
        {
            Patrol();
            DetectPlayer();
        };

        var chase = stateMachine.CreateState("Chase");
        chase.onStay = delegate
        {
            Chase();
        };

    }

    void Patrol()
    {
        hit = Physics2D.Raycast(groundCheck.position, -transform.up, 1f, groundLayer);

        if (hit.collider != false)
        {
            if (isFacingRight)
            {
                rb.linearVelocity = new Vector2(speed, rb.linearVelocity.y);
            }
            else
            {
                rb.linearVelocity = new Vector2(-speed, rb.linearVelocity.y);
            }
        }
        else
        {
            isFacingRight = !isFacingRight;
            transform.localScale = new Vector3(-transform.localScale.x, 1f, 1f);
        }
    }

    void DetectPlayer()
    {
        if (isFacingRight)
        {
            detectionDirection = Vector2.right;
        }
        else
        {
            detectionDirection = Vector2.left;
        }
        playerHit = Physics2D.Raycast(transform.position, detectionDirection, detectionRange, playerLayer);

        if (playerHit.collider != null && playerHit.collider.CompareTag("Player"))
        {
            stateMachine.TransitionTo("Chase");
        }
    }

    void Chase()
    {
        chaseDistance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, chaseSpeed * Time.deltaTime);

        if (chaseDistance > 4)
        {
            stateMachine.TransitionTo("Patrol");
        }
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.Update();
    }
}
