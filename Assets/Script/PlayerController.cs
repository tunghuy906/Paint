using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public float moveSpeed = 7f;
	public float jumpForce = 12f;

	public Transform groundCheck;
	public float groundCheckRadius = 0.2f;
	public LayerMask groundLayer;

	private Rigidbody2D rb;
	private SpriteRenderer spriteRenderer;
	private Animator animator;
	private Collider2D col;

	private float moveInput;
	private bool isGrounded;
	private bool isDead;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		animator = GetComponent<Animator>();
		col = GetComponent<Collider2D>();
	}

	void Update()
	{
		if (GameStateManager.Instance != null && GameStateManager.Instance.isDesignMode)
			return;

		if (isDead) return;

		// ===== GROUND CHECK =====
		isGrounded = Physics2D.OverlapCircle(
			groundCheck.position,
			groundCheckRadius,
			groundLayer
		);
		animator.SetBool("isGrounded", isGrounded);

		// ===== MOVE =====
		moveInput = Input.GetAxisRaw("Horizontal");
		rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

		if (moveInput > 0) spriteRenderer.flipX = false;
		else if (moveInput < 0) spriteRenderer.flipX = true;

		animator.SetBool("isMoving", moveInput != 0);

		// ===== JUMP =====
		if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
		{
			rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
			animator.SetTrigger("jump");
		}
	}

	public void Die()
	{
		if (isDead) return;

		isDead = true;
		rb.linearVelocity = Vector2.zero;
		rb.bodyType = RigidbodyType2D.Kinematic;
		animator.SetTrigger("die");

		StartCoroutine(DeathRoutine());
	}
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Trap"))
		{
			Die();
		}
	}
	IEnumerator DeathRoutine()
	{
		yield return new WaitForSeconds(1f);

		if (GameUIManager.Instance != null)
			GameUIManager.Instance.ShowGameOver();

		gameObject.SetActive(false);
	}
	void OnDrawGizmosSelected()
	{
		if (groundCheck == null) return;
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
	}
}
