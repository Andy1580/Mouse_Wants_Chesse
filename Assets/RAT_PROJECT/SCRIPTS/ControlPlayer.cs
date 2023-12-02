using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ControlPlayer : MonoBehaviour
{
    [Header("Movement")]
    private float moveSpeed;
    public float walkSpeed;
    public float sprintSpeed;
    private float desiredMoveSpeed;
    private float lastDesiredMoveSpeed;
    public float slideSpeed;
    public float wallrunSpeed;
    public float climbSpeed;
    public float airMinSpeed;

    public float speedIncreaseMultiplier;
    public float slopeIncreaseMultiplier;

    public float groundDrag;

    //[Header("Jumping")]
    //public float jumpForce;
    //public float jumpCooldown;
    public float airMultiplier;
    //bool readyToJump;

    [Header("Stealthing")]
    public float stealthSpeed;

    [Header("Keybinds")]
    //public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;
    public KeyCode stealthKey = KeyCode.LeftControl;
    public KeyCode thorw = KeyCode.X;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    public bool grounded;

    [Header("Slope Handling")]
    public float maxSlopeAngle;
    private RaycastHit slopeHit;
    private bool exitingSlope;


    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;
    public Image StaminaBar;
    public Image IndicadorRun;
    public Image IndicadorSig;
    public float estamina, MaxStamina;
    public float estaminareg;
    public float runcost;
    public float stealcost;

    public GameObject item;
    public float fuerza;
    public Animator animator;

    [SerializeField] public Transform start;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bola")
        {
            item = other.GameObject();
        }
    }

    private void Throw()
    {
        if(Input.GetKeyDown(thorw))
        {
            if(item == enabled)
            {
                item.transform.position = orientation.position + (orientation.forward * 2); ;
                item.SetActive(true);
                item.GetComponent<Rigidbody>().isKinematic = false;
                item.GetComponent<Rigidbody>().AddForce(orientation.forward * fuerza, ForceMode.Impulse);
                item.GetComponent<Rigidbody>().AddForce(orientation.up * (fuerza - 2), ForceMode.Impulse);
                item.GetComponent<Rigidbody>().useGravity = true;
                item = null;
            }

        }
    }

    public MovementState state;
    public enum MovementState
    {
        walking,
        sprinting,
        wallrunning,
        stealthing,
        climbing,
        sliding,
        air
    }

    public bool sliding;
    public bool crouching;
    public bool wallrunning;
    public bool climbing;
    public bool running;
    public bool stealth;

    private void Start()
    {
        animator = GetComponent<Animator>();
        GetComponent<Transform>().position = start.position;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        estamina = MaxStamina;
        //jump
        //readyToJump = true;
    }

    private void Update()
    {
        // ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        MyInput();
        SpeedControl();
        StateHandler();
        Throw();

        // handle drag
        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;

        if (estamina > 0 && !stealth)
            state = MovementState.sprinting;
        else
        {
            IndicadorRun.gameObject.SetActive(false);
            running = false;
        }
        if (estamina > 0 && !running)
            state = MovementState.stealthing;
        else
        {
            IndicadorSig.gameObject.SetActive(false);
            stealth = false;
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // when to jump
        //if (Input.GetKey(jumpKey) && readyToJump && grounded)
        //{
        //    readyToJump = false;

        //    Jump();

        //    Invoke(nameof(ResetJump), jumpCooldown);
        //}

        // start stealth
        if (Input.GetKeyDown(stealthKey))
        {
            stealth = true;
            IndicadorSig.gameObject.SetActive(true);
            //rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
        }

        // stop stealth
        if (Input.GetKeyUp(stealthKey))
        {
            stealth = false;
            IndicadorSig.gameObject.SetActive(true);
        }
        // start run
        if (Input.GetKeyDown(sprintKey))
        {
            running = true;
            IndicadorRun.gameObject.SetActive(true);
            //rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
        }

        // stop run
        if (Input.GetKeyUp(sprintKey))
        {
            running = false;
            IndicadorRun.gameObject.SetActive(false);
        }
    }

    private void StateHandler()
    {
        // Mode - Climbing
        if(climbing)
        {
            state = MovementState.climbing;
            desiredMoveSpeed = climbSpeed;
            animator.SetBool("IsCiming", true);
        }
        // Mode - Stealing
        else if (Input.GetKey(stealthKey))
        {
            if(estamina > 0) {
                state = MovementState.stealthing;
                stealth = true;
                moveSpeed = stealthSpeed;
                IndicadorSig.gameObject.SetActive(true);
                estamina -= stealcost * Time.deltaTime;
                if (estamina < 0) estamina = 0;
                StaminaBar.fillAmount = estamina / MaxStamina;
                animator.SetBool("IsStealth", true);
            }

        }

        // Mode - Sprinting
        else if (grounded && Input.GetKey(sprintKey))
        {
            if(estamina >= 0)
            {
                state = MovementState.sprinting;
                running = true;
                moveSpeed = sprintSpeed;
                IndicadorRun.gameObject.SetActive(true);
                estamina -= runcost * Time.deltaTime;
                if (estamina < 0) estamina = 0;
                StaminaBar.fillAmount = estamina / MaxStamina;
                animator.SetBool("IsRunning", true);
            }

        }

        // Mode - Walking
        else if (grounded)
        {
            state = MovementState.walking;
            moveSpeed = walkSpeed;
            animator.SetBool("IsWalking", true);
        }

        // Mode - Air
        else
        {
            state = MovementState.air;
            animator.SetBool("IsWalking", true);
        }
    }

    private void MovePlayer()
    {
        // calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // on slope
        if (OnSlope() && !exitingSlope)
        {
            rb.AddForce(GetSlopeMoveDirection() * moveSpeed * 20f, ForceMode.Force);

            if (rb.velocity.y > 0)
                rb.AddForce(Vector3.down * 80f, ForceMode.Force);
        }

        // on ground
        else if (grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

         //in air
        else if (!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);

        // turn gravity off while on slope
        rb.useGravity = !OnSlope();
    }

    private void SpeedControl()
    {
        // limiting speed on slope
        if (OnSlope() && !exitingSlope)
        {
            if (rb.velocity.magnitude > moveSpeed)
                rb.velocity = rb.velocity.normalized * moveSpeed;
        }

        // limiting speed on ground or in air
        else
        {
            Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            // limit velocity if needed
            if (flatVel.magnitude > moveSpeed)
            {
                Vector3 limitedVel = flatVel.normalized * moveSpeed;
                rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
            }
        }
    }

    //private void Jump()
    //{
    //    exitingSlope = true;

    //    // reset y velocity
    //    rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

    //    rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    //}
    //private void ResetJump()
    //{
    //    readyToJump = true;

    //    exitingSlope = false;
    //}

    private bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.3f))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }

        return false;
    }

    private Vector3 GetSlopeMoveDirection()
    {
        return Vector3.ProjectOnPlane(moveDirection, slopeHit.normal).normalized;
    }
    public void Estamina()
    {
        if (estamina <= MaxStamina)
            estamina += estaminareg * Time.deltaTime;
        StaminaBar.fillAmount = estamina / MaxStamina;
        //transform.position = refugio.position;

    }
}
