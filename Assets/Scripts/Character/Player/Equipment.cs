using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    public ItemData curEquip;
    public int EquipStat { get; private set; }

    public void EquipNew(ItemData data)
    {
        UnEquip();
        curEquip = data;
        //EquipStat = (int)data.value;
    }

    public void UnEquip()
    {
        if (curEquip != null)
        {
            curEquip = null;
            EquipStat = 0;
        }
    }
}
