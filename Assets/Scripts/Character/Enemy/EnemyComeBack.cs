using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyComeBack : MonoBehaviour
{
    [SerializeField] private float range = 10f;
    private SphereCollider myCollider;

    void Start()
    {
        myCollider = GetComponent<SphereCollider>();
        myCollider.radius = range;
        transform.SetParent(null);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Enemy enemy))
        {
            enemy.IsOut = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.TryGetComponent(out Enemy enemy))
        {
            enemy.IsOut = true;
        }
    }
}
