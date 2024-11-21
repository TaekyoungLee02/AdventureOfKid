using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour, IDataInitializer
{
    public float DamageAmount = 10f; // ���ط� ����
    public float DamageAngle = 90f;  // ���ظ� �� ���� ����

    public void Initialize(List<int> values)
    {
        DamageAmount = values[0];
        DamageAngle = values[1];
    }

    private void OnTriggerEnter(Collider other)
    {
        Vector3 otherPosition = other.transform.position; //�浹�� ������Ʈ�� ��ġ

        Vector3 directionToOther = (otherPosition - transform.position).normalized; //�浹�� ������Ʈ�� ����

        Vector3 spikeForward = transform.forward; // ������ ����

        float angle = Vector3.Angle(spikeForward, directionToOther); 

        if (angle < DamageAngle)
        {
            Debug.Log("�ƾ�!?");
        }
        else
        {
            Debug.Log("���� ���");
        }
    }


}
