using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerInputManager inputManager;

    public event Action<Vector2> OnMove;
    public event Action OnLook;
    public event Action OnJump;
    public event Action<float> OnStructureJump;

    private void Awake()
    {
        inputManager = GetComponent<PlayerInputManager>();
    }


    private void FixedUpdate()
    {
        OnMove?.Invoke(inputManager.PlayerMove);

        if (inputManager.IsJumping)
        {
            OnJump?.Invoke();
        }
    }

    private void LateUpdate()
    {
        OnLook?.Invoke();
    }

    public void MakePlayerJump(float jumpPower)
    {
        OnStructureJump?.Invoke(jumpPower);
    }
}
