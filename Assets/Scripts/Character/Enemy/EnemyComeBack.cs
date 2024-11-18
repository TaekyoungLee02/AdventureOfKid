using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyComeBack : MonoBehaviour
{
    [SerializeField] private float range = 10f;
    private SphereCollider myCollider;
    private Vector3 originPos;

    void Start()
    {
        myCollider = GetComponent<SphereCollider>();
        myCollider.radius = range;
        //transform.SetParent(null);

        originPos = transform.position;
    }

    private void Update()
    {
        transform.position = originPos;
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.TryGetComponent(out Enemy enemy))
    //        //if (other.gameObject.TryGetComponent(out NavMeshAgent navMeshAgent))
    //        //    if (navMeshAgent.remainingDistance < 0.1f)
    //                    enemy.IsOut = false;
    //}

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.TryGetComponent(out Enemy enemy) && other.gameObject == transform.parent.gameObject)
        {
            enemy.IsOut = true;
        }
    }
}
