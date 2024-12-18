using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SetScreenSize : MonoBehaviour
{
    private Camera currentCamera;

    private readonly int width = 1920;
    private readonly int height = 1080;

    private void Awake()
    {
        currentCamera = GetComponent<Camera>();   
    }

    private void OnEnable()
    {
        SetResolution();
    }

    private void SetResolution()
    {
        int currentWidth = Screen.width;
        int currentHeight = Screen.height;

        float currentRate = (float)width / height;
        float defaultRate = (float)currentWidth / currentHeight;

        float newRate;

        Screen.SetResolution(width, (int)((float)currentHeight / currentWidth * width), true);

        if (currentRate == defaultRate) return;
        else if (currentRate > defaultRate)
        {
            newRate = defaultRate / currentRate;
            currentCamera.rect = new Rect(0f, (1f - newRate) / 2f, 1f, newRate);
        }
        else
        {
            newRate = currentRate / defaultRate;
            currentCamera.rect = new Rect((1f - newRate) / 2f, 0f, newRate, 1f);

        }
    }
}
