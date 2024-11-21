using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    
    private bool isPause;

    public Action<int> AddCoinAction;
    public Func<int> GetCoinFunc;

    public static UIManager Instance
    {
        get
        {
            if (instance == null)
            {
                Init();
            }
            return instance;
        }
    }

    private void Awake()
    {
        RemoveDuplicates();

        DOTween.Init();

        GameObject uiCanvas = Resources.Load<GameObject>("Prefabs/UICanvas");
        Instantiate(uiCanvas);
    }

    private static void Init()
    {
        if (instance == null)
        {
            GameObject gameObj = new GameObject(nameof(UIManager));
            instance = gameObj.AddComponent<UIManager>();
            DontDestroyOnLoad(gameObj);
        }
    }

    private void RemoveDuplicates()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PauseClient()
    {
        isPause = !isPause;
        Time.timeScale = isPause ? 0f : 1f;

        int a = GetCoin();
    }

    public void AddCoin(int amount)
    {
        AddCoinAction?.Invoke(amount);
    }

    public int GetCoin()
    {
        return (int)GetCoinFunc?.Invoke();
    }
}
