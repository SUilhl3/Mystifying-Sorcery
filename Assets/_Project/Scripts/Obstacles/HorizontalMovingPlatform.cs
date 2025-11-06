using UnityEngine;

public class HorizontalMovingPlatform : MonoBehaviour
{
    [SerializeField]
    float speed = 3;
    float maxHorizontalDistance = 5;
    Vector2 originalPos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        originalPos = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (maxHorizontalDistance > 0)
        {
            var newX = Mathf.PingPong(Time.fixedTime * speed, maxHorizontalDistance);
            this.transform.position = originalPos + new Vector2(newX, 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.transform.SetParent(null);
    }
}
