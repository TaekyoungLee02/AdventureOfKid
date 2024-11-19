using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHealPotion : Item, IUseable
{
    private int value;

    public void Use()
    {
        Debug.Log($"플레이어의 체력이 {value} 회복됬습니다.");
    }

    public void Init(ItemData data)
    {
        value = data.value;
    }
}
