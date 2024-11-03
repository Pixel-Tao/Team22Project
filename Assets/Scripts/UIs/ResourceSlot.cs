using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourceSlot : MonoBehaviour
{
    public TextMeshProUGUI amountText;
    public Defines.ResourceType resourceType;

    private void Start()
    {
        switch (resourceType)
        {
            case Defines.ResourceType.Wood:
                GameManager.Instance.OnWoodCountChanged += SetAmount;
                SetAmount(GameManager.Instance.WoodCount);
                break;
            case Defines.ResourceType.Ore:
                GameManager.Instance.OnOreCountChanged += SetAmount;
                SetAmount(GameManager.Instance.OreCount);
                break;
            case Defines.ResourceType.Food:
                GameManager.Instance.OnFoodCountChanged += SetAmount;
                SetAmount(GameManager.Instance.FoodCount);
                break;
            case Defines.ResourceType.People:
                GameManager.Instance.OnPeopleCountChanged += SetAmount;
                SetAmount(GameManager.Instance.PeopleCount, GameManager.Instance.MaxPeopleCount);
                break;
        }
    }
    public void SetAmount(int amount, int max)
    {
        if (amount <= 0)
            amountText.color = Color.red;
        else
            amountText.color = Color.white;

        amountText.text = $"{amount:#,##0} / {max:#,##0}";
    }
    public void SetAmount(int amount)
    {
        if (amount <= 0)
            amountText.color = Color.red;
        else
            amountText.color = Color.white;

        amountText.text = amount.ToString("#,##0");
    }
}
