using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHealPotion : MonoBehaviour, IUseable
{
    public ItemData Data;

    public void Use()
    {
        Debug.Log($"�÷��̾��� ü���� {Data.Value} ȸ������ϴ�.");
    }
}
