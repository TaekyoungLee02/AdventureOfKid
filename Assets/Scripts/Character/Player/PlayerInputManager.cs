using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.LowLevel;

public class PlayerInputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    private VirtualJoystick joystick;
    private JumpControlUI jumpUI;
    private Vector2 playerAxis;
    private Vector2 playerMove;
    private bool isJumping;

    public PlayerInput PlayerInput { get { return playerInput; } }
    public Vector2 PlayerAxis { get { return playerAxis; } }
    public Vector2 PlayerMove { get { return playerMove; } }
    public bool IsJumping { get { return isJumping; } }

    private void Awake()
    {
        playerInput = new PlayerInput();
        joystick = FindObjectOfType<VirtualJoystick>();
        jumpUI = FindObjectOfType<JumpControlUI>();
    }

    private void OnEnable()
    {
        playerInput.Enable();
        InitPlayerInputEvent();
    }

    private void OnDisable()
    {
        playerInput.Disable();
        DeletePlayerInputEvent();
    }

    private void InitPlayerInputEvent()
    {
#if (UNITY_ANDROID || UNITY_IOS)

            playerInput.Player.PrimaryTouch.performed += Look;
            playerInput.Player.PrimaryTouch.canceled += Look;
            playerInput.Player.SecondaryTouch.performed += Look;
            playerInput.Player.SecondaryTouch.canceled += Look;

            joystick.OnTouchscreenMove += TouchScreenMove;
            jumpUI.OnTouchscreenJump += TouchScreenJump;

#else

            playerInput.Player.Look.performed += Look;
            playerInput.Player.Look.canceled += Look;

            playerInput.Player.Move.performed += Move;
            playerInput.Player.Move.canceled += Move;

            playerInput.Player.Jump.started += Jump;
            playerInput.Player.Jump.canceled += Jump;

#endif
    }

    private void DeletePlayerInputEvent()
    {
#if (UNITY_ANDROID || UNITY_IOS)

            playerInput.Player.PrimaryTouch.performed -= Look;
            playerInput.Player.PrimaryTouch.canceled -= Look;
            playerInput.Player.SecondaryTouch.performed -= Look;
            playerInput.Player.SecondaryTouch.canceled -= Look;

            joystick.OnTouchscreenMove -= TouchScreenMove;
            jumpUI.OnTouchscreenJump -= TouchScreenJump;

#else

        playerInput.Player.Look.performed -= Look;
        playerInput.Player.Look.canceled -= Look;

        playerInput.Player.Move.performed -= Move;
        playerInput.Player.Move.canceled -= Move;

        playerInput.Player.Jump.started -= Jump;
        playerInput.Player.Jump.canceled -= Jump;

#endif
    }

    private void Look(InputAction.CallbackContext context)
    {
        //#if (UNITY_EDITOR)

        //    if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        //    {
        //        var state = context.ReadValue<TouchState>();

        //        if (!EventSystem.current.IsPointerOverGameObject(state.touchId))
        //            playerAxis = state.delta;
        //    }
        //    else
        //    {
        //        playerAxis = context.ReadValue<Vector2>();

        //    }

        #if (UNITY_ANDROID || UNITY_IOS)

            var state = context.ReadValue<TouchState>();


            if (!EventSystem.current.IsPointerOverGameObject(state.touchId))
                playerAxis = state.delta;
            else
                playerAxis = Vector2.zero;

        #else

            playerAxis = context.ReadValue<Vector2>();

        #endif
    }
    

#if (UNITY_ANDROID || UNITY_IOS)
    private void TouchScreenMove(Vector2 movePosition)
    {
        playerMove = movePosition;
    }

    private void TouchScreenJump(bool isPushed)
    {
        isJumping = isPushed;
    }

#else

    private void Move(InputAction.CallbackContext context)
    {
        playerMove = context.ReadValue<Vector2>();
    }

    private void Jump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            isJumping = true;
        }
        else
        {
            isJumping = false;
        }
    }

#endif
}
