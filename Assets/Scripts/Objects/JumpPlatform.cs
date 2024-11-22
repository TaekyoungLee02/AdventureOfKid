using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPlatform : MonoBehaviour
{
    public Vector3 JumpForce;

    private void Start()
    {
        JumpForce = transform.up * 5f;
    }


    private void OnTriggerEnter(Collider other)
    {
        PlayerController pc = other.GetComponent<PlayerController>();
        if (pc != null)
        {
            pc.MakePlayerJump(JumpForce);
            SoundManager.Instance.Play("jump", Sound.Sfx);
        }
    }
}
