using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CustomData : IDataInitializer
{
    public int id;
    public List<int> values;

    public void Initialize(List<int> values)
    {
        this.values = values;
    }
}

public class UIShop : MonoBehaviour
{
    [SerializeField] private GameObject[] itemImageFrame;
    [SerializeField] private GameObject[] playerParts;
    [SerializeField] private Text buyText;
    [SerializeField] private GameObject panel;

    private List<Sprite[]> sprites = new List<Sprite[]>();

    Image[] itemImage;
    Text[] itemGoldText;

    private void Start()
    {
        LoadAllSprite();
        ItemAllSetting();
        gameObject.SetActive(false);

        var parts = FindObjectOfType<PlayerCustomizeApplier>().gameObject;

        for (int i = 0; i < parts.transform.childCount; ++i)
            playerParts[i] = parts.transform.GetChild(i).gameObject;

        for (int i = 0; i < playerParts.Length; i++)
        {
            int spriteCount = CountSpritesInCategory(i);
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

    void LoadAllSprite()
    {
        AddAndSortSprites("Sprites/CustomIcon/Hair");
        AddAndSortSprites("Sprites/CustomIcon/Face");
        AddAndSortSprites("Sprites/CustomIcon/HeadGear");
        AddAndSortSprites("Sprites/CustomIcon/Top");
        AddAndSortSprites("Sprites/CustomIcon/Bottom");
        AddAndSortSprites("Sprites/CustomIcon/EyeWear");
        AddAndSortSprites("Sprites/CustomIcon/Bag");
        AddAndSortSprites("Sprites/CustomIcon/Shoes");
        AddAndSortSprites("Sprites/CustomIcon/Glove");
    }

    void AddAndSortSprites(string path)
    {
        Sprite[] loadedSprites = Resources.LoadAll<Sprite>(path);
        Array.Sort(loadedSprites, (a, b) => ExtractNumber(a.name).CompareTo(ExtractNumber(b.name)));
        sprites.Add(loadedSprites);
    }

    private int ExtractNumber(string name)
    {
        string numberPart = System.Text.RegularExpressions.Regex.Match(name, @"\d+").Value;
        return string.IsNullOrEmpty(numberPart) ? 0 : int.Parse(numberPart);
    }

    public void ExitButton()
    {
        gameObject.SetActive(false);
    }

    void ItemAllSetting()
    {
        itemImage = new Image[itemImageFrame.Length];
        itemGoldText = new Text[itemImageFrame.Length];
        for (int i = 0; i < itemImageFrame.Length; i++)
        {
            for (int j = 0; j < itemImageFrame[i].transform.childCount; j++)
            {
                itemImageFrame[i].SetActive(false);
                if (itemImageFrame[i].transform.GetChild(j).name == "ItemImage")
                {
                    itemImage[i] = itemImageFrame[i].transform.GetChild(j).gameObject.GetComponent<Image>();
                }
                else
                {
                    itemGoldText[i] = itemImageFrame[i].transform.GetChild(j).GetComponent<Text>();
                }
            }
        }
    }

    void ResetAllImage()
    {
        for (int i = 0; i < itemImage.Length; i++)
        {
            itemImageFrame[i].gameObject.SetActive(false);
            itemImage[i].GetComponent<Image>().sprite = null;
            itemImageFrame[i].GetComponent<Button>().onClick.RemoveAllListeners();
        }
    }

    public void CategoryButton(int index)
    {
        ResetAllImage();

        for (int i = 0; i < sprites[index].Length; i++)
        {
            itemImageFrame[i].gameObject.SetActive(true);
            itemImage[i].sprite = sprites[index][i];
            itemImage[i].name = sprites[index][i].name;
            int temp = i;
            itemImageFrame[i].GetComponent<Button>().onClick.AddListener(() => BuyButton(temp));
        }
    }

    void BuyButton(int itemIndex)
    {
        if (UIManager.Instance.GetCoin() >= int.Parse(itemGoldText[itemIndex].text.Substring(0, itemGoldText[itemIndex].text.Length - 1)))
        {
            foreach (var parts in playerParts)
            {
                for (int i = 0; i < parts.transform.childCount; i++)
                {
                    if (itemImage[itemIndex].name == parts.transform.GetChild(i).name && parts.transform.GetChild(i).GetComponent<Image>().sprite == null)
                    {
                        panel.SetActive(true);
                        buyText.text = "구매를 완료했습니다.";
                        parts.transform.GetChild(i).GetComponent<Image>().sprite = itemImage[itemIndex].sprite;
                        UIManager.Instance.SubstractCoin(int.Parse(itemGoldText[itemIndex].text.Substring(0, itemGoldText[itemIndex].text.Length - 1)));
                        UIManager.Instance.UpdateCustomInfo();
                    }
                    else if (itemImage[itemIndex].name == parts.transform.GetChild(i).name && parts.transform.GetChild(i).GetComponent<Image>().sprite != null)
                    {
                        panel.SetActive(true);
                        buyText.text = "이미 가지고 있는 아이템 입니다.";
                    }
                }
            }
        }
        else
        {
            panel.SetActive(true);
            buyText.text = "골드가 부족합니다.";
        }
    }

    public void ExitPanel()
    {
        panel.SetActive(false);
    }
}
