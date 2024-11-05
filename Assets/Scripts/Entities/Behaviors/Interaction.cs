using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public event Action<string> PromptChangedEvent;

    [Header("건설모드")]
    [SerializeField] private float buildInteractionDistance = 3f;
    [SerializeField] private LayerMask groundLayer;

    [Header("일반모드")]
    [SerializeField] private float normalInteractionRadius = 1f;
    [SerializeField] private LayerMask peakupLayer;

    [SerializeField] private bool isBuildMode;
    [SerializeField] private float checkInterval = 0.2f;
    private Camera cam;
    private float time;
    public Vector2 screenAxis;

    [SerializeField] private TileObject currentTile;


    private void Awake()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if (isBuildMode != GameManager.Instance.IsBuildMode)
        {
            isBuildMode = GameManager.Instance.IsBuildMode;
        }

        if (Time.time - time >= checkInterval)
        {
            if (GameManager.Instance.IsBuildMode)
                CheckBuildable();
            else
            {
                ClearTile();
                CheckSurroundings();
            }
            time = Time.time;
        }
    }

    private void CheckBuildable()
    {
        Ray ray = cam.ScreenPointToRay(screenAxis);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, groundLayer))
        {
            if (hit.collider?.transform?.parent == null)
            {
                ClearTile();
                return;
            }

            if (hit.collider.transform.parent.TryGetComponent(out TileObject tileObj))
            {
                float distance = Vector3.Distance(tileObj.transform.position, transform.position);
                if (tileObj.IsPlayerOnTile || distance > buildInteractionDistance)
                {
                    ClearTile();
                    return;
                }

                TileSO tileSO = tileObj.data as TileSO;
                if (tileSO?.tileType == Defines.TileType.Ground)
                {
                    SetTile(tileObj);
                    currentTile.Flash();
                    return;
                }
            }
        }

        ClearTile();
    }

    private void CheckSurroundings()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, normalInteractionRadius, peakupLayer);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (!GameManager.Instance.IsBuildMode && colliders[i].TryGetComponent(out TileObject tileObj))
            {
                SetTile(tileObj);
            }
            else if (colliders[i].TryGetComponent(out ItemObject itemObj))
            {
                itemObj.OnInteract(transform);
            }
        }
    }

    public void MouseInteraction(Vector2 position)
    {
        screenAxis = position;
    }

    public void Interact()
    {
        currentTile?.OnInteract(transform);
    }

    private void SetTile(TileObject tile)
    {
        if (currentTile == tile) return;

        ClearTile();
        if (tile == null) return;
        currentTile = tile;
        PromptChangedEvent?.Invoke(currentTile.GetInteractPrompt());
    }

    private void ClearTile()
    {
        currentTile?.UnFlash();
        currentTile = null;
        PromptChangedEvent?.Invoke(string.Empty);
    }

    public void BuildingRotate()
    {
        currentTile?.OnBuildingRotate();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (isBuildMode)
            Gizmos.DrawWireSphere(transform.position, buildInteractionDistance);
        else
            Gizmos.DrawWireSphere(transform.position, normalInteractionRadius);
    }
}
