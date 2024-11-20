

using System.Collections.Generic;
using UnityEngine;

public class StructureLoader
{
	private DataBase<StructureData> _data;
	private Dictionary<int, GameObject> _loadedData = new();

	public StructureLoader()
	{
		_data = DataManager.Instance.StructureData;
	}

	public void LoadData(int id)
	{

	}
}