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
    }

    private void AddCoin(int amount)
    {
        coin += amount;
        coinText.text = coin.ToString();
    }
}