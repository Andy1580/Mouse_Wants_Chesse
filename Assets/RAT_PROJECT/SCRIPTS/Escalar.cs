using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escalar : MonoBehaviour
{
    [Header("References")]
    public Transform orientation;
    public Rigidbody rb;
    public ControlRaton pm;
    public LayerMask whatIsWall;
    public Transform esfera;
    //public Transform raton;
    

    [Header("Climbing")]
    public float climbSpeed;
    public float maxClimbTime;
    private float climbTimer;

    public bool climbing;

    [Header("ClimbJumping")]
    public float climbJumpUpForce;
    public float climbJumpBackForce;

    public KeyCode jumpKey = KeyCode.Space;
    public int climbJumps;
    private int climbJumpsLeft;

    [Header("Detection")]
    public float detectionLength;
    public float sphereCastRadius;
    public float maxWallLookAngle;
    private float wallLookAngle;

    private RaycastHit frontWallHit;
    private bool wallFront;

    private Transform lastWall;
    private Vector3 lastWallNormal;
    public float minWallNormalAngleChange;

    [Header("Exiting")]
    public bool exitingWall;
    public float exitWallTime;
    private float exitWallTimer;

    private void Update()
    {
        WallCheck();
        StateMachine();

        if (climbing && !exitingWall) ClimbingMovement();
    }
    
    private void StateMachine()
    {
        // State 1 - Climbing
        if (wallFront && Input.GetKey(KeyCode.W) && wallLookAngle < maxWallLookAngle && !exitingWall)
        {
            if (!climbing && climbTimer > 0) StartClimbing();
            //raton.rotation = Quaternion.Euler(-90f, 0, 0);
            //camara.rotation = Quaternion.Euler(-90f, 0, 0);

            // timer
            if (climbTimer > 0) climbTimer -= Time.deltaTime;
            if (climbTimer < 0) StopClimbing();
            
        }

        // State 2 - Exiting
        else if (exitingWall)
        {
            
            if (climbing) StopClimbing();
            
            if (exitWallTimer > 0) exitWallTimer -= Time.deltaTime;
            if (exitWallTimer < 0) exitingWall = false;
        }

        // State 3 - None
        else
        {
            
            if (climbing) StopClimbing();
            
        }

        if (wallFront && Input.GetKeyDown(jumpKey) && climbJumpsLeft > 0) ClimbJump();
    }

    private void WallCheck()
    {
        wallFront = Physics.SphereCast(transform.position, sphereCastRadius, orientation.forward, out frontWallHit, detectionLength, whatIsWall);

        wallLookAngle = Vector3.Angle(orientation.forward, -frontWallHit.normal);

        bool newWall = frontWallHit.transform != lastWall || Mathf.Abs(Vector3.Angle(lastWallNormal, frontWallHit.normal)) > minWallNormalAngleChange;

        if ((wallFront && newWall) || pm.grounded)

            {
            climbTimer = maxClimbTime;
            climbJumpsLeft = climbJumps;
        }
    }

    private void StartClimbing()
    {
        climbing = true;
        pm.climbing = false;

        lastWall = frontWallHit.transform;
        lastWallNormal = frontWallHit.normal;
        

        /// idea - camera fov change
    }

    private void ClimbingMovement()
    {
        rb.velocity = new Vector3(rb.velocity.x, climbSpeed, rb.velocity.z);

        /// idea - sound effect
    }

    private void StopClimbing()
    {
        climbing = false;
        pm.climbing = false;
        //raton.rotation = Quaternion.Euler(0, 0, 0);
        //camara.rotation = Quaternion.Euler(-90f, 0, 0);
        /// idea - particle effect
        /// idea - sound effect
    }

    private void ClimbJump()
    {
        exitingWall = true;
        exitWallTimer = exitWallTime;

        Vector3 forceToApply = transform.up * climbJumpUpForce + frontWallHit.normal * climbJumpBackForce;

        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(forceToApply, ForceMode.Impulse);

        climbJumpsLeft--;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, sphereCastRadius);
        //Gizmos.DrawWireSphere(transform.position, viewDistance);
    }
}
