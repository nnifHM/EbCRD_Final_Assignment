using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //scene instance objects
    public Rigidbody2D body;
    public BoxCollider2D groundCheck;
    public LayerMask groundMask;
    
    //movement properties
    public float acceleration;
    //[Range(0f, 1f)]
    public float groundDecay;
    public float maxXSpeed;

    public float jumpspeed;

    //Dash properties
    public float dashSpeed = 20f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;
    private bool isDashing = false;
    private bool canDash = true;

    public ParticleSystem Particles;
    
    //variables
    public bool grounded;
    float xInput;       
    float yInput;

    private bool hasJumped = false;
    //reference for Platform generations script
    public PlatformGenerator platformGenerator;

    public Animator animator;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        platformGenerator = FindAnyObjectByType<PlatformGenerator>();
    }

    void Update()
    {
        CheckInput();
        HandleJump();

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }

        animator.SetFloat("Speed", Mathf.Abs(body.velocity.x));
    }

    void FixedUpdate()
    {
        CheckGround();
        ApplyFriction();
        HandleXMovement(); 
    }

    void CheckInput()
    {
        //We want to access the current value of the axis, Axis are defined in Project Settings > Input Manager > Axes
        xInput = Input.GetAxis("Horizontal");
        yInput = Input.GetAxis("Vertical");
    }

    void HandleXMovement()
    {
        if (Mathf.Abs(xInput) > 0 && !isDashing)
        {
            //increment velocity by our accelleration, then clamp within max
            float increment = xInput * acceleration;
            float newSpeed = Mathf.Clamp(body.velocity.x + increment, -maxXSpeed, maxXSpeed);
            body.velocity = new Vector2(newSpeed, body.velocity.y); //This basically means "if anything is pressed on X direction, only update the x component of the velocity and let y component what it already is

            FaceInput();
        }
    }

    void FaceInput()
    {
        float direction = Mathf.Sign(xInput);
        transform.localScale = new Vector3(direction, 1, 1);
    }

    void HandleJump()
    {
        if (Input.GetButtonDown("Jump") && grounded)
        {
            body.velocity = new Vector2(body.velocity.x, jumpspeed);

            platformGenerator.GenerateNextPlatform();

            grounded = false;

            Debug.Log("Setting Bool IsJumping to true");
            animator.SetBool("IsJumping", true);
        }
    }


    void CheckGround()
    {
        grounded = Physics2D.OverlapBox(groundCheck.bounds.center, groundCheck.bounds.size, 0f, groundMask) != null;

        animator.SetBool("IsJumping", !grounded);
    }

    void ApplyFriction()
    {
        if(grounded && xInput == 0 && body.velocity.y <= 0)
        {
            body.velocity *= groundDecay;
        }
    }

    IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravitiy = body.gravityScale;
        body.gravityScale = 0;
        body.velocity = new Vector2(transform.localScale.x * dashSpeed, 0);
        Particles.Play();

        yield return new WaitForSeconds(dashDuration);
        
        body.gravityScale = originalGravitiy;
        isDashing = false;

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
}
