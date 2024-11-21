using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    private Animator animator;
    private PlayerController playerController;
    private PlayerMovement playerMovement;

    private Dictionary<string, int> animatorHashes;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerController = GetComponentInParent<PlayerController>();
        playerMovement = GetComponentInParent<PlayerMovement>();
        animatorHashes = new();

        InitAnimatorHash();
    }

    private void OnEnable()
    {
        InitAnimatorEvent();
    }

    private void OnDisable()
    {
        DeleteAnimatorEvent();
    }

    private void InitAnimatorHash()
    {
        animatorHashes.Add("isMoving", Animator.StringToHash("isMoving"));
        animatorHashes.Add("isRunning", Animator.StringToHash("isRunning"));
        animatorHashes.Add("OnJump", Animator.StringToHash("OnJump"));
    }

    private void InitAnimatorEvent()
    {
        playerController.OnMove += OnMove;
        playerController.OnJump += OnJump;
    }

    private void DeleteAnimatorEvent()
    {
        playerController.OnMove -= OnMove;
        playerController.OnJump -= OnJump;
    }

    private void OnMove(Vector2 moveDirection)
    {
        if (moveDirection != Vector2.zero)
        {
            animator.SetBool(animatorHashes["isMoving"], true);
        }
        else
        {
            animator.SetBool(animatorHashes["isMoving"], false);
        }
    }

    private void OnJump()
    {
        if (playerMovement.IsGrounded)
        {
            animator.SetTrigger(animatorHashes["OnJump"]);
        }
    }
}
