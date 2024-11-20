using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class Customizing : MonoBehaviour
{
    [SerializeField] private GameObject[] playerParts;
    [SerializeField] private GameObject customImages;
    [SerializeField] private Text[] numberText;

    GameObject[] imageObject;

    void Start()
    {
        CreateImageObject();
        UpdateAllCategoryCounts();
    }

    void UpdateAllCategoryCounts()
    {
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
            if (playerParts[index].transform.GetChild(i).GetComponent<Image>().sprite != null)
            {
                count++;
            }
        }
        return count;
    }

    void CreateImageObject()
    {
        imageObject = new GameObject[17];

        for (int i = 0; i < 17; i++)
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
            else
            {
                return;
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
                    }
                    else
                    {
                        part.transform.GetChild(i).gameObject.SetActive(false);
                    }
                }
            }
        }
    }
}