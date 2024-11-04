using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildingSlot : MonoBehaviour
{
    public BuildSO buildSO;

    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI descriptionText;

    [SerializeField] private NeedResourceSlot woodSlot;
    [SerializeField] private NeedResourceSlot oreSlot;
    [SerializeField] private NeedResourceSlot foodSlot;
    [SerializeField] private NeedResourceSlot peopleSlot;

    public void SetData(BuildSO buildSO)
    {
        this.buildSO = buildSO;

        if (buildSO == null)
        {
            return;
        }

        ResourceSlotClear();
        ShowResourceSlot(buildSO);
        titleText.text = buildSO.displayName;
        descriptionText.text = buildSO.description;
        icon.sprite = buildSO.icon;
    }

    private void ResourceSlotClear()
    {
        woodSlot.gameObject.SetActive(false);
        oreSlot.gameObject.SetActive(false);
        foodSlot.gameObject.SetActive(false);
        peopleSlot.gameObject.SetActive(false);
    }

    private void ShowResourceSlot(BuildSO buildSO)
    {
        if (buildSO.NeedResources == null)
            return;

        foreach (ResourceData resource in buildSO.NeedResources)
        {
            switch (resource.needResourceType)
            {
                case Defines.ResourceType.Wood:
                    woodSlot.gameObject.SetActive(true);
                    woodSlot.SetData(resource);
                    continue;
                case Defines.ResourceType.Ore:
                    oreSlot.gameObject.SetActive(true);
                    oreSlot.SetData(resource);
                    continue;
                case Defines.ResourceType.Food:
                    foodSlot.gameObject.SetActive(true);
                    foodSlot.SetData(resource);
                    continue;
                case Defines.ResourceType.People:
                    peopleSlot.gameObject.SetActive(true);
                    peopleSlot.SetData(resource);
                    continue;
            }
        }

    }

    public void HideSlot()
    {
        gameObject.SetActive(false);
    }
    public void ShowSlot()
    {
        gameObject.SetActive(true);
    }

    public void OnButtonClick()
    {
        BuildPopupUI popup = UIManager.Instance.PeekPopupUI<BuildPopupUI>();
        popup.Build(buildSO);
    }
}
