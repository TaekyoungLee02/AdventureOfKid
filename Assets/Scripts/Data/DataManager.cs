using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using Newtonsoft.Json;

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
        ItemData
    }

    private new void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        SaveStageData();

        //_stageData = LoadData<StageData>(DataType.StageData);

        _itemData = LoadData<ItemDataBase>(DataType.ItemData);
    }

    public void SaveStageData()
    {
        var objects = FindObjectsOfType<SceneObject>();

        List<ItemDataBase> stageData = new();

        ItemDataBase data = new();

        data.values.Add(0);
        data.values.Add(1);

        //StageData data = new();

        //data.id = 1;
        //data.name = "test";

        //for (int i = 0; i < objects.Length; i++)
        //{
        //    StageObject obj = new();
        //    obj.position = new(objects[i].transform.position);
        //    obj.rotation = new(objects[i].transform.rotation.eulerAngles);
        //    obj.scale = new(objects[i].transform.localScale);

        //    data.structures.Add(obj);
        //}
        stageData.Add(data);

        string json = JsonConvert.SerializeObject(stageData);

        print(json);

        File.WriteAllText(Application.dataPath + "/structure.json", json);










        //string path;
        //DataBase<T> dataBase = new();


        //if (!File.Exists(path)) return;

        //StringBuilder sb = new();
        //sb.Append(Application.dataPath);
        //sb.Append(path);
        //sb.Append(".json");

        //string dataText = File.ReadAllText(sb.ToString());
        //var data = JsonUtility.FromJson<List<T>>(dataText);

        //dataBase.Initalize(data);
    }



    private DataBase<T> LoadData<T>(DataType type) where T : DataTypeBase
    {
        string path;
        DataBase<T> dataBase = new();

        switch (type)
        {
            case DataType.StageData: path = Paths.JsonPathStageData; break;
            case DataType.ItemData: path = Paths.JsonPathItem; break;

            default: path = ""; break;
        }

        StringBuilder sb = new();
        sb.Append(Application.dataPath);
        sb.Append(path);

        path = sb.ToString();

        if (!File.Exists(path)) return null;

        string dataText = File.ReadAllText(path);
        var data = JsonConvert.DeserializeObject<List<T>>(dataText);

        print(data);

        dataBase.Initalize(data);

        return dataBase;
    }
}
