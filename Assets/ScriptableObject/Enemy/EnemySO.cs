using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnemyGroundData
{
    [field: SerializeField][field: Range(0f, 25f)] public float BaseSpeed { get; set; } = 5f;
    [field: SerializeField][field: Range(0f, 25f)] public float BaseRotationDamping { get; private set; } = 1f;

    [field: Header("IdleData")]

    [field: Header("WalkData")]

    [field: Header("ChasingData")]
    [field: SerializeField][field: Range(0f, 2f)] public float ChasingSpeedModifier { get; private set; } = 1f;
    [field: SerializeField] public float EnemyChasingRange { get; private set; } = 10f;
    [field: SerializeField] public float AttackRange { get; private set; } = 1.5f;
}

[CreateAssetMenu(fileName = "Enemy", menuName = "Characters/Enemy")]
public class EnemySO : ScriptableObject
{
    [field: SerializeField] public float PlayerChasingRange { get; private set; } = 10f;
    [field: SerializeField] public float AttackRange { get; private set; } = 1.5f;
    [field: SerializeField][field: Range(0f, 3f)] public float ForceTransitionTime { get; private set; }
    [field: SerializeField][field: Range(-10f, 10f)] public float Force { get; private set; }
    [field: SerializeField] public EnemyGroundData GroundData { get; private set; }
    [field: SerializeField][field: Range(0f, 1f)] public float Dealing_Start_TransitionTime { get; private set; }
    [field: SerializeField][field: Range(0f, 1f)] public float Dealing_End_TransitionTime { get; private set; }

    [Header("Info")]
    public int hp;
    public int damage;
}