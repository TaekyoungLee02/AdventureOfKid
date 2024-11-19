using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{
    //private bool _alreadyApllyForce;
    private bool _alreadyApliedDealing;

    public EnemyAttackState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.MovementSpeedModifier = 0;
        stateMachine.Enemy.NavMeshAgent.speed = stateMachine.MovementSpeedModifier;
        stateMachine.Enemy.NavMeshAgent.isStopped = true;
        base.Enter();
        StartAnimation(stateMachine.Enemy.AnimationData.AttackParameterHash);
        StartAnimation(stateMachine.Enemy.AnimationData.BaseAttackParameterHash);
        //_alreadyApllyForce = false;
        _alreadyApliedDealing = false;
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Enemy.AnimationData.AttackParameterHash);
        StopAnimation(stateMachine.Enemy.AnimationData.BaseAttackParameterHash);
    }

    public override void Update()
    {
        base.Update();

        //ForceMove();

        float normalizedTime = GetNormalizedTime(stateMachine.Enemy.Animator, "Attack");
        if (normalizedTime < 1f)
        {
            if (normalizedTime >= stateMachine.Enemy.Data.ForceTransitionTime)
            {
                // ´ïÇÎ ½Ãµµ
                //TryApplyForce();
            }

            if (!_alreadyApliedDealing && normalizedTime >= stateMachine.Enemy.Data.Dealing_Start_TransitionTime)
            {
                //stateMachine.Enemy.Weapon.SetAttack(stateMachine.Enemy.Data.damage, stateMachine.Enemy.Data.Force);
                stateMachine.Enemy.Weapon.gameObject.SetActive(true);
                _alreadyApliedDealing = true;
            }

            if (_alreadyApliedDealing && normalizedTime >= stateMachine.Enemy.Data.Dealing_End_TransitionTime)
            {
                stateMachine.Enemy.Weapon.gameObject.SetActive(false);
            }
        }
        else
        {
            if (IsInChasingRange())
            {
                stateMachine.ChangeState(stateMachine.ChasingState);
                return;
            }
            else
            {
                stateMachine.ChangeState(stateMachine.IdleState);
                return;
            }
        }
    }

    //private void TryApplyForce()
    //{
    //    if (_alreadyApllyForce) return;
    //    _alreadyApllyForce = true;

    //    _stateMachine.Enemy.ForceReceiver.Reset();
    //    _stateMachine.Enemy.ForceReceiver.AddForce(Vector3.forward * _stateMachine.Enemy.Data.Force);
    //}
}
