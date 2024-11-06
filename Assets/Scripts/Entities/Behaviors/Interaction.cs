using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{

    [Header("건설모드")]
    [SerializeField] private float buildInteractionDistance = 3f;
    [SerializeField] private LayerMask groundLayer;

    [Header("일반모드")]
    [SerializeField] private float normalInteractionRadius = 1f;
    [SerializeField] private LayerMask peakupLayer;

    [SerializeField] private bool isBuildMode;
    [SerializeField] private float checkInterval = 0.2f;
    [SerializeField] private float ItemAttractSpeed;
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
            ClearTile();
        }

        if (Time.time - time >= checkInterval)
        {
            CheckBuildable();
            CheckSurroundings();
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
                    if (GameManager.Instance.IsBuildMode)
                        currentTile.Flash();
                    else
                        currentTile.UnFlash();
                    return;
                }
            }
        }

        ClearTile();
    }

    private void CheckSurroundings()
    {
        if (GameManager.Instance.IsBuildMode) return;

        Collider[] colliders = Physics.OverlapSphere(transform.position, normalInteractionRadius, peakupLayer);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].TryGetComponent(out ItemObject itemObj))
            {
                itemObj.FollowSpeed = ItemAttractSpeed;
                itemObj.OnInteract(transform);
                return;
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
        if (currentTile != null && currentTile == tile)
        {
            string prompt = $"{tile?.GetInteractPrompt()}";
            UIManager.Instance.Prompt(prompt);
        }
        else if (tile == null)
        {
            ClearTile();
        }
        else
        {
            if (GameManager.Instance.IsBuildMode)
                currentTile?.UnFlash();
            currentTile = tile;

            string prompt = $"{tile?.GetInteractPrompt()}";
            UIManager.Instance.Prompt(prompt);
        }
    }

    private void ClearTile()
    {
        currentTile?.UnFlash();
        currentTile = null;
        UIManager.Instance.Prompt();
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
