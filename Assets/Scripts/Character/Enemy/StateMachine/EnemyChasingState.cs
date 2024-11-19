using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

public class EnemyChasingState : EnemyBaseState
{
    NavMeshPath path;
    Coroutine curCoroutine;
    WaitForSeconds corutineDelay = new WaitForSeconds(0.1f);
    bool canMovePath;

    public EnemyChasingState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
        path = new NavMeshPath();
    }

    public override void Enter()
    {
        stateMachine.MovementSpeedModifier = groundData.ChasingSpeedModifier;
        stateMachine.Enemy.NavMeshAgent.speed = stateMachine.MovementSpeed + stateMachine.MovementSpeedModifier;
        stateMachine.Enemy.NavMeshAgent.isStopped = false;
        canMovePath = true;
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

            if (curCoroutine == null)
            {
                curCoroutine = CoroutineHelper.Instance.RunCoroutine(CoPathFind());
            }

            if (!canMovePath)
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

    IEnumerator CoPathFind()
    {
        if (stateMachine.Enemy.NavMeshAgent.CalculatePath(stateMachine.Target.transform.position, path))
        {
            stateMachine.Enemy.NavMeshAgent.SetDestination(stateMachine.Target.transform.position);
        }
        else
        {
            canMovePath = false;
        }

        yield return corutineDelay;

        curCoroutine = null;
    }
}
