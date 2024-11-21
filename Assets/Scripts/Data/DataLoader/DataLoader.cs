using System.Collections.Generic;

public abstract class DataLoader<T> where T : DataTypeBase
{
    protected DataBase<T> _data;
    protected Dictionary<int, object> _loadedData = new();

    public abstract bool LoadData(int id);

    public object GetData(int id)
    {
        if (!_loadedData.ContainsKey(id)) return null;

        return _loadedData[id];
    }
}