using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float defaultJumpPower;
    private float jumpPower;
    private bool isJumping;
    private float moveDirectionY;

    private Transform playerCamera;
    private Rigidbody playerRB;
    private BoxCollider footCollider;
    private PlayerController playerController;

    public bool IsGrounded
    {
        get
        {
            return CheckGround();
        }
    }

    private void Awake()
    {
        playerCamera = GetComponentInChildren<PlayerThirdCamera>().transform;
        playerRB = GetComponent<Rigidbody>();
        playerController = GetComponent<PlayerController>();
    }

    private void OnEnable()
    {
        InitMovementEvent();
    }

    private void OnDisable()
    {
        DeleteMovementEvent();
    }

    private void InitMovementEvent()
    {
        playerController.OnMove += Move;
        playerController.OnJump += Jump;
        playerController.OnStructureJump += Jump;
    }

    private void DeleteMovementEvent()
    {
        playerController.OnMove -= Move;
        playerController.OnJump -= Jump;
        playerController.OnStructureJump += Jump;
    }

    private void Move(Vector2 moveDirection)
    {
        print(IsGrounded);

        if (isJumping && IsGrounded)
        {
            moveDirectionY = jumpPower;
            isJumping = false;
        }
        else if (!isJumping && IsGrounded)
        {
            moveDirectionY = 0;
        }

        if (!IsGrounded)
        {
            moveDirectionY += (Physics.gravity.y * Time.fixedDeltaTime);
        }

        Vector3 lookDirection = moveDirection;

        if (moveDirection != Vector2.zero)
        {
            lookDirection = playerCamera.TransformDirection(new(moveDirection.x, 0, moveDirection.y));
            lookDirection = new Vector3(lookDirection.x, 0, lookDirection.z).normalized;
            transform.forward = lookDirection;
        }

        //Debug.Log(speed * new Vector3(lookDirection.x, moveDirectionY, lookDirection.z));

        playerRB.velocity = speed * new Vector3(lookDirection.x, moveDirectionY, lookDirection.z);
    }

    private void Jump()
    {
        if (IsGrounded && !isJumping)
        {
            jumpPower = defaultJumpPower;
            isJumping = true;
        }
    }
    public void Jump(float jumpPower)
    {
        if (IsGrounded)
        {
            this.jumpPower = jumpPower;
            isJumping = true;
        }
    }

    public bool CheckGround()
    {
        return Physics.SphereCast(transform.position, 0.1f, Vector3.down, out var hit);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 0.1f);
    }
}
