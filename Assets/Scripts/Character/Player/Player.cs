using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerInputManager inputManager;
    private PlayerMovement playerMovement;
    private PlayerController playerController;
    private Equipment equipment;
    private PlayerHealth health;

    public Action<ItemData> AddItem;

    public PlayerInputManager InputManager { get { return inputManager; } }
    public PlayerMovement PlayerMovement { get { return playerMovement; } }
    public PlayerController PlayerController { get { return playerController; } }
    public Equipment Equipment { get { return equipment; } }
    public PlayerHealth Health { get { return health; } }

    private void Awake()
    {
        CharacterManager.Instance.Player = this;

        inputManager = GetComponent<PlayerInputManager>();
        playerMovement = GetComponent<PlayerMovement>();
        playerController = GetComponent<PlayerController>();
        equipment = GetComponent<Equipment>();
        health = GetComponent<PlayerHealth>();

        Cursor.lockState = CursorLockMode.Confined;
    }
}
