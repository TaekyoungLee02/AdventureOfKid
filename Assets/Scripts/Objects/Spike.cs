using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    public float DamageAmount = 10f; // 피해량 설정
    public float DamageAngle = 90f;  // 피해를 줄 각도 범위

    private void OnTriggerEnter(Collider other)
    {
        Vector3 otherPosition = other.transform.position; //충돌한 오브젝트의 위치

        Vector3 directionToOther = (otherPosition - transform.position).normalized; //충돌한 오브젝트의 방향

        Vector3 spikeForward = transform.forward; // 가시의 방향

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
