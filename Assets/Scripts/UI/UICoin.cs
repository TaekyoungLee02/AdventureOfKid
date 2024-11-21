using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UICoin : MonoBehaviour
{
    private TextMeshProUGUI coinText;
    private int coin;

    private void Awake()
    {
        coinText = GetComponentInChildren<TextMeshProUGUI>();
        coinText.text = coin.ToString();
    }

    void Start()
    {
        UIManager.Instance.AddCoinAction += AddCoin;
        UIManager.Instance.SubstractCoinAction += AddCoin;
        UIManager.Instance.GetCoinFunc += GetCoin;
    }

    private void AddCoin(int amount)
    {
        coin += amount;
        coinText.text = coin.ToString();
    }

    private void SubstractCoint(int amount)
    {
        coin -= amount;
        coinText.text = coin.ToString();
    }

    private int GetCoin()
    {
        return coin;
    }
}
