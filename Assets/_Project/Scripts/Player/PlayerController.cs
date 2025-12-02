using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    #region variables
    [Header("Movement Settings")]
    public float moveSpeed;
    public float jumpSpeed;

    [Header("References")]
    public PlayerData playerData;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Camera cam;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform ceilingCheck;
    [SerializeField] private Transform DashDirectionIndicator;
    [SerializeField] private Animator anim;

    [Header("State")]
    public bool grounded;
    public bool underCeiling;
    public int FacingDirection = 1;

    public int keys = 0;

    // Dash variables
    private bool canDash = true;
    private bool isDashing;
    private bool isHoldingDash;
    private float lastDashTime;
    private float dashStartTime;
    private Vector2 dashDirection;
    private Vector2Int dashDirectionInput;
    [SerializeField] private int maxDashes = 1;
    [SerializeField] private int dashesLeft;
    private bool dashInput;
    private bool dashInputStop;
    private Vector2 RawDashDirectionInput;

    private float currentMoveInputX;

    [Header("Combat Settings")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange = 0.5f;
    [SerializeField] private LayerMask enemyLayers;
    [SerializeField] private int attackDamage = 10;
    [SerializeField] private float attackRate = 2f;
    private float nextAttackTime = 0f;
    #endregion


    #region runtime functions
    void Start()
    {
        InitValues();
    }

    void Update()
    {
        grounded = CheckIfGrounded();
        underCeiling = CheckForCeiling();

        anim.SetFloat("Walking", Mathf.Abs(currentMoveInputX));

        if (grounded && !isDashing && !isHoldingDash) { dashesLeft = maxDashes; }

        HandleDash();
    }

    void FixedUpdate()
    {
        if (!isDashing)
        {
            float x = Mathf.Round(currentMoveInputX) * moveSpeed;
            rb.linearVelocity = new Vector2(x, rb.linearVelocity.y);
        }
    }
    #endregion


    public void addKey(int amount)
    {
        keys += amount;
    }
    public void loseKey(int amount)
    {
        keys -= amount;
    }

    #region Checks
    public bool CheckForCeiling() {return Physics2D.OverlapCircle(ceilingCheck.position, playerData.groundCheckRadius, playerData.whatIsGround);}

    public bool CheckIfGrounded() { return Physics2D.OverlapCircle(groundCheck.position, playerData.groundCheckRadius, playerData.whatIsGround); }

    public void CheckIfShouldFlip(int xInput) {if (xInput != 0 && xInput != FacingDirection){ Flip(); }}

    public bool hasKey()
    {
        if(keys > 0) { return true; }
        return false;
    }
    #endregion


    #region functions
    private void Flip()
    {
        FacingDirection *= -1;
        transform.Rotate(0f, 180f, 0f);
    }

    public void InitValues()
    {
        grounded = CheckIfGrounded();
        underCeiling = CheckForCeiling();
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;
        anim = GetComponent<Animator>();
        playerData.currentHealth = playerData.maxHealth; //have to reset health otherwise if player gets hit, currentHealth is permanently lowered

        dashesLeft = maxDashes;

        if (DashDirectionIndicator != null)
        {
            DashDirectionIndicator.gameObject.SetActive(false);
        }
    }

    public void TakeDamage(int  damage)
    {
        playerData.currentHealth -= damage;

        if (playerData.currentHealth <= 0)
        {
            Debug.Log("Player died");
            SceneManager.LoadScene("GameOver");
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
    #endregion



    #region Input Handlers
    public void OnMove(InputAction.CallbackContext value)
    {
        currentMoveInputX = value.ReadValue<Vector2>().x;
        if (!isDashing)
        {
            float x = Mathf.Round(currentMoveInputX) * moveSpeed;
            CheckIfShouldFlip((int)currentMoveInputX);
            rb.linearVelocity = new Vector2(x, rb.linearVelocity.y);
        }
    }

    public void OnJump(InputAction.CallbackContext value)
    {
        if (value.started) anim.SetTrigger("Jump");
        if (value.performed && grounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpSpeed);
        }
    }

    public void OnDashInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            anim.SetTrigger("Dashing");
            dashInput = true;
            dashInputStop = false;
        }
        else if (context.canceled)
        {
            dashInputStop = true;
        }
    }

    public void OnDashDirectionInput(InputAction.CallbackContext context)
    {
        RawDashDirectionInput = context.ReadValue<Vector2>();
        if (cam != null) { RawDashDirectionInput = cam.ScreenToWorldPoint((Vector3)RawDashDirectionInput) - transform.position; }

        dashDirectionInput = Vector2Int.RoundToInt(RawDashDirectionInput.normalized);
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            HandleAttack();
        }
    }
    #endregion

    #region Combat Logic
    private void HandleAttack()
    {
        // Can't attack if dashing or if on cooldown
        if (isDashing || Time.time < nextAttackTime) return;

        // Play animation
        if (anim != null) anim.SetTrigger("Attack");

        // Set cooldown
        nextAttackTime = Time.time + (1f / attackRate);

        // Detect enemies
        if (attackPoint != null)
        {
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

            foreach (Collider2D enemy in hitEnemies)
            {
                Debug.Log("Hit enemy: " + enemy.name);
            }
        }
        else
        {
            Debug.LogWarning("Attack Point is not assigned in the Inspector!");
        }
    }
    #endregion


    #region Dash Logic
    private void HandleDash()
    {
        if (dashInput)
        {
            dashInput = false; 

            if (canDash && !isDashing && dashesLeft > 0) {StartDash();}
        }

        if (isHoldingDash)
        {
            if (dashDirectionInput != Vector2.zero)
            {
                dashDirection = dashDirectionInput;
                dashDirection.Normalize();
            }

            if (DashDirectionIndicator != null)
            {
                float angle = Vector2.SignedAngle(Vector2.right, dashDirection);
                DashDirectionIndicator.rotation = Quaternion.Euler(0f, 0f, angle - 45f);
                DashDirectionIndicator.gameObject.SetActive(true);
            }

            if (dashInputStop || Time.unscaledTime >= dashStartTime + playerData.maxHoldTime)
            {
                isHoldingDash = false;
                Time.timeScale = 1f;
                dashStartTime = Time.time;
                rb.linearDamping = playerData.drag;
                rb.linearVelocity = dashDirection * playerData.dashVelocity;
                isDashing = true;
                canDash = false;
                CheckIfShouldFlip(Mathf.RoundToInt(dashDirection.x));

                if (DashDirectionIndicator != null){ DashDirectionIndicator.gameObject.SetActive(false); }
            }
        }

        if (isDashing)
        {
            if (Time.time >= dashStartTime + playerData.dashTime)
            {
                rb.linearDamping = 0f;
                isDashing = false;
                lastDashTime = Time.time;

                float x = Mathf.Round(currentMoveInputX) * moveSpeed;
                rb.linearVelocity = new Vector2(x, rb.linearVelocity.y);
            }
        }

        if (!canDash && Time.time >= lastDashTime + playerData.dashCooldown)
        {
            canDash = true;
        }
    }

    private void StartDash()
    {
        isHoldingDash = true;
        dashDirection = Vector2.right * FacingDirection;
        Time.timeScale = playerData.holdTimeScale;
        dashStartTime = Time.unscaledTime;
        dashesLeft--;
    }
    #endregion
}
