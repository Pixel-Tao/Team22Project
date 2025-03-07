using UnityEngine;

public class Equipment : MonoBehaviour
{
    ItemSO itemSO;
    GameObject equipprefap;
    Transform rightHandSlot;
    Transform head;

    private CharacterModel characterModel;
    private CharacterAnimController characterAnimController;
    private GameObject characterModelGameObject;

    public string startWeapon;
    public string startHelmet;

    public ItemSO EquipWeaponDate { get; private set; }
    public ItemSO EquipHelmetDate { get; private set; }

    public void EquipWeaponOld(ItemSO itemSO)
    {
        Transform weapon = itemSO.equipPrefab.transform.Find(itemSO.childPath);
        GameObject weaponInstance = Instantiate(weapon.gameObject, rightHandSlot.position, rightHandSlot.rotation);
        weaponInstance.transform.SetParent(rightHandSlot);
    }

    public void EquipWeapon(ItemSO itemSO)
    {
        UnEquipWeapon();
        EquipWeaponDate = itemSO;
        this.itemSO = itemSO;
        if (characterModel == null)
            characterModel = characterModelGameObject.GetComponent<CharacterModel>();

        if (characterAnimController == null)
            characterAnimController = GetComponent<CharacterAnimController>();

        GameObject weapon = Instantiate(itemSO.equipPrefab);
        Collider weaponCollider = weapon.GetComponent<Collider>();
        weaponCollider.enabled = false;
        if (!itemSO.LeftHand)
        {
            characterModel.InsertRightHandSlot(weapon);
        }
        else
        {
            characterModel.InsertLeftHandSlot(weapon);
        }
        
        characterAnimController.UseCombatLayer(itemSO.combatMotionType);
    }

    public void UnEquipWeapon()
    {
        // 무기 장비 해제
        if (characterModel == null)
            characterModel = characterModelGameObject.GetComponent<CharacterModel>();

        if (characterAnimController == null)
            characterAnimController = GetComponent<CharacterAnimController>();

        EquipWeaponDate = null;
        characterModel.ClearLeftHandSlot();
        characterModel.ClearRightHandSlot();
        characterAnimController.ResetLayerWeight();
    }

    public void EquipHelmetOld(ItemSO itemSO)
    {
        Transform Helmet = itemSO.equipPrefab.transform.Find(itemSO.childPath);
        GameObject helmetInstance = Instantiate(Helmet.gameObject, head.position, head.rotation);
        helmetInstance.transform.SetParent(head);
    }

    public void EquipHelmet(ItemSO itemSO)
    {
        UnEquipHelmet();
        this.itemSO = itemSO;
        
        if (characterModel == null)
            characterModel = characterModelGameObject.GetComponent<CharacterModel>();

        if (characterAnimController == null)
            characterAnimController = GetComponent<CharacterAnimController>();

        CharacterManager.Instance.JobChange(itemSO.helmetJob.jobType);
        characterModel = characterModelGameObject.GetComponent<CharacterModel>();

        if (EquipWeaponDate != null)
        {
            GameObject weapon = Instantiate(EquipWeaponDate.equipPrefab);

            if (!EquipWeaponDate.LeftHand)
            {
                characterModel.InsertRightHandSlot(weapon);
            }
            else
            {
                characterModel.InsertLeftHandSlot(weapon);
            }

            characterAnimController.UseCombatLayer(EquipWeaponDate.combatMotionType);
        }
    }
    public void UnEquipHelmet()
    {
        // 헬멧 장비 해제
        if (characterModel == null)
            characterModel = characterModelGameObject.GetComponent<CharacterModel>();

        CharacterManager.Instance.JobChange(Defines.JobType.None);
    }

    public void SetCharacterModel(GameObject characterModelGameObject)
    {
        this.characterModelGameObject = characterModelGameObject;
    }
    //Test
    ItemSO SetitemSO(string equip)
    {
        equipprefap = Resources.Load<GameObject>(equip);
        itemSO = equipprefap.GetComponent<ItemObject>()?.data as ItemSO;
        return itemSO;
    }
}
