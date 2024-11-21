using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;

    public int MaxHealth { get { return maxHealth; } }
    public int CurrentHealth { get {return currentHealth; } }

    public void ChangeHealth(int value)
    {
        currentHealth += value;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (currentHealth == 0) Debug.Log("Gameover");
    }
}
