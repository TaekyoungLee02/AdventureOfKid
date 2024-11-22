using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class Enemy : MonoBehaviour
{
    [field: SerializeField] public EnemySO Data { get; private set; }

    [field: Header("Animations")]
    [field: SerializeField] public EnemyAnimationData AnimationData { get; private set; }

    public Animator Animator { get; private set; }
    public CharacterController Controller { get; private set; }
    //public ForceReceiver ForceReceiver { get; private set; }

    public EnemyCondition Condition { get; private set; }

    public NavMeshAgent NavMeshAgent { get; private set; }

    private EnemyStateMachine stateMachine;

    public Action RemoveTarget;

    public bool IsOut { get; set; }
    [field: SerializeField] public bool CanThrow { get; private set; }

    public Vector3 OriginPos { get; private set; }

    [field: SerializeField] public Weapon Weapon { get; private set; }

    private void Awake()
    {
        AnimationData.Initialize();
        Animator = GetComponentInChildren<Animator>();
        Controller = GetComponent<CharacterController>();
        //ForceReceiver = GetComponent<ForceReceiver>();
        Condition = GetComponent<EnemyCondition>();

        NavMeshAgent = GetComponent<NavMeshAgent>();

        OriginPos = transform.position;

        stateMachine = new EnemyStateMachine(this);

        GetComponent<EnemyCondition>().StateMachine = stateMachine;
    }

    void Start()
    {
        stateMachine.ChangeState(stateMachine.IdleState);

        SoundManager.Instance.Play("bgm", Sound.Bgm, 0.5f);
    }

    private void Update()
    {
        stateMachine.HandleInput();
        stateMachine.Update();
    }

    private void FixedUpdate()
    {
        stateMachine.PhysicsUpdate();
    }

    public void OnTestDamage()
    {
        stateMachine.ChangeState(stateMachine.DamageState);

        // Temp
        EffectManager.Instance.PlayEffect("Hit", 1f, transform.position + Vector3.up, Quaternion.identity, "coin");
        EffectManager.Instance.SettingColor(0f, 1f, 0f);
    }
}
