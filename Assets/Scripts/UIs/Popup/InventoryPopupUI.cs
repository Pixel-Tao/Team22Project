using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPopupUI : UIPopup
{
    private List<ItemSlot> slots = new List<ItemSlot>();

    public Transform itemSlots;

    public TextMeshProUGUI itemTitleText;
    public TextMeshProUGUI itemDescriptionText;
    public TextMeshProUGUI itemEffectNameText;
    public TextMeshProUGUI itemEffectValueText;

    public Button useButton;
    public Button eqEquipButton;
    public Button eqUnEquipButton;
    public Button qsEquipButton;
    public Button qsUnEquipButton;
    public Button dropButton;

    private ItemSlot selectedItemSlot;

    private void Awake()
    {
        CharacterManager.Instance.OnItemSlotDataChanged -= ItemSlotDataChanged;
        CharacterManager.Instance.OnItemSlotDataChanged += ItemSlotDataChanged;
    }

    private void Start()
    {
        InitSlots();
    }

    private void ItemSlotDataChanged(ItemSlotData data)
    {
        ItemSlot slot = GetItemSlot(data);
        if (slot == null) return;

        if (data.itemSO == null)
        {
            slot.Clear();
            ButtonClear();
        }
        else
        {
            slot.UpdateUI();
            UpdateButton(slot.itemSlotData);
        }
    }

    private void OnEnable()
    {
        InitInventory();
    }

    private void InitSlots()
    {
        int itemSlotCount = CharacterManager.Instance.ItemSlotCount;
        GameObject itemSlotPrefab = ResourceManager.Instance.Load<GameObject>("Prefabs/UI/Slot/ItemSlot");
        if (itemSlotPrefab == null)
        {
            Debug.LogError("ItemSlot Prefab is null");
            return;
        }
        for (int i = 0; i < itemSlotCount; i++)
        {
            GameObject itemGo = Instantiate(itemSlotPrefab, itemSlots);
            itemGo.name = $"ItemSlot_{i}";
            ItemSlot slot = itemGo.GetComponent<ItemSlot>();
            ItemSlotData itemSlotData = CharacterManager.Instance.GetItemSlotData(i);
            slot.SetInventory(this);
            slot.SetData(itemSlotData);
            slots.Add(slot);
        }

        ButtonClear();
    }
    private void InitInventory()
    {
        if (selectedItemSlot != null)
            selectedItemSlot.Deselect();
        selectedItemSlot = null;
        ButtonClear();
        DescriptionClear();
    }
    private void ButtonClear()
    {
        useButton.gameObject.SetActive(false);
        eqEquipButton.gameObject.SetActive(false);
        eqUnEquipButton.gameObject.SetActive(false);
        qsEquipButton.gameObject.SetActive(false);
        qsUnEquipButton.gameObject.SetActive(false);
        dropButton.gameObject.SetActive(false);
    }

    private void UpdateButton(ItemSlotData slotData)
    {
        if (slotData?.itemSO == null) return;

        if (slotData.itemSO.itemType == Defines.ItemType.Consumable)
        {
            useButton.gameObject.SetActive(true);
            qsEquipButton.gameObject.SetActive(slotData.quickSlotKey < 0);
            qsUnEquipButton.gameObject.SetActive(slotData.quickSlotKey > -1);
            eqEquipButton.gameObject.SetActive(false);
            eqUnEquipButton.gameObject.SetActive(false);
        }
        else if (slotData.itemSO.itemType == Defines.ItemType.Equipable)
        {
            useButton.gameObject.SetActive(false);
            eqEquipButton.gameObject.SetActive(slotData.isEquipped == false);
            eqUnEquipButton.gameObject.SetActive(slotData.isEquipped);
            qsEquipButton.gameObject.SetActive(slotData.quickSlotKey < 0);
            qsUnEquipButton.gameObject.SetActive(slotData.quickSlotKey > -1);
        }
        else
        {
            useButton.gameObject.SetActive(false);
            eqEquipButton.gameObject.SetActive(false);
            eqUnEquipButton.gameObject.SetActive(false);
            qsEquipButton.gameObject.SetActive(false);
            qsUnEquipButton.gameObject.SetActive(false);
        }

        dropButton.gameObject.SetActive(true);
    }
    private void DescriptionClear()
    {
        itemTitleText.text = string.Empty;
        itemDescriptionText.text = string.Empty;
        itemEffectNameText.text = string.Empty;
        itemEffectValueText.text = string.Empty;
    }
    private void UpdateDescription(ItemSlotData slotData)
    {
        if (slotData.itemSO == null)
        {
            DescriptionClear();
            return;
        }

        itemTitleText.text = slotData.itemSO.displayName;
        itemDescriptionText.text = slotData.itemSO.description;
        string names = string.Empty;
        string values = string.Empty;
        foreach (var consumable in slotData.itemSO.consumables)
        {
            names += $"{consumable.consumeableType}\n";
            values += $"{consumable.amount}\n";
        }
        itemEffectNameText.text = names;
        itemEffectValueText.text = values;
    }

    public void SelectItem(ItemSlot slot)
    {
        if (selectedItemSlot != null)
            selectedItemSlot.Deselect();

        selectedItemSlot = slot;
        ItemSlotData slotData = selectedItemSlot.itemSlotData;

        ButtonClear();

        if (slot.itemSlotData.itemSO == null) return;

        UpdateButton(slotData);
        UpdateDescription(slotData);

        selectedItemSlot.Select();
    }

    public void OnUseButton()
    {
        if (selectedItemSlot == null) return;

        CharacterManager.Instance.UseItem(selectedItemSlot.itemSlotData.slotIndex);
    }

    public void OnEquipButton()
    {
        if (selectedItemSlot == null) return;

        CharacterManager.Instance.EquipItem(selectedItemSlot.itemSlotData.slotIndex);
    }

    public void OnUnEquipButton()
    {
        if (selectedItemSlot == null) return;

        CharacterManager.Instance.UnEquipItem(selectedItemSlot.itemSlotData.slotIndex);
    }

    public void OnQuickSlotEquipButton()
    {
        if (selectedItemSlot == null) return;

        QuickSlotSelectPopupUI popup = UIManager.Instance.ShowPopupUI<QuickSlotSelectPopupUI>();
        RectTransform popupRect = popup.GetWindowTransform();
        RectTransform buttonRect = qsEquipButton.GetComponent<RectTransform>();
        popupRect.position = buttonRect.position;
        popup.SelectedItemIndex(selectedItemSlot.itemSlotData.slotIndex);
    }

    public void OnQuickSlotUnEquipBotton()
    {
        if (selectedItemSlot == null) return;

        CharacterManager.Instance.UnEquipItem(selectedItemSlot.itemSlotData.slotIndex);
    }

    public void OnDropButton()
    {
        if (selectedItemSlot == null) return;

        CharacterManager.Instance.RemoveItemSlotData(selectedItemSlot.itemSlotData.slotIndex);
    }

    private ItemSlot GetItemSlot(ItemSlotData itemSlotData)
    {
        foreach (var slot in slots)
        {
            if (slot.itemSlotData == itemSlotData)
                return slot;
        }

        return null;
    }
}
