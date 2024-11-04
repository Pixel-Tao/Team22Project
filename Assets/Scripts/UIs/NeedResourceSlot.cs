using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NeedResourceSlot : MonoBehaviour
{
    [SerializeField] private Defines.ResourceType resourceType;
    [SerializeField] private TextMeshProUGUI amountText;
    private float amount;

    public void SetData(ResourceData resourceData)
    {
        amount = resourceData.amount;
        amountText.text = amount.ToString("F0");
    }
}
