using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Disabler : MonoBehaviour, ITriggerable
{
    public float MoveDistance = -1f;

    [SerializeField] private Vector3 initialPosition;
    [SerializeField] private Vector3 disablePosition;
    private Coroutine moveCoroutine;
    private void Awake()
    {
        initialPosition = transform.position;
        disablePosition = initialPosition + new Vector3(0, MoveDistance, 0);
    }

    public void ExecuteFunction()
    {
        if (moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
        }
        moveCoroutine = StartCoroutine(CoDisable());
    }
    public void RevokeFunction()
    {
        if (moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
        }
        moveCoroutine = StartCoroutine(CoEnable());
    }

    private IEnumerator CoDisable()
    {
        while (Vector3.Distance(transform.position, disablePosition) > 0.01f)
        {
            transform.position = Vector3.Lerp(transform.position, disablePosition, 8f * Time.deltaTime);
            yield return null;
        }
        transform.position = disablePosition;
    }

    private IEnumerator CoEnable()
    {
        while (Vector3.Distance(transform.position, initialPosition) > 0.01f)
        {
            transform.position = Vector3.Lerp(transform.position, initialPosition, 8f * Time.deltaTime);
            yield return null;
        }
        transform.position = initialPosition;
    }
}