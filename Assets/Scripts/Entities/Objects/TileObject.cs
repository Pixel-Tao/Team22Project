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
        StartCoroutine(FadeFlashCoroutine());
    }

    IEnumerator FadeFlashCoroutine()
    {
        for (int i = 0; i < meshRenderers.Length; i++)
        {
            meshRenderers[i].material.color = flashColor;
        }
        yield return waitFlashInterval;
        for (int i = 0; i < meshRenderers.Length; i++)
        {
            meshRenderers[i].material.color = originColors[i];
        }
        isFlashing = false;
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
        if (GameManager.Instance.IsBuilding) return;

        Debug.Log(GetInteractPrompt());
        BuildPopupUI popup = UIManager.Instance.ShowPopupUI<BuildPopupUI>();
        popup.SelectedTile(this);
        GameManager.Instance.ToggleBuilding();
    }
}
