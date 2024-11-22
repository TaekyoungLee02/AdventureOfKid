

using UnityEngine;

public class EnemyLoader : DataLoader<EnemyData>
{
    public EnemyLoader()
    {
        _data = DataManager.Instance.EnemyData;
    }

    public override bool LoadData(int id)
    {
        var data = _data.GetData(id);

        var loadedFile = Resources.Load(data.path);
        if (loadedFile == null) return false;

        _loadedData.Add(id, loadedFile);
        return true;
    }
}