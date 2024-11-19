using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMoveSpeedUp : Item, IUseable
{
    private int value;
    float dealyTime = 1.5f;

    public void Use()
    {
        StartCoroutine(CoSpeedUp());
    }

    IEnumerator CoSpeedUp()
    {
        Debug.Log($"�÷��̾��� ���ǵ尡 {value} �ö����ϴ�.");
        yield return new WaitForSeconds(dealyTime);
        Debug.Log($"�÷����� ���ǵ尡 ����ȭ �Ǿ����ϴ�.");
    }

    public void Init(ItemData data)
    {
        value = data.value;
    }
}
