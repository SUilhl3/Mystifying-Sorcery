using UnityEngine;

public class JumpBlock : MonoBehaviour
{
    [SerializeField] float jumpForce = 25f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            collision.rigidbody.linearVelocityY += jumpForce;
        }
    }
}
