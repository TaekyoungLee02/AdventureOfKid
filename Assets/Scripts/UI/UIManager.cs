using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    private ObjectPool effectPools;
    private GameObject curUseEffect;

    private GameObject[][] ga;
    private bool isPause;

    public Action<int> AddCoinAction;

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

        ga = new GameObject[0][];
    }

    public void PauseClient()
    {
        isPause = !isPause;
        Time.timeScale = isPause ? 0f : 1f;
    }

    public void AddCoin(int amount)
    {
        AddCoinAction?.Invoke(amount);
    }
}
