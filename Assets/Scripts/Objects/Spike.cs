using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    public float DamageAmount = 10f; // ���ط� ����
    public float DamageAngle = 45f;  // ���ظ� �� ���� ����

    private void OnTriggerEnter(Collider other)
    {
        Vector3 otherPosition = other.transform.position;

        Vector3 directionToOther = (otherPosition - transform.position).normalized;

        Vector3 spikeForward = transform.forward;

        float angle = Vector3.Angle(spikeForward, directionToOther);

        if (angle < DamageAngle)
        {
            Debug.Log("�ƾ�!?");
            // TODO : ������ �ֱ�
            //Health health = other.GetComponent<Health>();
            //if (health != null)
            //{
            //    health.TakeDamage(DamageAmount);
            //}
        }
        else
        {
            Debug.Log("���� ���");
        }
    }
}
