using Defines;
using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Player : MonoBehaviour
{
    private MovementController movement;
    private LookController look;
    private InputController input;
    private CameraController cameraController;
    private CombatController combat;

    private Interaction interaction;

    private InventoryPopupUI inventory;
    private Condition condition;

    [SerializeField] private List<JobSO> characters;
    private GameObject characterModelGameObject;

    public JobSO JobData { get; private set; }

    private void Awake()
    {
        CharacterManager.Instance.SetPlayer(this);
        movement = GetComponent<MovementController>();
        look = GetComponent<LookController>();
        input = FindObjectOfType<InputController>();
        cameraController = GetComponent<CameraController>();
        interaction = GetComponent<Interaction>();
        combat = GetComponent<CombatController>();
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
        input.AttackingEvent += combat.Attacking;
    }
    public void SetJob(JobType jobType)
    {
        if (characters == null) return;
        if (JobData?.jobType == jobType) return;

        if (characterModelGameObject != null)
        {
            Destroy(characterModelGameObject);
        }

        foreach (JobSO character in characters)
        {
            if (character.jobType == jobType)
            {
                JobData = character;
                characterModelGameObject = Instantiate(character.characterModelPrefab, transform);
                characterModelGameObject.name = character.characterModelPrefab.name;
                if (transform.TryGetComponent(out CharacterAnimController anim))
                {
                    anim.SetAnimator(characterModelGameObject.GetComponent<Animator>());
                }
                if (transform.TryGetComponent(out Condition condition))
                {
                    this.condition = condition;
                    condition.SetData(character.stat, true);
                }
                break;
            }
        }
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
        Debug.Log("건설모드로 변경");
        // TODO : 건설모드일때 추가로 작성해야할게 있다면 여기서
    }

    public void NormalMode()
    {
        Debug.Log("일반모드로 변경");
        // TODO : 일반모드일때 추가로 작성해야할게 있다면 여기서
    }


    #region 임시
    private CharacterModel characterModel;
    private CharacterAnimController characterAnimController;

    public ItemSO itemSO;

    public void EquipWeapon(ItemSO item)
    {
        UnEquipWeapon();
        itemSO = item;
        if (characterModel == null)
            characterModel = characterModelGameObject.GetComponent<CharacterModel>();

        if (characterAnimController == null)
            characterAnimController = GetComponent<CharacterAnimController>();

        GameObject weapon = Instantiate(item.equipPrefab);
        characterModel.InsertRightHandSlot(weapon.transform);
        characterAnimController.UseCombatLayer(item.combatMotionType);
    }

    public void UnEquipWeapon()
    {
        if (characterModel == null)
            characterModel = characterModelGameObject.GetComponent<CharacterModel>();

        if (characterAnimController == null)
            characterAnimController = GetComponent<CharacterAnimController>();

        characterModel.ClearRightHandSlot();
        characterAnimController.ResetLayerWeight();
    }
    #endregion
}
