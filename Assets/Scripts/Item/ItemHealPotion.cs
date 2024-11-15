using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHealPotion : MonoBehaviour, IUseable
{
    public ItemData Data;

    public void Use()
    {
        Debug.Log($"플레이어의 체력이 {Data.Value} 회복됬습니다.");
    }
}
