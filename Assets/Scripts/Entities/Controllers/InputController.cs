using Assets.Scripts.UIs.Popup;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;

public class InputController : MonoBehaviour
{
    public event Action<Vector3> MoveEvent;
    public event Action<Vector3> LookEvent;
    public event Action<Vector2> RotateEvent;
    public event Action<Vector2> MouseInteractionEvent;
    public event Action InteractEvent;
    public event Action BuildingRotateEvent;
    public event Action<bool> AttackingEvent;

    private Vector2 screenCenter;
    private Vector3 direction;

    private void Awake()
    {
        screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            Vector2 move = context.ReadValue<Vector2>();
            MoveEvent?.Invoke(new Vector3(move.x, 0, move.y));
            Debug.Log(move);
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            MoveEvent?.Invoke(Vector3.zero);
        }
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            if (GameManager.Instance.IsInteracting) return;

            Vector2 position = context.ReadValue<Vector2>();
            Vector2 directionFromCenter = position - screenCenter;
            direction = new Vector3(directionFromCenter.x, 0f, directionFromCenter.y).normalized;
            LookEvent?.Invoke(direction);
            MouseInteractionEvent?.Invoke(position);
        }
    }

    public void OnRotate(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            Vector2 delta = context.ReadValue<Vector2>();
            RotateEvent?.Invoke(delta);
        }
    }

    public void OnMouseLeftClick(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            if (GameManager.Instance.IsInteracting) return;
            if (GameManager.Instance.IsBuildMode)
            {
                InteractEvent?.Invoke();
            }
            else
            {
                AttackingEvent?.Invoke(true);
            }
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            AttackingEvent?.Invoke(false);
        }
    }

    public void OnMouseRightClick(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            if (GameManager.Instance.IsInteracting) return;
            if (GameManager.Instance.IsBuildMode)
            {
                BuildingRotateEvent?.Invoke();
            }
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            if (GameManager.Instance.IsInteracting) return;
            if (!GameManager.Instance.IsBuildMode)
                InteractEvent?.Invoke();
        }
    }

    public void OnInventory(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            InventoryPopupUI popup = UIManager.Instance.FindPopup<InventoryPopupUI>();
            if (popup == null)
                UIManager.Instance.ShowPopupUI<InventoryPopupUI>();
            else
                UIManager.Instance.ClosePopupUI(popup);
        }
    }

    public void OnBuildMode(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            GameManager.Instance.ToggleBuildMode();
        }
    }

    public void OnQuickSlot(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            if (int.TryParse(context.control.name, out int value))
            {
                CharacterManager.Instance.InputQuickSlotKey(value);
            }
        }
    }

    public void OnDebug(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            ConsolePopupUI popup = UIManager.Instance.FindPopup<ConsolePopupUI>();
            if (popup == null)
                UIManager.Instance.ShowPopupUI<ConsolePopupUI>();
            else
                UIManager.Instance.ClosePopupUI(popup);
        }
    }
}
