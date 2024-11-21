using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHeadCollider : MonoBehaviour
{
    [SerializeField] private EnemyCondition condition;
    private BoxCollider headCollider;

    void Start()
    {
        headCollider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            condition.TakePhysicalDamage(1);
        }
    }
}
