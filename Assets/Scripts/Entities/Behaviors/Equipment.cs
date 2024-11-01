using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    ItemSO itemSO;
    GameObject equipprefap;
    Transform rightHandSlot;
    Transform head;

    public string startWeapon;
    public string startHelmet;

    void Start()
    {
        rightHandSlot = transform.Find("Rig/root/hips/spine/chest/upperarm.r/lowerarm.r/wrist.r/hand.r/handslot.r");
        head = transform.Find("Rig/root/hips/spine/chest/head");

        //Test
        startWeapon = "Prefabs/Item/Weapon/Equip_Axe";
        startHelmet = "Prefabs/Item/Helmet/Equip_Barbarian_Hat";
        EquipWeapon(SetitemSO(startWeapon));
        EquipHelmet(SetitemSO(startHelmet));
    }

    public void EquipWeapon(ItemSO itemSO)
    {
        Transform weapon = itemSO.equipPrefab.transform.Find(itemSO.childPath);
        GameObject weaponInstance = Instantiate(weapon.gameObject, rightHandSlot.position, rightHandSlot.rotation);
        weaponInstance.transform.SetParent(rightHandSlot);
        //weaponInstance.transform.localPosition = Vector3.zero;
        //weaponInstance.transform.localRotation = Quaternion.identity;
    }

    public void EquipHelmet(ItemSO itemSO)
    {
        Transform Helmet = itemSO.equipPrefab.transform.Find(itemSO.childPath);
        GameObject helmetInstance = Instantiate(Helmet.gameObject, head.position, head.rotation);
        helmetInstance.transform.SetParent(head);
        //helmetInstance.transform.localPosition = Vector3.zero;
        //helmetInstance.transform.localRotation = Quaternion.identity;
    }

    //Test
    ItemSO SetitemSO(string equip)
    {
        equipprefap = Resources.Load<GameObject>(equip);
        itemSO = equipprefap.GetComponent<ItemObject>()?.data as ItemSO;
        return itemSO;
    }
}
