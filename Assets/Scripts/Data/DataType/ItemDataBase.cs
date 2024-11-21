using System;
using System.Collections.Generic;

[Serializable]
public class ItemDataBase : DataTypeBase
{
    public ItemType itemType;
    public List<int> values;

    public ItemDataBase()
    {
        values = new();
    }
}
