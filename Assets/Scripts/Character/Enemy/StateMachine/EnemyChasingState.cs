using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyChasingState : EnemyBaseState
{
    NavMeshPath path;

    public EnemyChasingState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
        path = new NavMeshPath();
    }

    public override void Enter()
    {
        stateMachine.MovementSpeedModifier = groundData.ChasingSpeedModifier;
        stateMachine.Enemy.NavMeshAgent.speed = stateMachine.MovementSpeed + stateMachine.MovementSpeedModifier;
        stateMachine.Enemy.NavMeshAgent.isStopped = false;
        base.Enter();
        StartAnimation(stateMachine.Enemy.AnimationData.GroundParameterHash);
        StartAnimation(stateMachine.Enemy.AnimationData.ChasingParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Enemy.AnimationData.GroundParameterHash);
        StopAnimation(stateMachine.Enemy.AnimationData.ChasingParameterHash);
    }

    public override void Update()
    {
        base.Update();

        if (!IsInChasingRange())
        {
            stateMachine.Enemy.NavMeshAgent.SetDestination(stateMachine.Enemy.transform.position);
            stateMachine.ChangeState(stateMachine.IdleState);
            return;
        }
        else
        {
            if (stateMachine.Enemy.IsOut)
            {
                ComeBackPath();

                return;
            }

            if (stateMachine.Enemy.NavMeshAgent.CalculatePath(stateMachine.Target.transform.position, path))
            {
                stateMachine.Enemy.NavMeshAgent.SetDestination(stateMachine.Target.transform.position);
            }
            else
            {
                stateMachine.Enemy.NavMeshAgent.SetDestination(stateMachine.Enemy.transform.position);
                stateMachine.ChangeState(stateMachine.IdleState);
                return;
            }

            if (IsInAttackRange())
            {
                stateMachine.ChangeState(stateMachine.AttackState);
                return;
            }
        }
    }

    protected bool IsInAttackRange()
    {
        float playerDistanceSqr = (stateMachine.Target.transform.position - stateMachine.Enemy.transform.position).sqrMagnitude;
        return playerDistanceSqr <= stateMachine.Enemy.Data.AttackRange * stateMachine.Enemy.Data.AttackRange;
    }
}
