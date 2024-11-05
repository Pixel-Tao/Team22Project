using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildingSlot : MonoBehaviour
{
    public BuildingSO buildingSO;

    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI descriptionText;

    [SerializeField] private NeedResourceSlot woodSlot;
    [SerializeField] private NeedResourceSlot oreSlot;
    [SerializeField] private NeedResourceSlot foodSlot;
    [SerializeField] private NeedResourceSlot peopleSlot;

    [SerializeField] private bool isDestroyable;
    public bool IsDestroyable => isDestroyable;

    public void SetData(BuildingSO buildSO)
    {
        this.buildingSO = buildSO;

        if (buildSO == null)
        {
            return;
        }

        ResourceSlotClear();
        ShowResourceSlot(buildSO);
        titleText.text = buildSO.displayName;
        descriptionText.text = buildSO.description;
        icon.sprite = buildSO.icon;
        isDestroyable = false;
    }

    private void ResourceSlotClear()
    {
        woodSlot.gameObject.SetActive(false);
        oreSlot.gameObject.SetActive(false);
        foodSlot.gameObject.SetActive(false);
        peopleSlot.gameObject.SetActive(false);
    }

    public void ShowResourceSlot(BuildingSO buildSO, bool isHalfAmount = false)
    {
        if (buildSO.NeedResources == null)
            return;

        foreach (ResourceData resource in buildSO.NeedResources)
        {
            switch (resource.needResourceType)
            {
                case Defines.ResourceType.Wood:
                    woodSlot.gameObject.SetActive(true);
                    woodSlot.SetData(resource, isHalfAmount);
                    continue;
                case Defines.ResourceType.Ore:
                    oreSlot.gameObject.SetActive(true);
                    oreSlot.SetData(resource, isHalfAmount);
                    continue;
                case Defines.ResourceType.Food:
                    foodSlot.gameObject.SetActive(true);
                    foodSlot.SetData(resource, isHalfAmount);
                    continue;
                case Defines.ResourceType.People:
                    peopleSlot.gameObject.SetActive(true);
                    peopleSlot.SetData(resource, false); // 인구수는 온전히 돌려받아야 함
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
        if (IsDestroyable)
        {
            // 철거
            popup.BuildingDestroy();
        }
        else
        {
            // 건설
            popup.Build(buildingSO);
        }
    }
    public void SetDestorySlot()
    {
        // 철거
        ResourceSlotClear();
        titleText.text = "건물 철거";
        descriptionText.text = string.Empty;
        icon.sprite = ResourceManager.Instance.Load<Sprite>("Textures/Icons/ResourceIconSheet/building_destroy");
        isDestroyable = true;
    }
}
