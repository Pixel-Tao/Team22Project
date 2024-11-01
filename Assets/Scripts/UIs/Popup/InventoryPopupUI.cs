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
    public Button equipButton;
    public Button unEquipButton;
    public Button dropButton;

    private ItemSlot selectedItemSlot;

    private void Awake()
    {
        CharacterManager.Instance.Player.SetInventory(this);
        InitSlots();
    }

    private void Start()
    {
    }

    private void InitSlots()
    {
        foreach (Transform child in itemSlots)
        {
            ItemSlot slot = child.GetComponent<ItemSlot>();
            if (slot != null)
            {
                slot.SetInventory(this);
                slot.Clear();
                slots.Add(slot);
            }
        }
    }

    public void AddItem(ItemSO itemSO)
    {

    }

    public void SelectItem(ItemSlot slot)
    {
        selectedItemSlot = slot;
    }

    public void OnUseButton()
    {

    }

    public void OnEquipButton()
    {

    }

    public void OnUnEquipButton()
    {

    }

    public void OnDropButton()
    {

    }

}
