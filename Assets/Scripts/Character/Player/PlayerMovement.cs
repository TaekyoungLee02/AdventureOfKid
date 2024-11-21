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
    private Vector3 jumpVector;
    private bool isJumping;
    private bool isGrounded;
    private bool isOnStructure;
    private float moveDirectionY;

    [SerializeField] private LayerMask groundMask;

    private Transform playerCamera;
    private Rigidbody playerRB;
    private BoxCollider footCollider;
    private PlayerController playerController;

    private float lastUpdateFrame;

    public bool IsGrounded
    {
        get
        {
            return CheckGround();
        }
        set
        {
            isGrounded = value;
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
        if (isJumping && IsGrounded)
        {
            isJumping = false;
        }
        else if (isOnStructure)
        {
            isJumping = false;
            isOnStructure = false;
        }
        else if (!isJumping && IsGrounded)
        {
            jumpVector = Vector3.zero;
        }

        if (!IsGrounded)
        {
            jumpVector.y += (Physics.gravity.y * Time.fixedDeltaTime);
        }

        Vector3 lookDirection = moveDirection;

        if (moveDirection != Vector2.zero)
        {
            lookDirection = playerCamera.TransformDirection(new(moveDirection.x, 0, moveDirection.y));
            lookDirection = new Vector3(lookDirection.x, 0, lookDirection.z).normalized;
            transform.forward = lookDirection;
        }

        Vector3 finalVector = lookDirection + jumpVector;

        playerRB.velocity = speed * finalVector;
    }

    private void Jump()
    {
        if (IsGrounded && !isJumping)
        {
            jumpVector = new Vector3(0, defaultJumpPower, 0);
            isJumping = true;
        }
    }
    public void Jump(Vector3 jumpVector)
    {
        if (!isJumping)
        {
            this.jumpVector = jumpVector;
            isJumping = true;
            isOnStructure = true;
        }
    }

    public bool CheckGround()
    {
        if (Time.frameCount == lastUpdateFrame)
            return isGrounded;
        lastUpdateFrame = Time.frameCount;

        isGrounded = Physics.Raycast(transform.position, Vector3.down, 0.3f, groundMask);
        return isGrounded;
    }
}
