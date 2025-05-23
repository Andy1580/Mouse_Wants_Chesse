using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ControlRaton : MonoBehaviour
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
    //public KeyCode stealthKey = KeyCode.LeftControl;
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

    public Rigidbody rb;
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
    public Escalar EscalarX;
    public Canvas estambre;
    public Image bola;
    public Transform lanzar;
    public Pause pausa1;
    

    [SerializeField] public Transform start;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bola")
        {
            item = other.GameObject();
            //item.gameObject.tag = "Ball";
            //estambre.AddComponent<Image>();
            //estambre.GetComponentInChildren<Image>().GetComponentInChildren<Image>().enabled=true;

        }
        if (other.gameObject.layer == 9)
        {
            if (InventarioRaton.HayEspacios)
            {
                Item item = other.GetComponent<Item>();
                InventarioRaton.Agregarltem(item.ItemSo);
                if (item.gameObject.CompareTag("Queso"))
                {
                    _quesos++;
                }
                contador.text = _quesos.ToString();
                other.gameObject.SetActive(false);
                //Destroy(other.gameObject);
            }


        }
        if (other.gameObject.layer == 8)
        {
            if (InventarioRaton.HayEspacios)
            {
                Item item = other.GetComponent<Item>();
                InventarioRaton.Agregarltem(item.ItemSo);
                other.gameObject.SetActive(false);
                //Destroy(other.gameObject);
            }

        }
    }

    private void Throw()
    {
        if (Input.GetKeyDown(thorw))
        {
            if (item == enabled)
            {
                item.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                item.GetComponent<Levitation>().enabled = false;
                //item.transform.rotation = lanzar.rotation;
                item.transform.position = lanzar.position + (lanzar.forward * 2); ;
                item.SetActive(true);
                item.GetComponent<Rigidbody>().isKinematic = false;
                //item.GetComponent<Rigidbody>().AddForce(lanzar.forward * fuerza, ForceMode.Impulse);
                //item.GetComponent<Rigidbody>().AddForce(lanzar.up * 10, ForceMode.Impulse);
                item.GetComponent<Rigidbody>().useGravity = true;
                //item.gameObject.tag = "Bola";
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
        //rb = GetComponent<Rigidbody>(); 2/12/23
        rb.freezeRotation = true;
        estamina = MaxStamina;
        //jump
        //readyToJump = true;
        
        
    }

    private void Update()
    {
        // ground check
        //grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
        grounded = true;
        climbing = EscalarX.climbing;
        Debug.Log(EscalarX.climbing);
        MyInput();
        SpeedControl();
        StateHandler();
        if (pausa1.pauseCanvas == false)
        {
            Throw();
        }
            Estamina();
        Dead();

        if(stealth == true)
        {
            animator.SetBool("IsStealth", true);
        }
        else
        {
            animator.SetBool("IsStealth", false);
        }

        // handle drag
        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;

        

        //if (estamina > 0 && !stealth)
        //    state = MovementState.sprinting;
        //else
        //{
        //    IndicadorRun.gameObject.SetActive(false);
        //    running = false;
        //}
        //if (estamina > 0 && !running)
        //    state = MovementState.stealthing;
        //else
        //{
        //    IndicadorSig.gameObject.SetActive(false);
        //    stealth = false;
        //}
        if(running == true && estamina > 0 && climbing == false)
        {
            animator.SetBool("IsRunning", true);
            IndicadorRun.gameObject.SetActive(true);
        }
        else
        {
            animator.SetBool("IsRunning", false);
            IndicadorRun.gameObject.SetActive(false);
        }
        if (climbing == true)
        {
            animator.SetBool("IsClimbing", true);
        }
        else
        {
            animator.SetBool("IsClimbing", false);
        }

        //if (estamina>0)
        //{
        //    if(running == true)
        //    {
        //        IndicadorRun.gameObject.SetActive(true);
        //    }

        //    if(stealth == true)
        //    {
        //        IndicadorSig.gameObject.SetActive(true);
        //    }

        //}


    }

    private void FixedUpdate()
    {
        MovePlayer();
    }
    public bool moviendose;
    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetAxisRaw("Horizontal") != 0 && running == false && stealth == false && climbing == false)
        {
            moviendose = true;
            animator.SetBool("IsWalking", true);
        }

       else if (Input.GetAxisRaw("Vertical") != 0 && running == false && stealth == false && climbing == false)
        {
            moviendose = true;
            animator.SetBool("IsWalking", true);
        }
        //if (Input.GetAxisRaw("Horizontal") != 0 && running == false && stealth == false && climbing == true)
        //{
        //    moviendose = false;
        //    animator.SetBool("IsWalking", false);
        //    animator.SetBool("IsClimbing", true);
        //}
        //if (Input.GetAxisRaw("Vertical") != 0 && running == false && stealth == false && climbing == true)
        //{
        //    moviendose = false;
        //    animator.SetBool("IsWalking", false);
        //    animator.SetBool("IsClimbing", true);
        //}
        else
        {
            moviendose = false;
            animator.SetBool("IsWalking", false);
        }

        


        // when to jump
        //if (Input.GetKey(jumpKey) && readyToJump && grounded)
        //{
        //    readyToJump = false;

        //    Jump();

        //    Invoke(nameof(ResetJump), jumpCooldown);
        //}


        //RECORDAR REACTIVAR MAS TARDE
        // start stealth
        //if (Input.GetKeyDown(stealthKey))
        //{
        //    stealth = true;
        //    IndicadorSig.gameObject.SetActive(true);
        //    //rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
        //}

        //// stop stealth
        //if (Input.GetKeyUp(stealthKey))
        //{
        //    stealth = false;
        //    IndicadorSig.gameObject.SetActive(false);
        //}
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
        
        if (climbing)
        {
            state = MovementState.climbing;
            desiredMoveSpeed = climbSpeed;
            
        }
        // Mode - Stealing
        //RECORDAR REACTIVAR MAS TARDE
        //else if (Input.GetKey(stealthKey))
        //{
        //    if (estamina > 0)
        //    {
        //        state = MovementState.stealthing;
        //        stealth = true;
        //        moveSpeed = stealthSpeed;
        //        IndicadorSig.gameObject.SetActive(true);
        //        estamina -= stealcost * Time.deltaTime;
        //        if (estamina < 0) estamina = 0;
        //        StaminaBar.fillAmount = estamina / MaxStamina;
                
        //    }

        //}

        // Mode - Sprinting
        else if (Input.GetKey(sprintKey) )
        {
            if (estamina > 0)
            {
                state = MovementState.sprinting;
                running = true;
                moveSpeed = sprintSpeed;
                IndicadorRun.gameObject.SetActive(true);
                estamina -= runcost * Time.deltaTime;
                if (estamina < 0) estamina = 0;
                StaminaBar.fillAmount = estamina / MaxStamina;
                
            }
            else
            {
                running = false;
                animator.SetBool("IsWalking", true);
            }
        }

        // Mode - Walking
        else if (grounded)
        {
            state = MovementState.walking;
            moveSpeed = walkSpeed;
        }

        // Mode - Air
        else
        {
            state = MovementState.air;
          
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
        if (estamina <= MaxStamina && running == false)
       
            estamina += estaminareg * Time.deltaTime;
        StaminaBar.fillAmount = estamina / MaxStamina;
        //transform.position = refugio.position;
        
        //parte integrada por ANDY
        if (estamina <= 0)
        {
            moveSpeed = walkSpeed;
            state = MovementState.walking;
        }
        else { return; }
        //termina parte integrada por ANDY
    }
    public TMP_Text contador;
    public int _quesos;

    [SerializeField] private Gato ratdead;
    private void  Dead()
    {
        if(ratdead.RatDead==true)
        {
            horizontalInput = 0;
            verticalInput = 0;
        }
        
    }
}
