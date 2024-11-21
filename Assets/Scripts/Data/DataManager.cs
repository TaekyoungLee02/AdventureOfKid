using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using Newtonsoft.Json;
using DG.Tweening.Plugins.Core.PathCore;

public class DataManager : SingletonBase<DataManager>
{
    private DataBase<StageData> _stageData = new();
    private DataBase<ItemDataBase> _itemData = new();
    private DataBase<StructureData> _structureData = new();

    public DataBase<StageData> StageData { get { return _stageData; } }
    public DataBase<ItemDataBase> ItemData { get { return _itemData; } }
    public DataBase<StructureData> StructureData { get { return _structureData; } }

    public enum DataType
    {
        StageData,
        ItemData,
        StructureData,
    }

    private new void Awake()
    {
        base.Awake();

        _itemData = LoadData<ItemDataBase>(DataType.ItemData);
        _stageData = LoadData<StageData>(DataType.StageData);
        _structureData = LoadData<StructureData>(DataType.StructureData);
    }

    private void Start()
    {
        //SaveStageData();

        //_stageData = LoadData<StageData>(DataType.StageData);


    }

    public void SaveStageData()
    {
        var objects = FindObjectsOfType<ObjectSerializer>();

        List<StageData> stageData = new();

        StageData data = new();

        data.id = 1;
        data.name = "test";

        for (int i = 0; i < objects.Length; i++)
        {
            StageObject obj = objects[i].Serialize();

            data.structures.Add(obj);
        }

        stageData.Add(data);

        string json = JsonConvert.SerializeObject(stageData);

        print(json);

        File.WriteAllText(Application.dataPath + Paths.JsonPathStageData, json);

    }



    private DataBase<T> LoadData<T>(DataType type) where T : DataTypeBase
    {
        string path;
        DataBase<T> dataBase = new();

        switch (type)
        {
            case DataType.StageData: path = Paths.JsonPathStageData; break;
            case DataType.ItemData: path = Paths.JsonPathItem; break;
            case DataType.StructureData: path = Paths.JsonPathStructure; break;

            default: path = ""; break;
        }

        StringBuilder sb = new();
        sb.Append(Application.dataPath);
        sb.Append(path);

        path = sb.ToString();

        if (!File.Exists(path)) return null;

        string dataText = File.ReadAllText(path);
        var data = JsonConvert.DeserializeObject<List<T>>(dataText);

        dataBase.Initalize(data);

        return dataBase;
    }
}
