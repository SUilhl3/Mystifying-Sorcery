using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 4f;
    public int damage = 2;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Launch();
    }
    void Launch()
    {
        rb.linearVelocity = new Vector2(speed, rb.linearVelocity.y); 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            PlayerController pc = collision.collider.GetComponent<PlayerController>();
            pc.TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
