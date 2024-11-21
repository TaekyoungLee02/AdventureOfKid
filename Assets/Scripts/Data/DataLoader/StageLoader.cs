

public class StageLoader : DataLoader<StageData>
{
    public StageLoader()
    {
        _data = DataManager.Instance.StageData;
    }

    public override bool LoadData(int id)
    {
        var data = _data.GetData(id);
        if (data == null) return false;

        _loadedData.Add(id, data);
        return true;
    }
}