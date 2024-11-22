using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Collider myCollider;

    private int damage = 1;

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
            go.GetComponent<ThrowingProjectile>().MyCollider = myCollider;
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
            EffectManager.Instance.PlayEffect("Hit", 1f, transform.position + Vector3.up, Quaternion.identity, "coin");
        }
    }

    public void SetAttack(int damage)
    {
        this.damage = damage;
    }
}
