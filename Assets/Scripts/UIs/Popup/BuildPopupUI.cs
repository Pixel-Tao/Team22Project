using Defines;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildPopupUI : UIPopup
{
    private List<BuildingSlot> slots = new List<BuildingSlot>();
    [SerializeField] private Transform buidingSlots;

    private TileObject tileObject;

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

        if (tileObject?.data == null)
            return;

        TileSO tileSO = tileObject.data as TileSO;

        foreach (BuildingSlot slot in slots)
        {
            BuildingSO buildingSO =  slot.buildingSO;
            if (buildingSO == null)
            {
                slot.HideSlot();
                continue;
            }

            if (slot.buildingSO == null)
            {
                if (slot.IsDestroyable && tileObject.IsDestroyable())
                {
                    // 건물이 올라가있는 곳임
                    slot.ShowSlot();
                    slot.ShowResourceSlot(tileObject.building.BuildingSO, true);
                    continue;
                }
            }
            else if (tileObject.IsBuildableByBuilding(buildingSO.buildingType))
            {
                // 빈 땅임
                slot.ShowSlot();
                continue;
            }
            else if (tileObject.IsNaturalResourceBuildable(buildingSO.buildingType))
            {
                // 자연물 위에 건설 가능한 땅임
                slot.ShowSlot();
                continue;
            }

            slot.HideSlot();
        }
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
        OnCloseButton();
    }

    public override void OnCloseButton()
    {
        base.OnCloseButton();
        GameManager.Instance.ToggleBuilding();
    }

}