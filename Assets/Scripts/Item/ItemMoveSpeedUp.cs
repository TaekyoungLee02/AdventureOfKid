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
        Debug.Log($"플레이어의 스피드가 {value} 올랐습니다.");
        yield return new WaitForSeconds(dealyTime);
        Debug.Log($"플레어의 스피드가 정상화 되었습니다.");
    }

    public void Init(ItemData data)
    {
        value = data.value;
    }
}
