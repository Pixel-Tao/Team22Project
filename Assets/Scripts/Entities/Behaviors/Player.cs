using UnityEngine;

public class Player : MonoBehaviour
{
    private MovementController movement;
    private LookController look;
    private InputController input;
    private CameraController cameraController;

    private Interaction interaction;

    private InventoryPopupUI inventory;

    private void Awake()
    {
        CharacterManager.Instance.SetPlayer(this);
        movement = GetComponent<MovementController>();
        look = GetComponent<LookController>();
        input = FindObjectOfType<InputController>();
        cameraController = GetComponent<CameraController>();
        interaction = GetComponent<Interaction>();
    }

    void Start()
    {
        SetEvent();
    }

    private void SetEvent()
    {
        input.MoveEvent += movement.Move;
        input.LookEvent += look.Look;
        input.MouseInteractionEvent += interaction.MouseInteraction;
        input.InteractEvent += interaction.Interact;
    }

    public void SetInventory(InventoryPopupUI inventory)
    {
        this.inventory = inventory;
    }

    public void AddItem(ItemSO itemSO)
    {
        inventory.AddItem(itemSO);
    }

    public void BuildMode()
    {
        // TODO : 건설모드일때 카메라 시점 변경
        Debug.Log("건설모드로 변경");
    }

    public void NormalMode()
    {
        // TODO : 건설모드가 아닐때 카메라 시점 변경
        Debug.Log("일반모드로 변경");
    }
}
