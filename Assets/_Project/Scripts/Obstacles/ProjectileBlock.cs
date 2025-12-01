using UnityEngine;

public class ProjectileBlock : MonoBehaviour
{
    [SerializeField] float detectionRange = 5f;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] bool shootingLeft;
    [SerializeField] GameObject projectile;
    [SerializeField] Vector3 offset;
    [SerializeField] float countingTime = 0;
    [SerializeField] float delayTime = 3f;
    private Vector2 detectionDirection;
    Rigidbody2D rb;

    private void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        
        if (shootingLeft)
        {
            detectionDirection = Vector2.left;
            offset = new Vector3(offset.x*-1, offset.y, offset.z);
        } else
        {
            detectionDirection = Vector2.right;
            //transform.Rotate(0f, 180f, 0f);
        }
    }

    private void Update()
    {
            // Raycast to detect player
            RaycastHit2D hit = Physics2D.Raycast(transform.position, detectionDirection, detectionRange, playerLayer);
            if (hit.collider != null && hit.collider.CompareTag("Player"))
            {
                StartShooting();
            }

    }

    //starts shooting the player when in range
    private void StartShooting()
    {
        if (countingTime >= delayTime)
        {
            GameObject proj = Instantiate(projectile, transform.position + offset, transform.rotation);
            Projectile projScript = proj.GetComponent<Projectile>();
            projScript.speed = projScript.speed * (shootingLeft ? -1 : 1);
            countingTime = 0f;
        }

        countingTime += 1 * Time.deltaTime;
    }
}
