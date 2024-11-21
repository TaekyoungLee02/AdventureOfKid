using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    private StageLoader stageLoader;
    private StructureLoader structureLoader;
    private ItemLoader itemLoader;
    private PlayerLoader playerLoader;


    private void Start()
    {
        stageLoader = new();
        structureLoader = new();
        itemLoader = new();
        playerLoader = new();

        SpawnFromData(1);
    }

    public void SpawnFromData(int id)
    {
        stageLoader.LoadData(id);
        var data = stageLoader.GetData(id) as StageData;

        SpawnObject(data.structures, structureLoader);
        SpawnObject(data.items, itemLoader);
        SpawnPlayer(data.playerVector);
    }

    private void SpawnObject<T>(List<StageObject> objects, DataLoader<T> dataLoader) where T : DataTypeBase
    {
        var objectIDSet = new HashSet<int>();

        // 세트에 ID를 추가해 먼저 중복된 ID를 뺀다.
        for (int i = 0; i < objects.Count; i++)
        {
            objectIDSet.Add(objects[i].id);
        }

        // ID에 할당된 오브젝트를 로드한다.
        foreach (var objectID in objectIDSet)
        {
            dataLoader.LoadData(objectID);
        }

        // 로드된 오브젝트를 가져와 씬에 배치한다
        for (int i = 0; i < objects.Count; i++)
        {
            var go = Instantiate(dataLoader.GetData(objects[i].id) as GameObject);

            go.transform.SetPositionAndRotation(objects[i].position.ToVector3(), Quaternion.Euler(objects[i].rotation.ToVector3()));
            go.transform.localScale = objects[i].scale.ToVector3();
        }
    }

    private void SpawnPlayer(StageObject playerVector)
    {
        playerLoader.LoadData(0);

        var player = Instantiate(playerLoader.GetData(0) as GameObject);
        player.transform.SetPositionAndRotation(playerVector.position.ToVector3(), Quaternion.Euler(playerVector.rotation.ToVector3()));
        player.transform.localScale = playerVector.scale.ToVector3();
    }
}
