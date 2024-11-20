using UnityEngine;

public class StructureLoader : DataLoader<StructureData>
{
	public StructureLoader()
	{
		_data = DataManager.Instance.StructureData;
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