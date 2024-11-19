using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnemyAnimationData
{
    [SerializeField] private string groundParameterName = "@Ground";
    [SerializeField] private string idleParameterName = "Idle";
    [SerializeField] private string wanderParameterName = "Wander";
    [SerializeField] private string chasingParameterName = "Chasing";

    [SerializeField] private string attackParameterName = "@Attack";
    //[SerializeField] private string _comboAttackParameterName = "ComboAttack";
    [SerializeField] private string baseAttackParameterName = "BaseAttack";

    [SerializeField] private string damageParameterName = "Damage";

    public int GroundParameterHash { get; private set; }
    public int IdleParameterHash { get; private set; }
    public int WanderParameterHash { get; private set; }
    public int ChasingParameterHash { get; private set; }

    public int AttackParameterHash { get; private set; }
    //public int ComboAttackParameterHash { get; private set; }
    public int BaseAttackParameterHash { get; private set; }

    public int DamageParameterHash { get; private set; }

    public void Initialize()
    {
        GroundParameterHash = Animator.StringToHash(groundParameterName);
        IdleParameterHash = Animator.StringToHash(idleParameterName);
        WanderParameterHash = Animator.StringToHash(wanderParameterName);
        ChasingParameterHash = Animator.StringToHash(chasingParameterName);

        AttackParameterHash = Animator.StringToHash(attackParameterName);
        //ComboAttackParameterHash = Animator.StringToHash(_comboAttackParameterName);
        BaseAttackParameterHash = Animator.StringToHash(baseAttackParameterName);

        DamageParameterHash = Animator.StringToHash(damageParameterName);
    }
}
