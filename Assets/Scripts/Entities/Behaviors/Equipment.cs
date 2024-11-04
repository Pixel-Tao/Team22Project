using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

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

    void Start()
    {
        //rightHandSlot = transform.Find("Rig/root/hips/spine/chest/upperarm.r/lowerarm.r/wrist.r/hand.r/handslot.r");
        //head = transform.Find("Rig/root/hips/spine/chest/head");

        ////Test
        //startWeapon = "Prefabs/Item/Weapon/Equip_Axe";
        //startHelmet = "Prefabs/Item/Helmet/Equip_Barbarian_Hat";
        //EquipWeapon(SetitemSO(startWeapon));
        //EquipHelmet(SetitemSO(startHelmet));
    }

    public void EquipWeaponOld(ItemSO itemSO)
    {
        Transform weapon = itemSO.equipPrefab.transform.Find(itemSO.childPath);
        GameObject weaponInstance = Instantiate(weapon.gameObject, rightHandSlot.position, rightHandSlot.rotation);
        weaponInstance.transform.SetParent(rightHandSlot);
        //weaponInstance.transform.localPosition = Vector3.zero;
        //weaponInstance.transform.localRotation = Quaternion.identity;
    }

    public void EquipWeapon(ItemSO itemSO)
    {
        UnEquipWeapon();
        this.itemSO = itemSO;
        if (characterModel == null)
            characterModel = characterModelGameObject.GetComponent<CharacterModel>();

        if (characterAnimController == null)
            characterAnimController = GetComponent<CharacterAnimController>();

        GameObject weapon = Instantiate(itemSO.equipPrefab);
        characterModel.InsertRightHandSlot(weapon.transform);
        characterAnimController.UseCombatLayer(itemSO.combatMotionType);
    }

    public void UnEquipWeapon()
    {
        // 무기 장비 해제
        if (characterModel == null)
            characterModel = characterModelGameObject.GetComponent<CharacterModel>();

        if (characterAnimController == null)
            characterAnimController = GetComponent<CharacterAnimController>();

        characterModel.ClearRightHandSlot();
        characterAnimController.ResetLayerWeight();
    }

    public void EquipHelmetOld(ItemSO itemSO)
    {
        Transform Helmet = itemSO.equipPrefab.transform.Find(itemSO.childPath);
        GameObject helmetInstance = Instantiate(Helmet.gameObject, head.position, head.rotation);
        helmetInstance.transform.SetParent(head);
        //helmetInstance.transform.localPosition = Vector3.zero;
        //helmetInstance.transform.localRotation = Quaternion.identity;
    }

    public void EquipHelmet(ItemSO itemSO)
    {

    }
    public void UnEquipHelmet()
    {
        // 헬멧 장비 해제
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
