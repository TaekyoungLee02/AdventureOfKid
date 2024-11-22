using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Customizing : MonoBehaviour
{
    [SerializeField] private GameObject[] playerParts = new GameObject[8];
    [SerializeField] private GameObject customImages;
    [SerializeField] private Text[] numberText;

    private SavePlayerCustomizeData savePlayerCustomizeData;

    private Dictionary<string, int> customData = new();

    private const int MaxParts = 17;

    GameObject[] imageObject;

    public Dictionary<string, int> CustomData
    {
        get { return customData; }
    }

    void Start()
    {
        CreateImageObject();
        UpdateAllCategoryCounts();
        UIManager.Instance.UpdateCustomInfoAction += UpdateAllCategoryCounts;
        gameObject.SetActive(false);

        savePlayerCustomizeData = GetComponent<SavePlayerCustomizeData>();
    }

    public void UpdateAllCategoryCounts()
    {
        var parts = FindObjectOfType<PlayerCustomizeApplier>().gameObject;

        for(int i = 0; i < parts.transform.childCount; ++i)
            playerParts[i] = parts.transform.GetChild(i).gameObject;

        for (int i = 0; i < playerParts.Length; i++)
        {
            int spriteCount = CountSpritesInCategory(i);
            numberText[i].text = spriteCount.ToString();
        }
    }

    int CountSpritesInCategory(int index)
    {
        int count = 0;
        for (int i = 0; i < playerParts[index].transform.childCount; i++)
        {
            playerParts[index].transform.GetChild(i).AddComponent<Image>();

            if (playerParts[index].transform.GetChild(i).GetComponent<Image>().sprite != null)
            {
                count++;
            }
        }
        return count;
    }

    void CreateImageObject()
    {
        imageObject = new GameObject[MaxParts];

        for (int i = 0; i < MaxParts; i++)
        {
            imageObject[i] = new GameObject("Image", typeof(Image));
            imageObject[i].transform.SetParent(customImages.transform);
            imageObject[i].AddComponent<Button>();
            imageObject[i].SetActive(false);
        }
    }

    GameObject GetImageObject(int index)
    {
        return imageObject[index];
    }

    void RestAllImage()
    {
        for (int i = 0; i < imageObject.Length; i++)
        {
            imageObject[i].SetActive(false);
            imageObject[i].GetComponent<Image>().sprite = null;
            imageObject[i].GetComponent<Button>().onClick.RemoveAllListeners();
        }
    }

    public void ChangeButton(int index)
    {
       RestAllImage();
       customImages.name = playerParts[index].name;
       for (int i = 0; i < playerParts[index].transform.childCount; i++)
       {
            if (playerParts[index].transform.GetChild(i).GetComponent<Image>().sprite != null)
            {
                GetImageObject(i).GetComponent<Image>().sprite = playerParts[index].transform.GetChild(i).GetComponent<Image>().sprite;
                if (GetImageObject(i).GetComponent<Image>().sprite != null)
                {
                    GetImageObject(i).SetActive(true);
                    GetImageObject(i).name = playerParts[index].transform.GetChild(i).name;
                    GetImageObject(i).GetComponent<Button>().onClick.AddListener(EquipCustom);
                }
            }
       }
    }

    void EquipCustom()
    {
        GameObject clickButton = EventSystem.current.currentSelectedGameObject;

        foreach (var part in playerParts)
        {
            string currentCategoryName = customImages.name;

            if (currentCategoryName == part.name)
            {
                
                for (int i = 0; i < part.transform.childCount; i++)
                {
                    if (part.transform.GetChild(i).name == clickButton.name)
                    {
                        part.transform.GetChild(i).gameObject.SetActive(true);
                        if (customData.ContainsKey(part.name))
                        {
                            customData.Remove(part.name);
                        }
                        customData.Add(part.name, i);
                    }
                    else
                    {
                        part.transform.GetChild(i).gameObject.SetActive(false);
                    }
                }
            }
        }

        savePlayerCustomizeData.SaveData(customData);
    }
}