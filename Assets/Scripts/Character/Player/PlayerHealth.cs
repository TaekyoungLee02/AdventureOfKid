using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;

    public int MaxHealth { get { return maxHealth; } }
    public int CurrentHealth { get {return currentHealth; } }

    private void Start()
    {
        UIManager.Instance.ChangeHpAction += ChangeHealth;
        UIManager.Instance.ChangeMaxHpAction += ChangeMaxHealth;
        UIManager.Instance.GetHpAction += GetHealth;
    }

    public void ChangeHealth(int value)
    {
        currentHealth += value;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (currentHealth == 0) Debug.Log("Gameover");
    }

    public void ChangeMaxHealth(int value)
    {
        maxHealth += value;
        currentHealth += value;
    }

    public int GetHealth()
    {
        return currentHealth;
    }

    public void TakePhysicalDamage(int damage)
    {
        ChangeHealth(-1);
    }
}
