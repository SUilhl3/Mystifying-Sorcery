using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Boss: MonoBehaviour
{
    public Transform player;
    private SpriteRenderer spriteRenderer;
    public bool isFlipped = false;

    public int maxHealth = 100;
    public int currentHealth;
    public Slider healthSlider;

    public Animator animator;
    public float deathDelay = 1f;


    private void Awake()
    {
        currentHealth = maxHealth;

        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }

    private void TakeDamage(int amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        healthSlider.value = currentHealth;

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        animator.SetTrigger("Death");

        GetComponent<Collider2D>().enabled = false;
        healthSlider.gameObject.SetActive(false);
        Destroy(gameObject, deathDelay);
    }
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    public void Fip()
    {
        if (player == null || spriteRenderer == null) return;
        if (transform.position.x > player.position.x &&  !isFlipped)
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
            isFlipped = !isFlipped;
        }
    }
}
