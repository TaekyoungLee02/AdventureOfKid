using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public List<ItemData> itemData;

    void Start()
    {
        
    }

    public GameObject GetItem(int id)
    {
        GameObject go = Instantiate(itemData[id].gameObjectPrefab);

        switch(itemData[id].itemType)
        {
            case ItemType.Heal:
                var poiton = go.AddComponent<ItemHealPotion>();
                poiton.Init(itemData[id]);
                break;
            case ItemType.MoveSpeedUp:
                var speedUp = go.AddComponent<ItemMoveSpeedUp>();
                speedUp.Init(itemData[id]);
                break;
        }
            
        return go;
    }
}
