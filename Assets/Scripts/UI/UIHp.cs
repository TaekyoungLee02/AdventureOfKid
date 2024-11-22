using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIHp : MonoBehaviour
{
    private GameObject hpGroup;
    private int curHp;

    bool isSet;

    private void Awake()
    {
        hpGroup = gameObject;
    }

    private void Update()
    {
        if (!isSet && CharacterManager.Instance.Player != null)
        {
            UIManager.Instance.ChangeHpAction += ChangeHp;
            curHp = CharacterManager.Instance.Player.Health.GetHealth();
            isSet = true;
        }
    }

    private void ChangeHp(int amount)
    {
        int changeHp = CharacterManager.Instance.Player.Health.GetHealth();

        if(curHp > changeHp)
            Destroy(hpGroup.transform.GetChild(0).gameObject);
    }
}
