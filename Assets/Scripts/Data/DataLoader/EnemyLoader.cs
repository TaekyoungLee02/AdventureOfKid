

public class EnemyLoader : DataLoader<EnemyData>
{
    public EnemyLoader()
    {
        _data = DataManager.Instance.EnemyData;
    }

    public override bool LoadData(int id)
    {
        var data = _data.GetData(id);
        if (data == null) return false;

        _loadedData.Add(id, data);
        return true;
    }
}