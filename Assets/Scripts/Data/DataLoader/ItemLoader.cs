using UnityEngine;

public class ItemLoader : DataLoader<ItemDataBase>
{
    public ItemLoader()
    {
        _data = DataManager.Instance.ItemData;
    }

    public override bool LoadData(int id)
    {
        var data = _data.GetData(id);

        var loadedFile = Resources.Load(data.path);
        if (loadedFile == null) return false;

        if (loadedFile is GameObject)
        {
            var go = loadedFile as GameObject;
            var init = go.GetComponent<IDataInitializer>();

            if (init != null)
            {
                init.Initialize(data.values);
            }
        }

        _loadedData.Add(id, loadedFile);
        return true;
    }
}