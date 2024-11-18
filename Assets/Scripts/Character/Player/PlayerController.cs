using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerInputManager inputManager;

    public event Action<Vector3> OnMove;
    public event Action OnLook;

    private void Awake()
    {
        inputManager = GetComponent<PlayerInputManager>();
    }

    private void OnEnable()
    {
        
    }


    private void FixedUpdate()
    {
        OnMove?.Invoke(new(inputManager.PlayerMove.x, 0, inputManager.PlayerMove.y));
    }

    private void LateUpdate()
    {
        OnLook?.Invoke();
    }
}
