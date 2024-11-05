using Defines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileObject : InteractableObject, IInteractable
{
    public BuildingObject building;
    public BuildingObject naturalBuilding { get; private set; }

    [SerializeField] private float flashInterval = 0.5f;
    [SerializeField] private Color flashColor;

    private MeshRenderer[] meshRenderers;
    private List<Color> originColors;
    private bool isFlashing;
    private WaitForSeconds waitFlashInterval;

    [SerializeField] private bool isPlayerOnTile;
    public bool IsPlayerOnTile => isPlayerOnTile;
    public TileSO TileSO => data as TileSO;

    private void Start()
    {
        waitFlashInterval = new WaitForSeconds(flashInterval);
        meshRenderers = new MeshRenderer[transform.childCount];
        originColors = new List<Color>();
        meshRenderers = transform.GetComponentsInChildren<MeshRenderer>();
        for (int i = 0; i < meshRenderers.Length; i++)
        {
            originColors.Add(meshRenderers[i].material.color);
        }
        if (building != null && building.buildedSO.buildType == BuildType.NaturalObject)
        {
            naturalBuilding = building;
        }
    }

    public void Flash()
    {
        if (isFlashing) return;
        isFlashing = true;
        for (int i = 0; i < meshRenderers.Length; i++)
        {
            meshRenderers[i].material.color = flashColor;
        }
    }

    public void UnFlash()
    {
        if (!isFlashing) return;
        isFlashing = false;
        for (int i = 0; i < meshRenderers.Length; i++)
        {
            meshRenderers[i].material.color = originColors[i];
        }
    }

    public string GetInteractPrompt()
    {
        if (building != null)
        {
            return $"{building.buildedSO.displayName}\n{building.buildedSO.description}";
        }
        else
        {
            return $"{data.displayName}\n{data.description}";
        }
    }

    public void OnInteract(Transform target = null)
    {
        // TODO : 건물 건설 UI 띄우기
        if (!GameManager.Instance.IsBuildMode) return;
        if (GameManager.Instance.IsBuilding) return;
        GameManager.Instance.ToggleBuilding();

        Debug.Log(GetInteractPrompt());
        BuildPopupUI popup = UIManager.Instance.ShowPopupUI<BuildPopupUI>();
        popup.SelectedTile(this);
    }

    public void OnBuildingRotate()
    {
        if (building == null) return;

        building.transform.Rotate(Vector3.up, 60f);
    }

    public void BuildingClear()
    {
        building = null;
    }
    public bool Build(BuildingSO buildingSO)
    {
        if (naturalBuilding != null)
        {
            // 자연물에 건설하는거면 자연물 숨김
            naturalBuilding.gameObject.SetActive(false);
        }

        GameObject go = PoolManager.Instance.SpawnBuilding(buildingSO.buildingType);
        go.transform.position = transform.position;
        BuildingObject buildingObj = go.GetComponent<BuildingObject>();
        buildingObj.buildedSO = buildingSO;
        buildingObj.SetTile(this);
        return true;
    }

    public void PlayerOnTile(bool isOn)
    {
        isPlayerOnTile = isOn;
    }

    /// <summary>
    /// 건설 가능한 땅이면서 건물이 없으면 true 아니면 false
    /// </summary>
    /// <returns></returns>
    public bool IsGround()
    {
        return TileSO?.tileType == TileType.Ground && building == null;
    }

    /// <summary>
    /// 자원 타일인지
    /// </summary>
    /// <returns></returns>
    public bool IsNaturalObject()
    {
        return naturalBuilding != null && naturalBuilding.buildedSO.buildType == BuildType.NaturalObject;
    }

    /// <summary>
    /// 자원 타일이면 자원 복원
    /// </summary>
    public void ReturnNaturalObject()
    {
        if (!IsNaturalObject()) return;

        naturalBuilding.gameObject.SetActive(true);
        naturalBuilding.SetTile(this);
    }

    /// <summary>
    /// 건설하려는 건물 타입이 자연물에 건설 할 수 있는 건물인지
    /// </summary>
    /// <param name="resourceBuildingType"></param>
    /// <returns></returns>
    public bool IsNaturalResourceBuildable(BuildingType wantBuildingType)
    {
        if (naturalBuilding == null) return false;

        if (naturalBuilding.buildedSO.buildType == BuildType.NaturalObject)
        {
            BuildNaturalObjectSO so = naturalBuilding.buildedSO as BuildNaturalObjectSO;
            return CompareResourceBuildType(wantBuildingType, so.naturalObjectType);
        }
        return false;
    }

    public bool IsGroundBuildable(BuildingType wantBuildingType)
    {
        return !IsNaturalObject() && !IsNaturalResourceBuildable(wantBuildingType);
    }

    public bool CompareResourceBuildType(BuildingType buildingType, NaturalObjectType noType)
    {
        switch (noType)
        {
            case Defines.NaturalObjectType.GrainLand:
                return Defines.BuildingType.Windmill_Red == buildingType;
            case Defines.NaturalObjectType.LoggingArea_A:
            case Defines.NaturalObjectType.LoggingArea_B:
                return Defines.BuildingType.Lumbermill_Red == buildingType;
            case Defines.NaturalObjectType.MiningArea_A:
            case Defines.NaturalObjectType.MiningArea_B:
            case Defines.NaturalObjectType.MiningArea_C:
                return Defines.BuildingType.Quarry_Red == buildingType;
            case Defines.NaturalObjectType.Well:
                return Defines.BuildingType.Watermill_Red == buildingType;
        }

        return false;
    }

    public bool IsResourceBuilding(BuildingType buildingType)
    {
        switch (buildingType)
        {
            case BuildingType.Windmill_Red:
            case BuildingType.Lumbermill_Red:
            case BuildingType.Quarry_Red:
            case BuildingType.Market_Red:
            case BuildingType.Watermill_Red:
                return true;
        }

        return false;
    }

    public bool IsBuilded()
    {
        return building != null && building.BuildingSO?.buildType == BuildType.Building;
    }
}
