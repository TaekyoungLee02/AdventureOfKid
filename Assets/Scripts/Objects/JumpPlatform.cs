using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPlatform : MonoBehaviour
{
    public float JumpForce = 10f;

    private void OnTriggerEnter(Collider other)
    {
        PlayerController controller = other.GetComponent<PlayerController>();

        if (controller != null)
        {
            controller.MakePlayerJump(transform.up * JumpForce);
        }

        //Rigidbody rb = other.GetComponent<Rigidbody>();
        //if (rb != null)
        //{
        //    rb.AddForce(transform.up * JumpForce, ForceMode.Impulse);
        //}
    }
}
