using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    [Header("아이템 데이터")]
    public ItemSlotData itemSlotData;

    [Header("아이템 슬롯 UI")]
    public Image icon;
    public TextMeshProUGUI countText;
    public Outline selectedOutline;
    public Transform equippedMark;

    private InventoryPopupUI inventory;

    private void Start()
    {
        
    }

    public void SetInventory(InventoryPopupUI inventory)
    {
        this.inventory = inventory;
    }

    public void SetData(ItemSlotData itemSlotData)
    {
        this.itemSlotData = itemSlotData;
    }

    public void OnItemClick()
    {
        inventory.SelectItem(this);
        Debug.Log(itemSlotData.slotIndex);
    }

    public void Select()
    {
        selectedOutline.enabled = true;
    }
    public void Deselect()
    {
        selectedOutline.enabled = false;
    }

    public void Clear()
    {
        icon.sprite = null;
        icon.gameObject.SetActive(false);
        countText.text = string.Empty;
        selectedOutline.enabled = false;
        equippedMark.gameObject.SetActive(false);
    }
}
