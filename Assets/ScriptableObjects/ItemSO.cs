using Defines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "S0_ItemData", menuName = "Datas/S0_ItemData")]
public class ItemSO : InteractableSO
{
    public int itemValue;
    public Sprite itemIcon;
    public ItemType itemType;
    
    [Header("중첩")]
    public bool isStackable;
    public int stackSize;

    [Header("장비")]
    public EquipType equipType;
    public GameObject equipPrefab;
    public CharacterAnimCombatLayerType combatMotionType;

    [Header("소비")]
    public List<ConsumableData> consumables;
    public GameObject dropItemPrefab;
    public string childPath;

    
}
