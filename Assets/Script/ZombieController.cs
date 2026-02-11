using UnityEngine;

public class ZombieController : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float jumpForce = 10f;
    public float attackRange = 1f;
    public float attackCooldown = 1.2f;
    private int facingDir = 1;

	public Transform player;
	public Transform groundCheck;
    public Transform wallCheck;
    public Transform attackPoint;

    public LayerMask groundLayer;
    public LayerMask obstacleLayer;
    public LayerMask playerLayer;

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sr;

    private bool isGrounded;
    private bool isAttacking;
    private bool isDead;
    private float attackTimer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

		FindPlayer();
	}

    void Update()
    {
		if (player == null)
		{
			FindPlayer();
			return;
		}

		if (GameStateManager.Instance != null && GameStateManager.Instance.isDesignMode)
		{
			rb.linearVelocity = Vector2.zero;     
			animator.SetBool("isRunning", false);
			animator.SetBool("isAttacking", false);
			return;
		}

		if (isDead) return;

        // ===== CHECK GROUND =====
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
        animator.SetBool("isGrounded", isGrounded);
        animator.SetFloat("yVelocity", rb.linearVelocity.y);

        // ===== FACE PLAYER =====
        if (player.position.x > transform.position.x)
        {
            facingDir = 1;
            sr.flipX = false;
        }
        else
        {
            facingDir = -1;
            sr.flipX = true;
        }
        wallCheck.localPosition = new Vector3(
            Mathf.Abs(wallCheck.localPosition.x) * facingDir,
            wallCheck.localPosition.y,
            wallCheck.localPosition.z
        );

        attackPoint.localPosition = new Vector3(
            Mathf.Abs(attackPoint.localPosition.x) * facingDir,
            attackPoint.localPosition.y,
            attackPoint.localPosition.z
        );
        // ===== ATTACK CHECK =====
        bool playerInRange = Physics2D.OverlapCircle(
            attackPoint.position,
            attackRange,
            playerLayer
        );

        attackTimer -= Time.deltaTime;

		if (playerInRange && !isAttacking && attackTimer <= 0)
		{
			isAttacking = true;
			animator.SetBool("isAttacking", true);
			rb.linearVelocity = Vector2.zero;
			attackTimer = attackCooldown;
		}

		animator.SetBool("isAttacking", isAttacking);

        // ===== MOVE =====
        if (!isAttacking)
        {
            float dir = player.position.x > transform.position.x ? 1 : -1;
            rb.linearVelocity = new Vector2(dir * moveSpeed, rb.linearVelocity.y);
            animator.SetBool("isRunning", true);
        }
        else
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        }

        // ===== JUMP WHEN HIT WALL =====
        bool hitWall = Physics2D.OverlapCircle(wallCheck.position, 0.1f, obstacleLayer);

        if (hitWall && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }
	void FindPlayer()
	{
		GameObject p = GameObject.FindGameObjectWithTag("Player");
		if (p != null)
			player = p.transform;
	}
	public void EndAttack()
	{
		isAttacking = false;
		animator.SetBool("isAttacking", false);
	}

	public void DamagePlayer()
	{
		Collider2D hit = Physics2D.OverlapCircle(
			attackPoint.position,
			attackRange,
			playerLayer
		);

		if (hit != null)
		{
			PlayerController player = hit.GetComponent<PlayerController>();
			if (player != null)
			{
				player.Die();
			}
		}
	}
	private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Trap"))
        {
            Die();
        }
    }

	public void Die()
	{
		if (isDead) return;

		isDead = true;
		rb.linearVelocity = Vector2.zero;
		rb.bodyType = RigidbodyType2D.Kinematic;
		animator.SetTrigger("die");

		if (ZombieManager.Instance != null)
			ZombieManager.Instance.OnZombieDead();

		Destroy(gameObject, 1.2f);
	}


	// ===== DEBUG GIZMOS =====
	void OnDrawGizmosSelected()
    {
        if (groundCheck)
            Gizmos.DrawWireSphere(groundCheck.position, 0.2f);

        if (wallCheck)
            Gizmos.DrawWireSphere(wallCheck.position, 0.1f);

        if (attackPoint)
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
