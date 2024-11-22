using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOnStep : MonoBehaviour
{
    [SerializeField] private PlayerController controller;

    public Vector3 JumpForce = Vector3.up * 0.1f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            controller.MakePlayerJump(JumpForce);
        }
    }
}
