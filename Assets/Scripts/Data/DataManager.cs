using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using Newtonsoft.Json;
using DG.Tweening.Plugins.Core.PathCore;
using UnityEditor.Experimental.GraphView;

public class DataManager : SingletonBase<DataManager>
{
    private DataBase<StageData> _stageData = new();
    private DataBase<ItemDataBase> _itemData = new();
    private DataBase<StructureData> _structureData = new();
    private DataBase<PlayerData> _playerData = new();
    private DataBase<EnemyData> _enemyData = new();

    public DataBase<StageData> StageData { get { return _stageData; } }
    public DataBase<ItemDataBase> ItemData { get { return _itemData; } }
    public DataBase<StructureData> StructureData { get { return _structureData; } }
    public DataBase<PlayerData> PlayerData { get { return _playerData; } }
    public DataBase<EnemyData> EnemyData { get { return _enemyData; } }

    public enum DataType
    {
        StageData,
        ItemData,
        StructureData,
        PlayerData,
        EnemyData,
    }

    private new void Awake()
    {
        base.Awake();

        _itemData = LoadData<ItemDataBase>(DataType.ItemData);
        _stageData = LoadData<StageData>(DataType.StageData);
        _structureData = LoadData<StructureData>(DataType.StructureData);
        _playerData = LoadData<PlayerData>(DataType.PlayerData);
        _enemyData = LoadData<EnemyData>(DataType.EnemyData);
    }

    private void Start()
    {
        //SaveStageData();

        //_stageData = LoadData<StageData>(DataType.StageData);

        //SavePlayerCustomizeData s = new();
        //s.SaveData(new Dictionary<string, int>());
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

            if (objects[i].ID / 1000000 == 50) data.items.Add(obj);
            else if (objects[i].ID / 1000000 == 20) data.enemies.Add(obj);
            else data.structures.Add(obj);

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
            case DataType.PlayerData: path = Paths.JsonPathPlayer; break;
            case DataType.EnemyData: path = Paths.JsonPathEnemy; break;

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
