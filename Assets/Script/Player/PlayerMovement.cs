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

    public bool isFacingRight;
    private bool isGrounded;
    private Rigidbody2D rb;
    private Vector2 moveInput;

    //Ladder
    private bool climbable;

    //Player States
    private enum playerStates
    {
        defaultState,
        Climb,
        Carrying,
    }

    playerStates states;

    //Input Map
    private CustomInput m_input = null;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        m_input = new CustomInput();
        states = playerStates.defaultState;
    }

    private void StateManager()
    {
        switch (states)
        {
            case playerStates.defaultState:
                MovementAndRotation();
                break;
            case playerStates.Climb:
                //Climb();
                break;
            case playerStates.Carrying:
                break;
            default:
                break;
        }
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
        //if(Keyboard.current.wKey.wasPressedThisFrame)
        //{
        //    Debug.Log("W is pressed");
        //}
    }

    private void FixedUpdate()
    {
        StateManager();
    }

    public void Climb(InputAction.CallbackContext context)
    {
        Vector2 tempVec2 = context.ReadValue<Vector2>();
        float climbSpeed = 1f;
        Debug.Log(tempVec2);
        if (context.performed && climbable)
        {
            rb.gravityScale = 0f;
            rb.velocity = new Vector2(rb.velocity.x, tempVec2.y * climbSpeed);
        }
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Ladder")
        {
            climbable = true;
            Debug.Log("Found a ladder");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ladder")
        {
            climbable = false;
            rb.gravityScale = 1f;
            Debug.Log("Left a ladder");
        }
    }
}
