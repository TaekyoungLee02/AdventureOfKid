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

    public bool _isDie;

    void Update()
    {
        if (hp == 0 && !_isDie)
        {
            Die();
            _isDie = true;
        }
    }

    public void TakePhysicalDamage(int damage)
    {
        hp -= damage;

        // TODO : Damage State ¡¯¿‘

        if (hp < 0)
            hp = 0;
    }

    public void Die()
    {
        Enemy enemy = GetComponent<Enemy>();

        enemy.Animator.SetTrigger("Die");

        enemy.RemoveTarget?.Invoke();

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
    }
}
