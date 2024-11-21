using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIShop : MonoBehaviour
{
    [SerializeField] private GameObject[] itemImageFrame;

    Image[] itemImage;
    Text[] itemGoldText;

    private void Start()
    {
        ItemAllSetting();
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
                    itemImage[j] = itemImageFrame[i].transform.GetChild(j).gameObject.GetComponent<Image>();
                }
                else
                {
                    itemGoldText[j] = itemImageFrame[i].transform.GetChild(j).GetComponent<Text>();
                }
            }
        }
    }

    void RestAllImage()
    {
        
    }

    public void CategoryButton(int index)
    {

    }

    public void BuyButton(int index)
    {

    }
}
