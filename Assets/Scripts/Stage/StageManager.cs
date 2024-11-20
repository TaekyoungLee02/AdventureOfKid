using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    DataBase<StageData> _stageData;


    public void SpawnFromData(int id)
    {
        var currentStageData = _stageData.GetData(id);

        var currentStructure = currentStageData.structures;

        for (int i = 0; i < currentStructure.Count; i++)
        {
            
        }
    }
}
