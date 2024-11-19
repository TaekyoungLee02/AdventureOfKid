using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class JumpControlUI : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Image buttonImage;

    [SerializeField]
    private float buttonAlphaWhenPushed;
    private float buttonAlphaDefault;

    public event Action<bool> OnTouchscreenJump;

    private void Awake()
    {
        buttonImage = GetComponent<Image>();
        buttonAlphaDefault = buttonImage.color.a;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnTouchscreenJump?.Invoke(true);

        buttonImage.color = new Color(buttonImage.color.r, buttonImage.color.g, buttonImage.color.b, buttonAlphaWhenPushed);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        OnTouchscreenJump?.Invoke(false);

        buttonImage.color = new Color(buttonImage.color.r, buttonImage.color.g, buttonImage.color.b, buttonAlphaDefault);
    }
}
