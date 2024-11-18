using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonObject : MonoBehaviour
{
    public float weightThreshold;
    public float totalMass;

    private Vector3 originalPosition;
    public float pressedDepth;

    public List<Rigidbody> objectsOnButton = new List<Rigidbody>();

    private void Awake()
    {
        originalPosition = transform.position;
    }

    private void OnTriggerEnter(Collision collision)
    {
        Rigidbody rb = collision.collider.attachedRigidbody;
        if (rb != null && !objectsOnButton.Contains(rb))
        {
            objectsOnButton.Add(rb);
            UpdateMass();
        }
    }

    void OnTriggerExit(Collision collision)
    {
        Rigidbody rb = collision.collider.attachedRigidbody;
        if (rb != null && objectsOnButton.Contains(rb))
        {
            objectsOnButton.Remove(rb);
            UpdateMass();
        }
    }

    private void Update()
    {
        if (totalMass >= weightThreshold)
        {
            MoveButtonDown();
        }
        else
        {
            MoveButtonUp();
        }
    }

    void UpdateMass()
    {
        totalMass = 0;
        foreach (Rigidbody rb in objectsOnButton)
        {
            totalMass += rb.mass;
        }
    }

    IEnumerator MoveButton(Vector3 targetPosition, float duration)
    {
        Vector3 startPosition = transform.position;
        float time = 0;

        while (time < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition;
    }

    void MoveButtonDown()
    {
        StartCoroutine(MoveButton(originalPosition - new Vector3(0, pressedDepth, 0), 0.5f));
    }

    void MoveButtonUp()
    {
        StartCoroutine(MoveButton(originalPosition, 0.4f));
    }
}

