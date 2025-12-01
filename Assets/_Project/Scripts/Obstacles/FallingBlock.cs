using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBlock : MonoBehaviour
{
    [SerializeField] float detectionRange = 5f;
    [SerializeField] float fallSpeed;
    [SerializeField] LayerMask playerLayer;
    private bool isFalling = false;
    public int damage = 10;
    Rigidbody2D rb;

    private void Start()
    {

        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!isFalling)
        {
            // Raycast downward to detect player
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, detectionRange, playerLayer);
            if (hit.collider != null && hit.collider.CompareTag("Player"))
            {
                StartFalling();
            }
        }

    }

    //falls down towards the ground when player is beneath 
    private void StartFalling()
    {
        isFalling = true;
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = fallSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            PlayerController pc = collision.collider.GetComponent<PlayerController>();
            pc.TakeDamage(damage);
        }
    }
}
