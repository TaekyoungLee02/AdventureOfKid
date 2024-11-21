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
    public event Action<Vector3> OnStructureJump;

    private void Awake()
    {
        inputManager = GetComponent<PlayerInputManager>();
    }


    private void FixedUpdate()
    {

        if (inputManager.IsJumping)
        {
            OnJump?.Invoke();
        }

        OnMove?.Invoke(inputManager.PlayerMove);

    }

    private void LateUpdate()
    {
        OnLook?.Invoke();
    }

    public void MakePlayerJump(Vector3 jumpVector)
    {
        OnStructureJump?.Invoke(jumpVector);
    }
}
