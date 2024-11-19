using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    public float DamageAmount = 10f; // 피해량 설정
    public float DamageAngle = 45f;  // 피해를 줄 각도 범위

    private void OnTriggerEnter(Collider other)
    {
        Vector3 otherPosition = other.transform.position;

        Vector3 directionToOther = (otherPosition - transform.position).normalized;

        Vector3 spikeForward = transform.forward;

        float angle = Vector3.Angle(spikeForward, directionToOther);

        if (angle < DamageAngle)
        {
            Debug.Log("아야!?");
            // TODO : 데미지 주기
            //Health health = other.GetComponent<Health>();
            //if (health != null)
            //{
            //    health.TakeDamage(DamageAmount);
            //}
        }
        else
        {
            Debug.Log("가시 통과");
        }
    }
}
