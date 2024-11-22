using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBaseState : IState
{
    protected EnemyStateMachine stateMachine;
    protected readonly EnemyGroundData groundData;

    // Temp
    float minWanderDistance = 5f;
    float maxWanderDistance = 10f;
    float detectDistance    = 5f;
    float minWanderWaitTime = 1f;
    float maxWanderWaitTime = 5f;
    float fieldOfView = 120f;


    public EnemyBaseState(EnemyStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
        groundData = stateMachine.Enemy.Data.GroundData;
    }

    public virtual void Enter()
    {
    }

    public virtual void Exit()
    {
    }

    public virtual void HandleInput()
    {
    }

    public virtual void PhysicsUpdate()
    {
    }

    public virtual void Update()
    {
        if (stateMachine.Enemy.Condition.isDie)
        {
            stateMachine.Enemy.NavMeshAgent.isStopped = true;
            return;
        }
        
        if ((stateMachine.CurrentState == stateMachine.ChasingState
            || stateMachine.CurrentState == stateMachine.AttackState))
        {
            if (stateMachine.Enemy.NavMeshAgent.velocity.sqrMagnitude > 0.1f)
                LookAtNavPath();
            else
                Rotate();
        }
    }

    protected void StartAnimation(int animatorHash)
    {
        stateMachine.Enemy.Animator.SetBool(animatorHash, true);
    }

    protected void StopAnimation(int animatorHash)
    {
        stateMachine.Enemy.Animator.SetBool(animatorHash, false);
    }

    private void Rotate()
    {
        Vector3 movementDirection = GetMovementDirection();

        Rotate(movementDirection);
    }

    private Vector3 GetMovementDirection()
    {
        Vector3 dir = (stateMachine.Target.transform.position - stateMachine.Enemy.transform.position);

        return dir;
    }

    private void Rotate(Vector3 direction)
    {
        if (direction != Vector3.zero)
        {
            Transform playerTransform = stateMachine.Enemy.transform;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            playerTransform.rotation = Quaternion.Slerp(playerTransform.rotation, targetRotation, stateMachine.RotationDamping * Time.deltaTime);
        }
    }

    //protected void ForceMove()
    //{
    //    stateMachine.Enemy.Controller.Move(_stateMachine.Enemy.ForceReceiver.Movement * Time.deltaTime);
    //}

    protected float GetNormalizedTime(Animator animator, string tag)
    {
        AnimatorStateInfo currentInfo = animator.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo nextInfo = animator.GetNextAnimatorStateInfo(0);

        if (animator.IsInTransition(0) && nextInfo.IsTag(tag))
        {
            return nextInfo.normalizedTime;
        }
        else if (!animator.IsInTransition(0) && currentInfo.IsTag(tag))
        {
            return currentInfo.normalizedTime;
        }
        else
        {
            return 0;
        }
    }

    protected bool IsInChasingRange()
    {
        if (stateMachine.Target == null) return false;

        float playerDistanceSqr = (stateMachine.Target.transform.position - stateMachine.Enemy.transform.position).sqrMagnitude;
        return playerDistanceSqr <= stateMachine.Enemy.Data.PlayerChasingRange * stateMachine.Enemy.Data.PlayerChasingRange;
    }

    protected async void WanderToNewLocation()
    {
        float delayRate = Random.Range(minWanderWaitTime, maxWanderWaitTime) * 1000;
        await Task.Delay((int)delayRate);

        stateMachine.ChangeState(stateMachine.WanderState);
        stateMachine.Enemy.NavMeshAgent.SetDestination(GetWanderLocation());
    }

    protected Vector3 GetWanderLocation()
    {
        NavMeshHit hit;

        NavMesh.SamplePosition(stateMachine.Enemy.transform.position +
            (Random.onUnitSphere * Random.Range(minWanderDistance, maxWanderDistance)),
            out hit, maxWanderDistance, NavMesh.AllAreas);

        int i = 0;

        while (Vector3.Distance(stateMachine.Enemy.transform.position, hit.position) < detectDistance)
        {
            NavMesh.SamplePosition(stateMachine.Enemy.transform.position +
            (Random.onUnitSphere * Random.Range(minWanderDistance, maxWanderDistance)),
            out hit, maxWanderDistance, NavMesh.AllAreas);
            i++;
            if (i == 30) break;
        }

        return hit.position;
    }

    protected bool IsPlayerInFieldOfView()
    {
        Vector3 directionToPlayer = stateMachine.Target.transform.position - stateMachine.Enemy.transform.position;
        float angle = Vector3.Angle(stateMachine.Enemy.transform.forward, directionToPlayer);
        return angle < fieldOfView * 0.5f;
    }

    private void LookAtNavPath()
    {
        Transform playerTransform = stateMachine.Enemy.transform;
        Quaternion targetRotation = Quaternion.LookRotation(stateMachine.Enemy.NavMeshAgent.velocity.normalized);
        playerTransform.rotation = Quaternion.Slerp(playerTransform.rotation, targetRotation, stateMachine.RotationDamping * Time.deltaTime);
    }

    protected void ComeBackPath()
    {
        NavMeshPath newPath = new NavMeshPath();
        if (stateMachine.Enemy.NavMeshAgent.CalculatePath(stateMachine.Enemy.OriginPos, newPath))
        {
            stateMachine.Enemy.NavMeshAgent.SetPath(newPath);
        }
        //stateMachine.Enemy.NavMeshAgent.SetDestination(stateMachine.Enemy.OriginPos);

        if (stateMachine.Enemy.NavMeshAgent.remainingDistance < 0.1f)
        {
            stateMachine.Enemy.IsOut = false;
        }
    }
}
