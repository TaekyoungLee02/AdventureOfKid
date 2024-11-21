using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UICoin : MonoBehaviour
{
    private TextMeshProUGUI coinText;
    private int coin = 1000;

    private void Awake()
    {
        coinText = GetComponentInChildren<TextMeshProUGUI>();
        coinText.text = coin.ToString();
    }

    void Start()
    {
        UIManager.Instance.AddCoinAction += AddCoin;
        UIManager.Instance.SubstractCoinAction += SubstractCoin;
        UIManager.Instance.GetCoinFunc += GetCoin;
    }

    private void AddCoin(int amount)
    {
        coin += amount;
        coinText.text = coin.ToString();
    }

    private void SubstractCoin(int amount)
    {
        coin -= amount;
        coinText.text = coin.ToString();
    }

    private int GetCoin()
    {
        return coin;
    }
}
