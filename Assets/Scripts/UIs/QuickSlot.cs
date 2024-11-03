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

    private void Start()
    {
        
    }

    public void InitQuickSlot(int keyNumber)
    {

    }

    public void OnItemClick()
    {
        Debug.Log(itemSlotData.slotIndex);
    }

    public void Select()
    {
        
    }
    public void Deselect()
    {
        
    }

    public void Clear()
    {
        icon.sprite = null;
        icon.gameObject.SetActive(false);
        countText.text = string.Empty;
    }
}
