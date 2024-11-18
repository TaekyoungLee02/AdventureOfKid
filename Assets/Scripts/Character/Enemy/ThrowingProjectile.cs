using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingProjectile : MonoBehaviour
{
    [SerializeField] private float speed = 10f;

    private void Start()
    {
        Invoke("DestroySelf", 5f);
    }

    void Update()
    {
        transform.Translate(speed * Time.deltaTime * Vector3.forward);
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }
}
