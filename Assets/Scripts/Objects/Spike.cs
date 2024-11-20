using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    public float DamageAmount = 10f; // ���ط� ����
    public float DamageAngle = 90f;  // ���ظ� �� ���� ����

    private void OnTriggerEnter(Collider other)
    {
        Vector3 otherPosition = other.transform.position; //�浹�� ������Ʈ�� ��ġ

        Vector3 directionToOther = (otherPosition - transform.position).normalized; //�浹�� ������Ʈ�� ����

        Vector3 spikeForward = transform.forward; // ������ ����

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