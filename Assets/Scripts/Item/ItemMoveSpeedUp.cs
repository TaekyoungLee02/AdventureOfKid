using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMoveSpeedUp : MonoBehaviour, IUseable
{
    public ItemData Data;
    float dealyTime = 1.5f;

    public void Use()
    {
        StartCoroutine(CoSpeedUp());
    }

    IEnumerator CoSpeedUp()
    {
        Debug.Log($"�÷��̾��� ���ǵ尡 {Data.Value} �ö����ϴ�.");
        yield return new WaitForSeconds(dealyTime);
        Debug.Log($"�÷����� ���ǵ尡 ����ȭ �Ǿ����ϴ�.");
    }
}
