using UnityEngine;

public class Key : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<CapsuleCollider2D>().tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().addKey(1);
            gameObject.SetActive(false);
        }
    }
}
