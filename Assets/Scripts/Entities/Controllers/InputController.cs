using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    public event Action<Vector3> MoveEvent;
    public event Action<Vector2> LookEvent;
    public event Action<bool> MouseRightPressedEvent;
    public event Action<Vector2> CameraRotateEvent;
    public event Action<float> CameraZoomEvent;
    public event Action<bool> BuildModeEvent;
    public event Action<Vector2> MouseInteractionEvent;
    public event Action InteractEvent;
    public event Action BuildingRotateEvent;
    public event Action<bool> AttackingEvent;

    public event Action<Vector2> MouseMoveEvent;

    private Vector2 screenCenter;
    private Vector3 direction;

    private void Awake()
    {
       
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            Vector2 move = context.ReadValue<Vector2>();
            MoveEvent?.Invoke(new Vector3(move.x, 0, move.y));
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
            LookEvent?.Invoke(position);
        }
    }

    public void OnRotate(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            Vector2 position = context.ReadValue<Vector2>();
            CameraRotateEvent?.Invoke(position);
        }
    }

    public void OnCameraZoom(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            float position = context.ReadValue<float>();
            // 휠 위로 올리면 1, 아래로 내리면 -1
            CameraZoomEvent?.Invoke(position);
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
            else
            {
                MouseRightPressedEvent?.Invoke(true);
            }
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            MouseRightPressedEvent?.Invoke(false);
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
            BuildModeEvent?.Invoke(GameManager.Instance.IsBuildMode);
            if (GameManager.Instance.IsBuildMode)
            {
                MouseRightPressedEvent?.Invoke(false);
            }
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
