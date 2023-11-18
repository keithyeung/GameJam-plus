using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;


public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float climbingSpeed = 1f;
    public Transform groundCheck;
    public LayerMask groundLayer;

    public bool isFacingRight;
    public bool isGrounded;

    private Rigidbody2D rb;
    private Vector2 moveInput;

    //Ladder
    private bool climbable;
    public bool isClimbing;

    //Tutorial UI
    public bool pickAndThrowTutorial;
    public bool interactTutorial;
    public GameObject pickUpText;
    public GameObject throwText;
    public GameObject interactText;
    public LayerMask interactiveLayer;

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



    private float walkTimer = 0;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        m_input = new CustomInput();
        states = playerStates.defaultState;
        isFacingRight = true;
    }

    private void Start()
    {
        transform.position = GameManager.instance._playerRespawnPos;
        interactiveLayer = 9;
        pickAndThrowTutorial = false;
        interactTutorial = false;
        pickUpText.SetActive(false);
        throwText.SetActive(false);
        interactText.SetActive(false);
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
        
    }

    private void FixedUpdate()
    {
        StateManager();
        
    }

    public void Climb(InputAction.CallbackContext context)
    {
        if (GetComponent<PlayerItems>()._heldObject != null) { return; }
        Vector2 tempVec2 = context.ReadValue<Vector2>();

        if (context.started && climbable)
        {
            isClimbing = true;
            rb.gravityScale = 0f;
            GetComponent<BoxCollider2D>().isTrigger = true;
            FindAnyObjectByType<PlayerAnimation>().SwitchingSpritesToClimbing();
            

            AudioManager.instance.Play("Climbing");
        }
        else if (context.canceled && !climbable)
        {
            isClimbing = false;
            rb.gravityScale = 1f; // Restore gravity when climbing is canceled
            AudioManager.instance.Stop("Climbing");

        }

        if (isClimbing)
        {
            if (rb.velocity.y == 0 && tempVec2.y != 0)
            {
                AudioManager.instance.Play("Climbing");
            }
            else if (rb.velocity.y != 0 && tempVec2.y == 0)
            {
                AudioManager.instance.Stop("Climbing");
            }

            rb.velocity = new Vector2(rb.velocity.x, tempVec2.y * climbingSpeed);
                        
        }
    }

    public void IsGrounded()
    {
        // Check if the player is grounded
        bool grounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
        if (isGrounded == false && grounded == true)
        {
            AudioManager.instance.Play("Landing");
        }
        isGrounded = grounded;
    }

    public void MovementAndRotation()
    {
        if (rb.velocity.x == 0 && moveInput.x != 0)
        {
            if (onBrittle)
            {
                AudioManager.instance.Play("WalkingOnBrittleGround");

            }
            else
            {
                AudioManager.instance.Play("Walking");

            }
        }
        else if (rb.velocity.x != 0 && moveInput.x == 0)
        {
            AudioManager.instance.Stop("Walking");
        }


        if (rb.velocity.x != 0)
        {
            walkTimer += Time.deltaTime;
            if (walkTimer > 10)
            {
                walkTimer = 0;
                AudioManager.instance.Play("VoiceWalking");
            }
        }

        // Apply movement
        float moveX = moveInput.x * moveSpeed * Time.deltaTime;
        rb.velocity = new Vector2(moveX, rb.velocity.y);
        if (moveX > 0f || moveX < 0f)
        {
            FindAnyObjectByType<PlayerAnimation>().WalkingAni();
        }
        else
        {
            FindAnyObjectByType<PlayerAnimation>().Idle();
        }

        if (!isFacingRight && moveInput.x > 0f)
        {
            Flip();
        }
        else if (isFacingRight && moveInput.x < 0f)
        {
            Flip();
        }
    }

    bool onBrittle;
    public void OnBrittleGround(bool onOrOff)
    {
        if (onOrOff)
        {
            if (rb.velocity.x != 0)
            {
                AudioManager.instance.Stop("Walking");
                AudioManager.instance.Play("WalkingOnBrittleGround");

                onBrittle = true;
            }
        }
        else
        {
            if (rb.velocity.x != 0)
            {
                AudioManager.instance.Stop("WalkingOnBrittleGround");
                AudioManager.instance.Play("Walking");

                onBrittle = false;
            }
        }
        
    }


    public void Jump(InputAction.CallbackContext context)
    {
        if(GetComponent<PlayerItems>()._heldObject != null) { return; }
        if(context.performed && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);

            AudioManager.instance.Play("Jump");
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
        if (collision.gameObject.tag == "Ladder")
        {
            if(GetComponent<PlayerItems>()._heldObject == null)
            {
                climbable = true;
            }
        }
        else if (collision.gameObject.tag == "Worm")
        {
            AudioManager.instance.Play("Death");
            GameManager.instance.Restart();
        }
        else if (collision.gameObject.tag == "PickUpTrigger")
        {
            if (!pickAndThrowTutorial)
            {
                pickUpText.SetActive(true);
            }
        }
        
        if (collision.gameObject.name == "Button" && !interactTutorial)
        {
            
            interactText.SetActive(true);    
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ladder")
        {
            climbable = false;
            isClimbing = false;
            rb.gravityScale = 1f;
            GetComponent<BoxCollider2D>().isTrigger = false;
            GetComponent<PlayerAnimation>().SwitchingSpritesToNormal();
        }
        else if (collision.gameObject.tag == "PickUpTrigger")
        {
            pickUpText.SetActive(false);
        }
        else if (collision.gameObject.name == "Button")
        {
            interactText.SetActive(false);
        }
        //Debug.Log(collision.gameObject.name);
    }
}
