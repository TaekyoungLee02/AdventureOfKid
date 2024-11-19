using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class VirtualJoystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    private RectTransform rectTransform;
    private RectTransform controller;
    private Vector2 touchPosition;

    public event Action<Vector2> OnTouchscreenMove;
    public Vector2 MovePosition { get { return touchPosition; } }

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        controller = transform.GetChild(0).GetComponent<RectTransform>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        touchPosition = Vector2.zero;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, eventData.position, eventData.pressEventCamera, out touchPosition))
        {
            touchPosition.x = touchPosition.x / rectTransform.sizeDelta.x;
            touchPosition.y = touchPosition.y / rectTransform.sizeDelta.y;

            touchPosition = touchPosition.magnitude > 1 ? touchPosition.normalized : touchPosition;

            controller.anchoredPosition = new Vector2(
                touchPosition.x * (rectTransform.sizeDelta.x / 2),
                touchPosition.y * (rectTransform.sizeDelta.y / 2));
        }

        OnTouchscreenMove?.Invoke(touchPosition);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        touchPosition = Vector2.zero;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, eventData.position, eventData.pressEventCamera, out touchPosition))
        {
            touchPosition.x = touchPosition.x / rectTransform.sizeDelta.x;
            touchPosition.y = touchPosition.y / rectTransform.sizeDelta.y;

            touchPosition = touchPosition.magnitude > 1 ? touchPosition.normalized : touchPosition;

            controller.anchoredPosition = new Vector2(
                touchPosition.x * (rectTransform.sizeDelta.x / 2),
                touchPosition.y * (rectTransform.sizeDelta.y / 2));
        }

        OnTouchscreenMove?.Invoke(touchPosition);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        controller.anchoredPosition = Vector2.zero;
        OnTouchscreenMove?.Invoke(Vector2.zero);
    }
}
