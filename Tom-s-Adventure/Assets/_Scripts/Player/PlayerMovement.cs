using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator anim;

    [Range(0, 1)][SerializeField] private float m_SlideSpeed = .36f;   // How much to smooth out the movement
    [Range(0, .3f)][SerializeField] private float m_MovementSmoothing = .05f;   // How much to smooth out the movement
    [SerializeField] int totalJumps;
    [SerializeField] private bool m_AirControl = false;                         // Whether or not a player can steer while jumping;

    private float dirX = 0f;
    private bool facingRight = true;

    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpHeight = 15f;
    [SerializeField] private float gravityScale = 2f;

    // private enum MovementState { idle, running, jumping, falling }

    // ground attributes
    private bool grounded = true;
    const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
    [SerializeField] public Transform groundCheck;
    [SerializeField] private LayerMask m_WhatIsGround;

    bool jump = false;
    int availableJumps;
    bool multipleJump;
    bool coyoteJump;
    private Vector3 m_Velocity = Vector3.zero;

    private void Awake()
    {
        availableJumps = totalJumps;
    }
    // Start is called before the first frame update
    private void Start()
    {
        this.rb = PlayerCtrl.Ins.GetComponent<Rigidbody2D>();
        this.anim = PlayerCtrl.Ins.Model.GetComponent<Animator>();
        StartCoroutine(Respawn());
    }

    protected bool CanMove()
    {
        return rb.bodyType == RigidbodyType2D.Dynamic && !GameManager.Ins.pause && !PlayerStatus.Ins.IsDead && !GameManager.Ins.levelCompleted;
    }

    IEnumerator Respawn()
    {
        rb.bodyType = RigidbodyType2D.Static;
        yield return new WaitForSeconds(.1f);
        anim.SetTrigger("respawning");

    }
    public void ApplyGravity()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = gravityScale;
    }
    // Update is called once per frame
    private void Update()
    {
        if (!CanMove()) return;
        HandlePCInput();

        UpdateAnimationState();
    }

    protected void HandlePCInput()
    {
        dirX = Input.GetAxisRaw("Horizontal");

        anim.SetFloat("yVelocity", rb.velocity.y);

        if (Input.GetButtonDown("Jump") && totalJumps > 0)
        {
            jump = true;
        }
    }
    private void Move()
    {
        Vector2 targetVelocity = new Vector2(dirX * moveSpeed, rb.velocity.y);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

        if (jump)
        {
            Jump();
        }
    }

    void Jump()
    {
        AudioManager.Ins.PlaySFX(EffectSound.JumpSound);
        if (grounded)
        {
            multipleJump = true;
            availableJumps--;

            // rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
            rb.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
            anim.SetBool("grounded", false);
        }
        else
        {
            if (coyoteJump && availableJumps > 0)
            {
                coyoteJump = false;
                multipleJump = true;
                availableJumps--;

                rb.AddForce(Vector2.up * jumpHeight * 1.2f, ForceMode2D.Impulse);
            }

            else if (multipleJump && availableJumps > 0)
            {
                availableJumps--;

                rb.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
            }
        }
        jump = false;
    }

    private void FixedUpdate()
    {
        if (!CanMove()) return;

        bool wasGrounded = grounded;
        bool onAir = true;
        grounded = false;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, k_GroundedRadius, m_WhatIsGround);
        if (colliders.Length > 0)
        {
            grounded = true;
            onAir = false;
            if (!wasGrounded)
            {
                anim.SetBool("grounded", true);
                availableJumps = totalJumps;
                multipleJump = false;
            }
        }
        else anim.SetBool("grounded", false);

        if (onAir)
        {
            if (wasGrounded)
                StartCoroutine(CoyoteJumpDelay());
        }

        if (rb.velocity.y < 0)
        {
            rb.gravityScale = gravityScale * 2;
        }
        else
        {
            rb.gravityScale = gravityScale;
        }

        Move();
    }

    IEnumerator CoyoteJumpDelay()
    {
        coyoteJump = true;
        yield return new WaitForSeconds(0.2f);
        coyoteJump = false;
    }

    private void UpdateAnimationState()
    {
        if (dirX > 0)
        {
            anim.SetBool("running", true);
            if (!facingRight) Flip();
        }
        else if (dirX < 0)
        {
            anim.SetBool("running", true);
            if (facingRight) Flip();
        }
        else
        {
            anim.SetBool("running", false);
        }

    }
    private void Flip()
    {
        facingRight = !facingRight;
        transform.parent.Rotate(0f, 180f, 0f);
    }
}
