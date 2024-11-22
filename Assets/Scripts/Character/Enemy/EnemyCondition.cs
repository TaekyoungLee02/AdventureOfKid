using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    public void TakePhysicalDamage(int damage);
}

public class EnemyCondition : MonoBehaviour, IDamageable
{
    [SerializeField] private int hp = 1;

    public EnemyStateMachine StateMachine { get; set; }

    public bool isDie;

    void Update()
    {
        if (hp == 0 && !isDie)
        {
            Die();
            isDie = true;
        }
    }

    public void TakePhysicalDamage(int damage)
    {
        hp -= damage;

        EffectManager.Instance.PlayEffect("Hit", 1f, transform.position + Vector3.up, Quaternion.identity, "collision");

        if (hp <= 0)
        {
            hp = 0;
            return;
        }

        StateMachine.ChangeState(StateMachine.DamageState);
    }

    public void Die()
    {
        Enemy enemy = GetComponent<Enemy>();

        enemy.Animator.SetTrigger("Die");

        StateMachine.RemoveTarget();

        GetComponent<CharacterController>().enabled = false;

        StartCoroutine(CoroutineDie());
    }

    IEnumerator CoroutineDie()
    {
        while (true)
        {
            AnimatorStateInfo stateInfo = gameObject.GetComponent<Enemy>().Animator.GetCurrentAnimatorStateInfo(0);

            if (stateInfo.IsName("Die") && stateInfo.normalizedTime >= 1f)
                break;

            yield return null;
        }

        yield return new WaitForSeconds(0.5f);

        Destroy(gameObject);
        UIManager.Instance.AddCoin(500);
    }
}
