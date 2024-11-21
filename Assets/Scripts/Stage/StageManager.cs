using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    private StageLoader stageLoader;
    private StructureLoader structureLoader;
    private ItemLoader itemLoader;


    private void Start()
    {
        stageLoader = new();
        structureLoader = new();
        itemLoader = new();

        SpawnFromData(1);
    }

    public void SpawnFromData(int id)
    {
        stageLoader.LoadData(id);
        var data = stageLoader.GetData(id) as StageData;

        SpawnObject(data.structures, structureLoader);
        SpawnObject(data.items, itemLoader);
    }

    private void SpawnObject<T>(List<StageObject> objects, DataLoader<T> dataLoader) where T : DataTypeBase
    {
        var objectIDSet = new HashSet<int>();

        // ��Ʈ�� ID�� �߰��� ���� �ߺ��� ID�� ����.
        for (int i = 0; i < objects.Count; i++)
        {
            objectIDSet.Add(objects[i].id);
        }

        // ID�� �Ҵ�� ������Ʈ�� �ε��Ѵ�.
        foreach (var objectID in objectIDSet)
        {
            dataLoader.LoadData(objectID);
        }

        // �ε�� ������Ʈ�� ������ ���� ��ġ�Ѵ�
        for (int i = 0; i < objects.Count; i++)
        {
            var go = Instantiate(dataLoader.GetData(objects[i].id) as GameObject);

            go.transform.SetPositionAndRotation(objects[i].position.ToVector3(), Quaternion.Euler(objects[i].rotation.ToVector3()));
            go.transform.localScale = objects[i].scale.ToVector3();
        }
    }
}
