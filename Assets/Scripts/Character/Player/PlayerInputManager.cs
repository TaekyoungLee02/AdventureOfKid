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
    private Vector2 playerAxis;
    private Vector2 playerMove;

    public PlayerInput PlayerInput { get { return playerInput; } }
    public Vector2 PlayerAxis { get { return playerAxis; } }
    public Vector2 PlayerMove { get { return playerMove; } }

    private void Awake()
    {
        playerInput = new PlayerInput();
        joystick = FindObjectOfType<VirtualJoystick>();
        InitPlayerInput();
    }

    private void OnEnable()
    {
        playerInput.Enable();
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }

    private void InitPlayerInput()
    {
        #if (UNITY_ANDROID || UNITY_IOS)

            playerInput.Player.PrimaryTouch.performed += Look;
            playerInput.Player.PrimaryTouch.canceled += Look;
            playerInput.Player.SecondaryTouch.performed += Look;
            playerInput.Player.SecondaryTouch.canceled += Look;

            joystick.OnTouchscreenMove += TouchScreenMove;

        #else

            playerInput.Player.Look.performed += Look;
            playerInput.Player.Look.canceled += Look;

            playerInput.Player.Move.performed += Move;
            playerInput.Player.Move.canceled += Move;

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

        Debug.Log(movePosition);
    }

#else

    private void Move(InputAction.CallbackContext context)
    {
        playerMove = context.ReadValue<Vector2>();
    }

#endif
}
