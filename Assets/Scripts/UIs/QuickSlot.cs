using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuickSlot : MonoBehaviour
{
    [Header("아이템 데이터")]
    public ItemSlotData itemSlotData;

    [Header("아이템 슬롯 UI")]
    public Image icon;
    public TextMeshProUGUI countText;
    public TextMeshProUGUI keyText;

    private bool isChangeItem;
    public int KeyNumber { get; private set; }

    private int selectedItemSlotDataIndex;

    public void InitQuickSlot(int keyNumber, bool isChangeItem = false)
    {
        Clear();
        this.isChangeItem = isChangeItem;
        this.KeyNumber = keyNumber;
        keyText.text = keyNumber.ToString();
    }

    public void SelectedItemSlotData(int itemSlotDataIndex)
    {
        selectedItemSlotDataIndex = itemSlotDataIndex;
    }

    public void SetItemData(ItemSlotData itemSlotData)
    {
        this.itemSlotData = itemSlotData;

        if (itemSlotData == null)
        {
            Clear();
            return;
        }

        UpdateUI();
    }

    public void UpdateCount(int count)
    {
        countText.text = count.ToString();
    }

    public void OnItemClick()
    {
        if (isChangeItem)
        {
            CharacterManager.Instance.EquipQuickSlotItem(selectedItemSlotDataIndex, KeyNumber);
            QuickSlotSelectPopupUI popup = UIManager.Instance.PeekPopupUI<QuickSlotSelectPopupUI>();
            if (popup != null) UIManager.Instance.ClosePopupUI(popup);
        }
        else
        {
            if (itemSlotData == null) return;
            CharacterManager.Instance.UseItem(itemSlotData.slotIndex);
        }
    }

    public void UpdateUI()
    {
        if (itemSlotData?.itemSO == null) return;
        icon.sprite = itemSlotData.itemSO.itemIcon;
        icon.gameObject.SetActive(true);
        countText.text = itemSlotData.itemCount.ToString();
    }
    public void Clear()
    {
        itemSlotData = null;
        icon.sprite = null;
        icon.gameObject.SetActive(false);
        countText.text = string.Empty;
    }
}
