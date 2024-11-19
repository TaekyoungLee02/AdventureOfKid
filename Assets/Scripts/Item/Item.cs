using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private float amplitude = 0.01f;
    private float frequency = 5f;

    private float rotationSpeed = 90f;

    private Vector3 startPosition;

    public virtual void Rotation(GameObject itemObject)
    {
        itemObject.transform.Rotate(-45, 0, 0);
        startPosition = itemObject.transform.position;
        StartCoroutine(CoItemRotation(itemObject));
    }

    IEnumerator CoItemRotation(GameObject itemObject)
    {
        while (true)
        {
            Vector3 newPostiotn = startPosition;
            newPostiotn.y += Mathf.Sin(Time.time * frequency) * amplitude;
            itemObject.transform.position = newPostiotn;

            itemObject.transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
            yield return new WaitForEndOfFrame();
        }
    }
}
