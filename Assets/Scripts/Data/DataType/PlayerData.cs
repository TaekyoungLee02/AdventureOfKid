using System;
using System.Collections.Generic;

[Serializable]
public class PlayerData : DataTypeBase
{
    public Dictionary<string, int> customizeData;

    public PlayerData()
    {
        customizeData = new();
    }
}
