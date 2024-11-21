using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
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

    private Sprite[] hairSprite;
    private Sprite[] faceSprite;
    private Sprite[] headGearSprite;
    private Sprite[] topSprite;
    private Sprite[] bottomSprite;
    private Sprite[] eyeWearSprite;
    private Sprite[] bagSprite;
    private Sprite[] shoesSprite;
    private Sprite[] gloveSprite;

    private List<Sprite> allSprite = new();

    Image[] itemImage;
    Text[] itemGoldText;

    private void Start()
    {
        ItemAllSetting();
        
    }

    void LoadAllSprite()
    {
        hairSprite = Resources.LoadAll<Sprite>("Sprites/CustomIcon/Hair");
        faceSprite = Resources.LoadAll<Sprite>("Sprites/CustomIcon/Face");
        headGearSprite = Resources.LoadAll<Sprite>("Sprites/CustomIcon/HeadGear");
        topSprite = Resources.LoadAll<Sprite>("Sprites/CustomIcon/Top");
        bottomSprite = Resources.LoadAll<Sprite>("Sprites/CustomIcon/EyeWear");
        eyeWearSprite = Resources.LoadAll<Sprite>("Sprites/CustomIcon/Bag");
        bagSprite = Resources.LoadAll<Sprite>("Sprites/CustomIcon/Bag");
        shoesSprite = Resources.LoadAll<Sprite>("Sprites/CustomIcon/Shoes");
        gloveSprite = Resources.LoadAll<Sprite>("Sprites/CustomIcon/Glove");

        //allSprite.Add(hairSprite, faceSprite, headGearSprite, topSprite, bottomSprite, eyeWearSprite, bagSprite, shoesSprite, gloveSprite);
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

    void RestAllImage()
    {
        
    }

    public void CategoryButton()
    {
        for (int i = 0; i < itemImage.Length; i++)
        {
            itemImageFrame[i].gameObject.SetActive(true);
            itemImage[i].sprite = bagSprite[i];
        }
    }

    public void BuyButton(int index)
    {

    }
}
