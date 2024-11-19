using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageState : EnemyBaseState
{
    AnimatorStateInfo beforeInfo;
    float beforeSpeed;

    public EnemyDamageState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        beforeInfo = stateMachine.Enemy.Animator.GetCurrentAnimatorStateInfo(0);
        beforeSpeed = stateMachine.Enemy.NavMeshAgent.speed;
        stateMachine.Enemy.NavMeshAgent.speed = 0f;
        stateMachine.Enemy.NavMeshAgent.isStopped = true;
        base.Enter();
        stateMachine.Enemy.Animator.SetTrigger(stateMachine.Enemy.AnimationData.DamageParameterHash);
    }

    public override void Exit()
    {
        stateMachine.Enemy.NavMeshAgent.speed = beforeSpeed;
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        AnimatorStateInfo currentInfo = stateMachine.Enemy.Animator.GetCurrentAnimatorStateInfo(0);

        if (beforeInfo.shortNameHash != currentInfo.shortNameHash)
        {
            if(currentInfo.normalizedTime >= 1f)
                stateMachine.ChangeState(stateMachine.IdleState);
        }
    }
}
