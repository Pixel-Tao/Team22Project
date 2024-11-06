using Defines;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildPopupUI : UIPopup
{
    private List<BuildingSlot> slots = new List<BuildingSlot>();
    [SerializeField] private Transform buidingSlots;

    private TileObject tileObject;
    private GridLayoutGroup gridLayoutGroup;

    private void Awake()
    {
        InitBuildingSlots();
    }

    private void InitBuildingSlots()
    {
        if (slots.Count > 0) return;

        Defines.BuildingType[] buildingTypes = (Defines.BuildingType[])System.Enum.GetValues(typeof(Defines.BuildingType));
        foreach (Defines.BuildingType buildingType in buildingTypes)
        {
            if (buildingType == Defines.BuildingType.None)
            {
                // 철거를 넣음
                BuildingSlot slot = GenerateBuildingSlot(null);
                slot.SetDestorySlot();
                slots.Add(slot);
                continue;
            }
            else if (buildingType == BuildingType.Castle_Red)
            {
                continue;
            }

            BuildingSO buildSO = ResourceManager.Instance.GetSOBuildingData<BuildingSO>(buildingType);
            if (buildSO == null)
            {
                Debug.LogWarning($"Failed to load building : {buildingType}");
                continue;
            }
            if (buildSO.buildType == BuildType.None || buildSO.buildingType == BuildingType.None)
            {
                continue;
            }

            slots.Add(GenerateBuildingSlot(buildSO));
        }


    }

    private BuildingSlot GenerateBuildingSlot(BuildingSO buildSO)
    {
        GameObject go = ResourceManager.Instance.Instantiate("UI/Slot/BuildingSlot", buidingSlots);
        BuildingSlot slot = go.GetComponent<BuildingSlot>();
        slot.SetData(buildSO);
        return slot;
    }

    public void SelectedTile(TileObject tileObject)
    {
        this.tileObject = tileObject;

        if (tileObject?.TileSO == null)
            return;

        if (tileObject.IsBuilded() && !tileObject.IsGoal())
        {
            ShowDestroySlot();
        }
        else if (tileObject.IsNaturalObject())
        {
            // 자연물
            ShowNaturalObjectBuilding(tileObject);
        }
        else if (tileObject.IsGround())
        {
            // 그냥 땅
            ShowGroundBuilding(tileObject);
        }
    }

    private void ShowNaturalObjectBuilding(TileObject tileObject)
    {
        int showCount = 0;
        foreach (BuildingSlot slot in slots)
        {
            if (slot.IsDestroyable || !tileObject.IsResourceBuilding(slot.buildingSO.buildingType))
            {
                slot.HideSlot();
                continue;
            }

            if (tileObject.IsNaturalResourceBuildable(slot.buildingSO.buildingType))
            {
                slot.ShowSlot();
                showCount++;
            }
            else
            {
                slot.HideSlot();
            }
        }
        SetGridFixedColumn(showCount);
    }
    private void ShowGroundBuilding(TileObject tileObject)
    {
        int showCount = 0;
        foreach (BuildingSlot slot in slots)
        {
            if (slot.IsDestroyable || tileObject.IsResourceBuilding(slot.buildingSO.buildingType))
            {
                slot.HideSlot();
                continue;
            }

            if (tileObject.IsGroundBuildable(slot.buildingSO.buildingType))
            {
                slot.ShowSlot();
                showCount++;
            }
            else
            {
                slot.HideSlot();
            }
        }
        SetGridFixedColumn(showCount);
    }
    private void ShowDestroySlot()
    {
        foreach (BuildingSlot slot in slots)
        {
            if (slot.IsDestroyable)
            {
                slot.ShowSlot();
                slot.ShowResourceSlot(tileObject.building.BuildingSO, true);
            }
            else
            {
                slot.HideSlot();
            }
        }
        SetGridFixedColumn(1);
    }

    private void SetGridFixedColumn(int showSlotCount)
    {
        if (gridLayoutGroup == null)
            gridLayoutGroup = buidingSlots.GetComponent<GridLayoutGroup>();

        gridLayoutGroup.constraintCount = showSlotCount > 1 ? 2 : 1;
    }

    public void Build(BuildingSO buildSO)
    {
        if (GameManager.Instance.UseResources(buildSO.NeedResources) == false)
        {
            // 자원 부족
            Debug.Log("Not enough resources");
            return;
        }

        tileObject.Build(buildSO);
        tileObject = null;
        OnCloseButton();
    }

    public void BuildingDestroy()
    {
        if (tileObject?.building == null) return;
        GameManager.Instance.ReturnResources(tileObject.building.BuildingSO.NeedResources, true);
        tileObject.building.Destroy();
        tileObject.ReturnNaturalObject();
        OnCloseButton();
    }

    public override void OnCloseButton()
    {
        base.OnCloseButton();
    }

}