using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public Item item;
    public List<ItemData> itemData;
    public GameObject randomItemBox;

    public GameObject GetItem(int id)
    {
        GameObject go = Instantiate(itemData[id].gameObjectPrefab);
        go.transform.position = randomItemBox.transform.position + new Vector3(0, 0.5f, 0);
        item.Rotation(go);

        switch (itemData[id].itemType)
        {
            case ItemType.Heal:
                var poiton = go.AddComponent<ItemHealPotion>();
                poiton.Init(itemData[id]);
                break;
            case ItemType.MoveSpeedUp:
                var speedUp = go.AddComponent<ItemMoveSpeedUp>();
                speedUp.Init(itemData[id]);
                break;
            case ItemType.Armor:
                var armor = go.AddComponent<ItemArmor>();
                break;
        }
            
        return go;
    }

    public void ItemSpawn()
    {
        int rnd = Random.Range(0, itemData.Count);
        GetItem(rnd);
    }
}