using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Collider myCollider;

    private int damage;
    private float knockback;

    private List<Collider> alreadyCollider = new List<Collider>();

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        alreadyCollider.Clear();

        GameObject enemyObj = myCollider.gameObject;

        if(enemyObj.TryGetComponent(out Enemy enemy) && enemy.CanThrow)
        {
            var go = Resources.Load<GameObject>("Prefabs/Thorn Projectile");
            Vector3 dir = enemyObj.transform.forward;
            Instantiate(go, transform.position, Quaternion.LookRotation(dir));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == myCollider) return;
        if (alreadyCollider.Contains(other)) return;

        alreadyCollider.Add(other);

        if (other.TryGetComponent(out IDamageable damageable))
        {
            damageable.TakePhysicalDamage(damage);
        }

        //if (other.TryGetComponent(out ForceReceiver force))
        //{
        //    Vector3 dir = (other.transform.position - _myCollider.transform.position);
        //    force.AddForce(dir * _knockback);
        //}
    }

    public void SetAttack(int damage, float knockback)
    {
        this.damage = damage;
        this.knockback = knockback;
    }
}
