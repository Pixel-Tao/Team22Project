using UnityEngine;

public class Player : MonoBehaviour
{
    private MovementController movement;
    private LookController look;
    private InputController input;
    private CameraController cameraController;

    private void Awake()
    {
        movement = GetComponent<MovementController>();
        look = GetComponent<LookController>();
        input = FindObjectOfType<InputController>();
        cameraController = GetComponent<CameraController>();
    }

    void Start()
    {
        SetEvent();
    }

    private void SetEvent()
    {
        input.MoveEvent += movement.Move;
        input.LookEvent += look.Look;
    }
}
