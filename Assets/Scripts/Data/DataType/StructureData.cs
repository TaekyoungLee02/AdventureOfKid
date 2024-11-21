using System;
using System.Collections.Generic;

[Serializable]
public class StructureData : DataTypeBase
{
    public List<int> values;

    public StructureData() 
    { 
        values = new();
    }
}
