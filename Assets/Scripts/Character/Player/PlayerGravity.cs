using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGravity : MonoBehaviour
{
    private const float gravity = -9.81f;
    private CharacterController playerCC;

    private void Awake()
    {
        playerCC = GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        if (!playerCC.isGrounded)
            playerCC.Move(new Vector3(0, gravity * Time.deltaTime, 0));
        else
            playerCC.Move(new Vector3(0, 0, 0));
    }
}
