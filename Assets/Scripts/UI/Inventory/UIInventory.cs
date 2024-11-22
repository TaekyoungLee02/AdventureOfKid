using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIInventory : MonoBehaviour
{
    public ItemSlot[] slots;
    public GameObject inventoryWindow;
    public Transform slotPanel;
    //public Transform dropPosition;

    [Header("Select Item")]
    public TextMeshProUGUI selectedItemName;
    public TextMeshProUGUI selectedItemDescription;
    //public TextMeshProUGUI selectedStatName;
    public TextMeshProUGUI selectedStatValue;
    public GameObject useBtn;
    public GameObject equipBtn;
    public GameObject unEquipBtn;
    //public GameObject dropBtn;
    public GameObject hpGroup;

    //private PlayerController _controller;
    //private PlayerCondition _condition;

    private ItemData selectedItem;
    private int selectedItemIdx;

    bool isAction;
    int curEquipIdx;

    // Start is called before the first frame update
    void Start()
    {
        //_controller = CharacterManager.Instance.Player._controller;
        //_condition = CharacterManager.Instance.Player._condition;
        //_dropPosition = CharacterManager.Instance.Player._dropPosition;

        //_controller.Inventory += Toggle;
        
        //inventoryWindow.SetActive(false);
        slots = new ItemSlot[slotPanel.childCount];

        for(int i = 0; i < slots.Length; ++i)
        {
            slots[i] = slotPanel.GetChild(i).GetComponent<ItemSlot>();
            slots[i].index = i;
            slots[i].inventory = this;
        }

        ClearSelectedItemWindow();
        UpdateUI();
    }

    private void Update()
    {
        if (!isAction && CharacterManager.Instance.Player != null)
        {
            CharacterManager.Instance.Player.AddItem += AddItem;
            isAction = true;
            inventoryWindow.SetActive(false);
        }
    }

    private void OnEnable()
    {
        ClearSelectedItemWindow();
    }

    private void ClearSelectedItemWindow()
    {
        selectedItemName.text = string.Empty;
        selectedItemDescription.text = string.Empty;
        //selectedStatName.text = string.Empty;
        selectedStatValue.text = string.Empty;

        useBtn.SetActive(false);
        equipBtn.SetActive(false);
        unEquipBtn.SetActive(false);
        //dropBtn.SetActive(false);
    }

    public void Toggle()
    {
        if(IsOpen())
        {
            inventoryWindow.SetActive(false);
        }
        else
        {
            inventoryWindow.SetActive(true);
        }
    }

    public bool IsOpen()
    {
        return inventoryWindow.activeInHierarchy;
    }

    void AddItem(ItemData data)
    {
        // 아이템이 중복 가능한지 canStack
        //if(data._canStack)
        //{
        //    ItemSlot slot = GetItemStack(data);
        //    if(slot != null)
        //    {
        //        slot._quantity++;
        //        UpdateUI();
        //        CharacterManager.Instance.Player._itemData = null;
        //        return;
        //    }
        //}

        // 비어있는 슬롯 가져온다
        ItemSlot emptySlot = GetEmptySlot();

        // 있다면
        if(emptySlot != null )
        {
            emptySlot.item = data;
            emptySlot.quantity = 1;
            UpdateUI();
            return;
        }

        // 없다면
        //ThrowItem(data);
    }

    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; ++i)
        {
            if(slots[i].item != null)
                slots[i].Setting();
            else
                slots[i].Clear();
        }
    }

    ItemSlot GetItemStack(ItemData data)
    {
        for (int i = 0; i < slots.Length; ++i)
        {
            if (slots[i].item == data /*&& _slots[i]._quantity < data._maxStackAmount*/)
            {
                return slots[i];
            }
        }
        return null;
    }

    ItemSlot GetEmptySlot()
    {
        for (int i = 0; i < slots.Length; ++i)
        {
            if (slots[i].item == null)
            {
                return slots[i];
            }
        }
        return null;
    }

    //void ThrowItem(ItemData data)
    //{
    //    Instantiate(data._dropPrefab, _dropPosition.position, 
    //        Quaternion.Euler(Vector3.one * Random.value * 360));
    //}

    public void SelectItem(int index)
    {
        if (slots[index].item == null) return;

        selectedItem = slots[index].item;
        selectedItemIdx = index;

        selectedItemName.text = selectedItem.itemName;
        selectedItemDescription.text = selectedItem.itemDescription;

        //selectedStatName.text = string.Empty;
        selectedStatValue.text = selectedItem.value.ToString();

        useBtn.SetActive(selectedItem.itemType == ItemType.MoveSpeedUp || selectedItem.itemType == ItemType.Heal);
        equipBtn.SetActive(selectedItem.itemType == ItemType.Armor && !slots[index].equipped);
        unEquipBtn.SetActive(selectedItem.itemType == ItemType.Armor && slots[index].equipped);
        //dropBtn.SetActive(true);
    }

    public void OnUseButton()
    {
        if(selectedItem.itemType == ItemType.MoveSpeedUp ||
            selectedItem.itemType == ItemType.Heal)
        {
            switch (selectedItem.itemType)
            {
                case ItemType.MoveSpeedUp:
                    {
                        CharacterManager.Instance.Player.PlayerMovement.Speed = selectedItem.value;
                    }
                    break;
                case ItemType.Heal:
                    {
                        int prevHp = CharacterManager.Instance.Player.Health.GetHealth();
                        CharacterManager.Instance.Player.Health.ChangeHealth(1);
                        int curHp = CharacterManager.Instance.Player.Health.GetHealth();

                        if (prevHp < curHp)
                        {
                            var hp = Resources.Load<GameObject>("Prefabs/Hp");
                            Instantiate(hp, hpGroup.transform);
                        }
                    }
                    break;
            }

            SoundManager.Instance.Play("heal", Sound.Sfx, 0.5f);
            RemoveSelectedItem();
        }
    }

    //public void OnDropButton()
    //{
    //    ThrowItem(_selectedItem);
    //    RemoveSelectedItem();
    //}

    private void RemoveSelectedItem()
    {
        slots[selectedItemIdx].quantity--;

        if(slots[selectedItemIdx].quantity <= 0)
        {
            selectedItem = null;
            slots[selectedItemIdx].item = null;
            selectedItemIdx = -1;
            ClearSelectedItemWindow();
        }

        UpdateUI();
    }

    public void OnEquipButton()
    {
        if (slots[curEquipIdx].equipped)
        {
            UnEquip(curEquipIdx);
        }

        slots[selectedItemIdx].equipped = true;
        curEquipIdx = selectedItemIdx;
        CharacterManager.Instance.Player.Equipment.EquipNew(selectedItem);

        var hp = Resources.Load<GameObject>("Prefabs/Hp");
        Instantiate(hp, hpGroup.transform);
        CharacterManager.Instance.Player.Health.ChangeMaxHealth(1);

        UpdateUI();

        SelectItem(selectedItemIdx);
    }

    public void UnEquipButton()
    {
        UnEquip(selectedItemIdx);
    }

    private void UnEquip(int index)
    {
        slots[index].equipped = false;
        CharacterManager.Instance.Player.Equipment.UnEquip();

        Destroy(hpGroup.transform.GetChild(0).gameObject);
        CharacterManager.Instance.Player.Health.ChangeMaxHealth(-1);

        UpdateUI();

        if(selectedItemIdx == index)
        {
            SelectItem(selectedItemIdx);
        }
    }

    //public void TempAddItem()
    //{
    //    if (tempItem == null) return;

    //    CharacterManager.Instance.Player.AddItem?.Invoke(tempItem);
    //}
}
