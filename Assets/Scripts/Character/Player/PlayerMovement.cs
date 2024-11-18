using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private Transform playerCamera;
    private CharacterController playerCC;
    private PlayerController playerController;

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
    }

    private void OnDisable()
    {
        playerController.OnLook -= Look;
        playerController.OnMove -= Move;
    }

    private void Look()
    {
        Vector3 lookDirection = playerCamera.TransformDirection(Vector3.forward);
        lookDirection = new Vector3(lookDirection.x, 0, lookDirection.z).normalized;

        transform.forward = lookDirection;
    }

    private void Move(Vector3 moveDirection)
    {
        playerCC.Move(speed * Time.deltaTime * transform.TransformDirection(moveDirection));
    }
}
