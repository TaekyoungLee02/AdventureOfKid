using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : SingletonBase<Player>
{
    private PlayerInputManager inputManager;
    private PlayerMovement playerMovement;
    private PlayerController playerController;

    public PlayerInputManager InputManager { get { return inputManager; } }
    public PlayerMovement PlayerMovement { get { return playerMovement; } }
    public PlayerController PlayerController { get { return playerController; } }

    private new void Awake()
    {
        base.Awake();

        inputManager = GetComponent<PlayerInputManager>();
        playerMovement = GetComponent<PlayerMovement>();
        playerController = GetComponent<PlayerController>();
    }
}