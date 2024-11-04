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
            if (!Utils.IsGroundBuildabe(buildingType)) continue;
            if (Utils.IsNaturalResource(buildingType)) continue;
            BuildSO buildSO = ResourceManager.Instance.GetSOBuildingData<BuildSO>(buildingType);
            if (buildSO == null)
            {
                Debug.LogWarning($"Failed to load building : {buildingType}");
                continue;
            }

            GameObject go = ResourceManager.Instance.Instantiate("UI/Slot/BuildingSlot", buidingSlots);
            BuildingSlot slot = go.GetComponent<BuildingSlot>();
            slot.SetData(buildSO);
            slots.Add(slot);
        }
    }

    public void SelectedTile(TileObject tileObject)
    {
        this.tileObject = tileObject;

        if (tileObject?.data == null)
            return;

        TileSO tileSO = tileObject.data as TileSO;

        foreach (BuildingSlot slot in slots)
        {
            if (tileSO.tileType == Defines.TileType.Ground
                && tileObject.building == null
                && Utils.IsGroundBuildabe(slot.buildSO.buildingType))
            {
                // 빈땅
                slot.gameObject.SetActive(true);
                continue;
            }
            else if (tileSO.tileType == Defines.TileType.Ground
                && tileObject.building != null
                && Utils.IsResourceBuildable(slot.buildSO.buildingType, tileObject.building.buildedSO.buildingType))
            {
                // 무언가 있음
                slot.gameObject.SetActive(true);
                continue;
            }

            slot.gameObject.SetActive(false);
        }
    }

    public void Build(BuildSO buildSO)
    {
        

        GameObject prefab = ResourceManager.Instance.Load<GameObject>(Utils.BuildingEnumToPrefabPath(buildSO.buildingType));
        if (prefab == null)
        {
            Debug.Log("Prefab is null");
            return;
        }
        GameObject go = Instantiate(prefab, tileObject.transform.position, Quaternion.identity);
        BuildingObject buildingObj = go.GetComponent<BuildingObject>();
        buildingObj.buildedSO = buildSO;

        if (tileObject.building != null)
        {
            PoolManager.Instance.Despawn(tileObject.building.gameObject);
            tileObject.building = null;
        }

        tileObject.building = buildingObj;
        
        OnCloseButton();
    }

    public override void OnCloseButton()
    {
        base.OnCloseButton();
        GameManager.Instance.ToggleBuilding();
    }

}