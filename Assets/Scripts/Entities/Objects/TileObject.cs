using Defines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileObject : InteractableObject, IInteractable
{
    public BuildingObject building;

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
    public bool Build(BuildSO buildSO)
    {
        GameObject prefab = ResourceManager.Instance.Load<GameObject>(Utils.BuildingEnumToPrefabPath(buildSO.buildingType));
        if (prefab == null)
        {
            Debug.Log("Prefab is null");
            return false;
        }
        building?.Destroy();
        GameObject go = Instantiate(prefab, transform.position, Quaternion.identity);
        BuildingObject buildingObj = go.GetComponent<BuildingObject>();
        buildingObj.buildedSO = buildSO;
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
    public bool IsBuildable()
    {
        return TileSO?.tileType == TileType.Ground && building == null;
    }

    /// <summary>
    /// 건설 하려는 건물 타입이 건설 가능한 구역이면 true 아니면 false
    /// </summary>
    /// <param name="buildingType"></param>
    /// <returns></returns>
    public bool IsBuildableByBuilding(BuildingType buildingType)
    {
        return IsBuildable() && Utils.IsGroundBuildable(buildingType);
    }

    /// <summary>
    /// 건물이 있지만 건물이 자연물이 아니면 true 아니면 false
    /// </summary>
    /// <returns></returns>
    public bool IsDestroyable()
    {
        return building != null && !IsNaturalResource();
    }

    /// <summary>
    /// 자연물이면 true 아니면 false
    /// </summary>
    /// <returns></returns>
    public bool IsNaturalResource()
    {
        if (building == null) return false;
        return Utils.IsNaturalResource(building.buildedSO.buildingType);
    }

    /// <summary>
    /// 건설하려는 건물 타입이 자연물에 건설 할 수 있는 건물인지
    /// </summary>
    /// <param name="resourceBuildingType"></param>
    /// <returns></returns>
    public bool IsNaturalResourceBuildable(BuildingType wantBuildingType)
    {
        if (building == null) return false;
        return Utils.IsResourceBuildable(wantBuildingType, building.buildedSO.buildingType);
    }
}
