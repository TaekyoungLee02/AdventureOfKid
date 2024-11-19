using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHealPotion : MonoBehaviour, IUseable
{
    private int value;

    private float amplitude = 0.01f;
    private float frequency = 5f;

    private float rotationSpeed = 90f;

    private Vector3 startPosition;

    private void Start()
    {
        transform.Rotate(-45, 0, 0);
        startPosition = transform.position;
        StartCoroutine(CoItemRotation());
    }

    private void Update()
    {
      
    }

    public void Use()
    {
        Debug.Log($"플레이어의 체력이 {value} 회복됬습니다.");
    }

    public void Init(ItemData data)
    {
        value = data.value;
    }

    IEnumerator CoItemRotation()
    {
        while (true)
        {
            Vector3 newPostiotn = startPosition;
            newPostiotn.y += Mathf.Sin(Time.time * frequency) * amplitude;
            transform.position = newPostiotn;

            transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
            yield return new WaitForEndOfFrame();
        }
    }
}
