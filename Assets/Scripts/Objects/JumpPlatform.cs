using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPlatform : MonoBehaviour
{
    public float JumpForce = 10f;

    private void OnTriggerEnter(Collider other)
    {
        PlayerController PC = other.GetComponent<PlayerController>();
        if (PC != null)
        {
            PC.MakePlayerJump(JumpForce);
        }
    }
}
