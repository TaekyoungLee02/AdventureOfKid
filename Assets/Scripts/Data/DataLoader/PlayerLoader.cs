using UnityEngine;

public class PlayerLoader : DataLoader<PlayerData>
{
    public PlayerLoader()
    {
        _data = DataManager.Instance.PlayerData;
    }

    public override bool LoadData(int id)
    {
        var data = _data.GetData(id);

        var loadedFile = Resources.Load<GameObject>(data.path);
        if (loadedFile == null) return false;

        var cus = loadedFile.GetComponentInChildren<PlayerCustomizeApplier>();

        if (data.customizeData.Count != 0)
            cus.ApplyCustomize(data.customizeData);

        _loadedData.Add(id, loadedFile);
        return true;
    }
}