using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;


public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public Transform groundCheck;
    public LayerMask groundLayer;

    private bool isFacingRight;
    private bool isGrounded;
    private Rigidbody2D rb;
    private Vector2 moveInput;

    //Input Map
    private CustomInput m_input = null;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        m_input = new CustomInput();
    }

    private void OnEnable()
    {
        m_input.Enable();
    }

    private void OnDisable()
    {
        m_input.Disable();
    }

    private void Update()
    {
        IsGrounded();
        MovementAndRotation();
    }

    public void IsGrounded()
    {
        // Check if the player is grounded
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    public void MovementAndRotation()
    {
        // Apply movement
        float moveX = moveInput.x * moveSpeed * Time.deltaTime;
        rb.velocity = new Vector2(moveX, rb.velocity.y);

        if (!isFacingRight && moveInput.x > 0f)
        {
            Flip();
        }
        else if (isFacingRight && moveInput.x < 0f)
        {
            Flip();
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if(context.performed && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        if(context.canceled && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    public void Move(InputAction.CallbackContext context)
    {
        moveInput.x = context.ReadValue<Vector2>().x;
    }
}
