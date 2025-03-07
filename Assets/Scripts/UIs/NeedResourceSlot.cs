using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NeedResourceSlot : MonoBehaviour
{
    [SerializeField] private Defines.ResourceType resourceType;
    [SerializeField] private TextMeshProUGUI amountText;
    private float amount;

    private void Start()
    {
        amountText.text = amount.ToString("F0");
    }

    public void SetData(ResourceData resourceData, bool isHalfAmount)
    {
        amount = isHalfAmount ? resourceData.amount / 2 : resourceData.amount;
        amountText.text = amount.ToString("F0");
    }
}
