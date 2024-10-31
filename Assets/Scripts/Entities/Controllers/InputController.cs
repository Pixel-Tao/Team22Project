using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    public event Action<Vector3> MoveEvent;
    public event Action<Vector3> LookEvent;
    public event Action<Vector2> RotateEvent;

    private Vector2 screenCenter;
    private Vector3 direction;
    private bool isRotating;

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
        if (isRotating) return;
        if (context.phase == InputActionPhase.Performed)
        {
            Vector2 position = context.ReadValue<Vector2>();
            Vector2 directionFromCenter = position - screenCenter;
            direction = new Vector3(directionFromCenter.x, 0f, directionFromCenter.y).normalized;
            LookEvent?.Invoke(direction);
        }
    }

    public void OnRotate(InputAction.CallbackContext context)
    {
        if (!isRotating) return;
        if (context.phase == InputActionPhase.Performed)
        {
            Vector2 delta = context.ReadValue<Vector2>();
            RotateEvent?.Invoke(delta);
        }
    }

    public void OnMouseLeftClick(InputAction.CallbackContext context)
    {

    }

    public void OnMouseRightClick(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            isRotating = true;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            isRotating = false;
            Cursor.lockState = CursorLockMode.None;
            RotateEvent?.Invoke(Vector3.zero);
        }
    }
}
