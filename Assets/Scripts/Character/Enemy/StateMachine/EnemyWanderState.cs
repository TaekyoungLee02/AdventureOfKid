using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyWanderState : EnemyBaseState
{
    public EnemyWanderState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateMachine.Enemy.NavMeshAgent.isStopped = false;
        StartAnimation(stateMachine.Enemy.AnimationData.GroundParameterHash);
        StartAnimation(stateMachine.Enemy.AnimationData.WanderParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Enemy.AnimationData.GroundParameterHash);
        StopAnimation(stateMachine.Enemy.AnimationData.WanderParameterHash);
    }

    public override void Update()
    {
        base.Update();

        if (stateMachine.Enemy.IsOut)
        {
            ComeBackPath();

            return;
        }

        if (IsInChasingRange() && IsPlayerInFieldOfView())
        {
            stateMachine.ChangeState(stateMachine.ChasingState);
        }
        else
        {
            if (stateMachine.Enemy.NavMeshAgent.remainingDistance < 0.1f)
            {
                stateMachine.ChangeState(stateMachine.IdleState);
            }
        }
    }
}
