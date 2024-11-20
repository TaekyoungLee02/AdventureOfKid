using System;
using System.Collections.Generic;

[Serializable]
public class StageData : DataTypeBase
{
    public List<StageObject> structures;
    public List<StageObject> items;
    public List<StageObject> enemies;

    public StageData()
    {
        structures = new();
        items = new();
        enemies = new();
    }
}
