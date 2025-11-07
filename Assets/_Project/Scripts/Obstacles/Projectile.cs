using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 4f;
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
        Destroy(gameObject);
    }
}
