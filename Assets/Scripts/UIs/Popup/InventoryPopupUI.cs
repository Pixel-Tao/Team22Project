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

    private void Start()
    {
        InitSlots();
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
            slot.Clear();
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

    public void SelectItem(ItemSlot slot)
    {
        if (selectedItemSlot != null)
            selectedItemSlot.Deselect();

        selectedItemSlot = slot;
        ItemSlotData slotData = selectedItemSlot.itemSlotData;

        ButtonClear();

        if (slot.itemSlotData.itemSO == null) return;

        useButton.gameObject.SetActive(slotData.itemSO.itemType == Defines.ItemType.Consumable);
        eqEquipButton.gameObject.SetActive(slotData.itemSO.itemType == Defines.ItemType.Equipable && slotData.isEquipped == false);
        eqUnEquipButton.gameObject.SetActive(slotData.itemSO.itemType == Defines.ItemType.Equipable && slotData.isEquipped);
        qsEquipButton.gameObject.SetActive(slotData.itemSO.itemType == Defines.ItemType.Consumable && slotData.quickSlotIndex < 0);
        qsUnEquipButton.gameObject.SetActive(slotData.itemSO.itemType == Defines.ItemType.Consumable && slotData.quickSlotIndex > -1);
        dropButton.gameObject.SetActive(true);

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

        //CharacterManager.Instance.EquipQuickSlotItem(selectedItemSlot.itemSlotData.slotIndex);
    }

    public void OnQuickSlotUnEquipBotton()
    {
        if (selectedItemSlot == null) return;

        CharacterManager.Instance.UnEquipQuickSlotItem(selectedItemSlot.itemSlotData.slotIndex);
    }

    public void OnDropButton()
    {
        if (selectedItemSlot == null) return;

        CharacterManager.Instance.RemoveItemSlotData(selectedItemSlot.itemSlotData.slotIndex);
    }

}
