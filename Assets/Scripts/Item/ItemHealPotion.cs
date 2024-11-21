using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHealPotion : Item, IUseable, IDataInitializer
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

    public void Initialize(List<int> values)
    {
        value = values[0];
    }
}
