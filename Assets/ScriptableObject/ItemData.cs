using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum ItemType
{
    MoveSpeedUp,
    Heal,
    Armor
}

[CreateAssetMenu(fileName = "Item", menuName = "NewItem")]

public class ItemData : ScriptableObject
{
    [Header("Info")]
    public string itemName;
    public string itemDescription;
    public ItemType itemType;
    public int value;
    public GameObject gameObjectPrefab;
    public Sprite icon;
}
