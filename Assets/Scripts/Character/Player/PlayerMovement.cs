using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float jumpPower;
    private bool isJumping;
    private float moveDirectionY;

    private Transform playerCamera;
    private CharacterController playerCC;
    private PlayerController playerController;

    public bool CanJump { get { return playerCC.isGrounded; } }

    private void Awake()
    {
        playerCamera = GetComponentInChildren<PlayerThirdCamera>().transform;
        playerCC = GetComponent<CharacterController>();
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
    }

    private void DeleteMovementEvent()
    {
        playerController.OnMove -= Move;
        playerController.OnJump -= Jump;
    }

    private void Move(Vector2 moveDirection)
    {
        if (isJumping)
        {
            moveDirectionY = jumpPower;
            isJumping = false;
        }
        moveDirectionY += (Physics.gravity.y * Time.deltaTime);

        Vector3 lookDirection = moveDirection;

        if (moveDirection != Vector2.zero)
        {
            lookDirection = playerCamera.TransformDirection(new(moveDirection.x, 0, moveDirection.y));
            lookDirection = new Vector3(lookDirection.x, 0, lookDirection.z).normalized;
            transform.forward = lookDirection;
        }

        playerCC.Move(speed * Time.deltaTime * new Vector3(lookDirection.x, moveDirectionY, lookDirection.z));
    }

    private void Jump()
    {
        if (playerCC.isGrounded)
        {
            isJumping = true;
        }
    }
}
