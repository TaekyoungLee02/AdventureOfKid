using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public List<IUseable> items;

    void Start()
    {
        
    }

    public IUseable GetItem(int id)
    {
        return items[id];
    }
}
