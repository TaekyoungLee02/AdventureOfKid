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
        Debug.Log($"플레이어의 스피드가 {Data.Value} 올랐습니다.");
        yield return new WaitForSeconds(dealyTime);
        Debug.Log($"플레어의 스피드가 정상화 되었습니다.");
    }
}
