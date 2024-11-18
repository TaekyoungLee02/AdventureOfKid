using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonObject : MonoBehaviour
{

    public float weightThreshold = 5f; // 버튼이 작동하는 중량 임계값
    public Vector3 pressedPositionOffset = new Vector3(0, -0.1f, 0); // 버튼이 내려가는 위치 오프셋
    public float moveSpeed = 2f; // 버튼의 이동 속도

    private Vector3 initialPosition; // 버튼의 초기 위치
    private bool isPressed = false; // 버튼이 눌렸는지 여부
    private float currentWeight = 0f; // 현재 버튼 위의 총 무게

    [SerializeField] private List<Rigidbody> objectsOnButton = new List<Rigidbody>(); // 버튼 위에 있는 오브젝트 리스트

    void Start()
    {
        initialPosition = transform.position; // 버튼의 초기 위치 저장
    }

    void Update()
    {
        // 현재 무게가 임계값 이상이고 버튼이 아직 눌리지 않은 경우
        if (currentWeight >= weightThreshold && !isPressed)
        {
            isPressed = true;
            StopAllCoroutines();
            StartCoroutine(MoveButton(initialPosition + pressedPositionOffset));
            ActivateConnectedScript();
        }
        // 현재 무게가 임계값 미만이고 버튼이 눌린 상태인 경우
        else if (currentWeight < weightThreshold && isPressed)
        {
            isPressed = false;
            StopAllCoroutines();
            StartCoroutine(MoveButton(initialPosition));
            DeactivateConnectedScript();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody rb = collision.collider.attachedRigidbody;
        if (rb != null && !objectsOnButton.Contains(rb))
        {
            objectsOnButton.Add(rb);
            currentWeight += rb.mass;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        Rigidbody rb = collision.collider.attachedRigidbody;
        if (rb != null && objectsOnButton.Contains(rb))
        {
            objectsOnButton.Remove(rb);
            currentWeight -= rb.mass;
        }
    }

    private IEnumerator MoveButton(Vector3 targetPosition)
    {
        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPosition;
    }

    private void ActivateConnectedScript()
    {
        // 버튼이 눌렸을 때 실행할 스크립트나 기능을 여기에 추가하세요.
        Debug.Log("버튼이 눌렸습니다.");
    }

    private void DeactivateConnectedScript()
    {
        // 버튼이 올라왔을 때 실행할 스크립트나 기능을 여기에 추가하세요.
        Debug.Log("버튼이 올라왔습니다.");
    }
}



