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

    private Transform playerCamera;
    private CharacterController playerCC;
    private PlayerController playerController;

    private Vector3 moveDirection;

    private void Awake()
    {
        playerCamera = GetComponentInChildren<PlayerThirdCamera>().transform;
        playerCC = GetComponent<CharacterController>();
        playerController = GetComponent<PlayerController>();
    }

    private void OnEnable()
    {
        playerController.OnLook += Look;
        playerController.OnMove += Move;
        playerController.OnJump += Jump;
    }

    private void OnDisable()
    {
        playerController.OnLook -= Look;
        playerController.OnMove -= Move;
        playerController.OnJump -= Jump;
    }

    private void Look()
    {
        Vector3 lookDirection = playerCamera.TransformDirection(Vector3.forward);
        lookDirection = new Vector3(lookDirection.x, 0, lookDirection.z).normalized;

        transform.forward = lookDirection;
    }

    private void Move(Vector2 moveDirection)
    {
        this.moveDirection = new(moveDirection.x, this.moveDirection.y, moveDirection.y);

        if (isJumping)
        {
            this.moveDirection.y = jumpPower;
            isJumping = false;
        }

        this.moveDirection.y += (Physics.gravity.y * Time.deltaTime);

        playerCC.Move(speed * Time.deltaTime * transform.TransformDirection(this.moveDirection));
    }

    private void Jump()
    {
        if (playerCC.isGrounded)
        {
            isJumping = true;
        }
    }
}
