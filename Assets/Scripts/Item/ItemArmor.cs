using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemArmor : MonoBehaviour, IUseable
{
    public void Use()
    {
        Debug.Log("아머가 장착되었습니다.");
    }
}
