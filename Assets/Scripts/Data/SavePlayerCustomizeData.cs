using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SavePlayerCustomizeData : MonoBehaviour
{
    public void SaveData(Dictionary<string, int> customizeData)
    {
        List<PlayerData> playerData = new();

        PlayerData data = new();

        data.customizeData = customizeData;
        data.path = Paths.ObjectPathPlayer;

        playerData.Add(data);

        string json = JsonConvert.SerializeObject(playerData);

        print(json);

        File.WriteAllText(Application.streamingAssetsPath + Paths.JsonPathPlayer, json);

    }
}
