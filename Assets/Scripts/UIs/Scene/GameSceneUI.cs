
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameSceneUI : UIScene
{
    // 게임씬에서 사용할 고정 UI

    [SerializeField] private Transform quickSlots;

    private List<QuickSlot> slots = new List<QuickSlot>();

    private void Start()
    {
        InitQuickSlots();
    }

    private void OnEnable()
    {
        CharacterManager.Instance.OnItemSlotDataChanged -= ItemSlotDataChanged;
        CharacterManager.Instance.OnItemSlotDataChanged += ItemSlotDataChanged;
        CharacterManager.Instance.OnQuickSlotEquipped -= QuickSlotEquipped;
        CharacterManager.Instance.OnQuickSlotEquipped += QuickSlotEquipped;
        CharacterManager.Instance.OnQuickSlotUnEquipped -= QuickSlotUnEquipped;
        CharacterManager.Instance.OnQuickSlotUnEquipped += QuickSlotUnEquipped;
    }
    private void InitQuickSlots()
    {
        for (int i = 0; i < CharacterManager.Instance.QuickSlotCount; i++)
        {
            GameObject go = ResourceManager.Instance.Instantiate("UI/Slot/QuickSlot", quickSlots);
            go.name = $"QuickSlot_{i}";
            QuickSlot quickSlot = go.GetComponent<QuickSlot>();
            quickSlot.InitQuickSlot(i + 1);
            slots.Add(quickSlot);
        }
    }

    private void QuickSlotEquipped(ItemSlotData data)
    {
        QuickSlot quickSlot = GetQuickSlotByIndex(data.slotIndex);
        if (quickSlot == null)
        {
            quickSlot = GetQuickSlotByKey(data.quickSlotKey);
            if (quickSlot == null) return;
        }
        quickSlot.SetItemData(data);
    }

    private void QuickSlotUnEquipped(ItemSlotData data)
    {
        QuickSlot quickSlot = GetQuickSlotByIndex(data.slotIndex);
        if (quickSlot == null)
        {
            quickSlot = GetQuickSlotByKey(data.quickSlotKey);
            if (quickSlot == null) return;
        }
        quickSlot.Clear();
    }

    private void ItemSlotDataChanged(ItemSlotData data)
    {
        QuickSlot quickSlot = GetQuickSlotByKey(data.quickSlotKey);
        if (quickSlot == null) return;

        quickSlot.UpdateUI();
    }

    private QuickSlot GetQuickSlotByIndex(int index)
    {
        foreach (QuickSlot slot in slots)
        {
            if (slot.itemSlotData == null) continue;

            if (slot.itemSlotData.slotIndex == index)
            {
                return slot;
            }
        }

        return null;
    }
    private QuickSlot GetQuickSlotByKey(int key)
    {
        foreach (QuickSlot slot in slots)
        {
            if (slot.KeyNumber == key)
            {
                return slot;
            }
        }

        return null;
    }
}
