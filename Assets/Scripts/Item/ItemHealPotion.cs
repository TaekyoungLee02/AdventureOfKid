using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHealPotion : Item, IUseable
{
    private int value;

    public void Use()
    {
        Debug.Log($"�÷��̾��� ü���� {value} ȸ������ϴ�.");
    }

    public void Init(ItemData data)
    {
        value = data.value;
    }
}
