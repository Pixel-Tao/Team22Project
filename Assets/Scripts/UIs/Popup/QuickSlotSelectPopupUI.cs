using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickSlotSelectPopupUI : UIPopup
{
    [SerializeField] private Transform quickSlots;

    private List<QuickSlot> slots = new List<QuickSlot>();

    private void Awake()
    {
        InitQuickSlots();
    }

    private void UpdateQuickSlots(int selectedSlotDataIndex)
    {
        foreach (var slot in slots)
        {
            ItemSlotData currentQuickSlotData = CharacterManager.Instance.GetQuickSlotData(slot.KeyNumber);
            slot.SetItemData(currentQuickSlotData);
            slot.SelectedItemSlotData(selectedSlotDataIndex);
        }
    }
    private void InitQuickSlots()
    {
        for (int i = 0; i < CharacterManager.Instance.QuickSlotCount; i++)
        {
            GameObject go = ResourceManager.Instance.Instantiate("UI/Slot/QuickSlot", quickSlots);
            go.name = $"QuickSlot_{i}";
            QuickSlot quickSlot = go.GetComponent<QuickSlot>();
            int key = i + 1;
            quickSlot.InitQuickSlot(key, true);
            ItemSlotData itemSlotData = CharacterManager.Instance.GetQuickSlotData(key);
            quickSlot.SetItemData(itemSlotData);
            slots.Add(quickSlot);
        }
    }

    public void SelectedItemIndex(int itemSlotDataIndex)
    {
        UpdateQuickSlots(itemSlotDataIndex);
    }

    public RectTransform GetWindowTransform()
    {
        return quickSlots.GetComponent<RectTransform>();
    }
}
