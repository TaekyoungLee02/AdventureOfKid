using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum ItemType
{
    MoveSpeedUp,
    Heal
}

[CreateAssetMenu(fileName = "Item", menuName = "NewItem")]

public class ItemData : ScriptableObject
{
    [Header("Info")]
    public string ItemName;
    public string ItemDescription;
    public ItemType ItemType;
    public int Value;
    public GameObject GameObjectPrefab;
}
