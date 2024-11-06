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
    public JobSO helmetJob;
    public bool LeftHand;

    [Header("무기정보(speed는 -면 속도 빨라짐)")]
    public float attack;
    public float attackSpeed;
    public float attackScale;

    [Header("소비")]
    public List<ConsumableData> consumables;
    public GameObject dropItemPrefab;
    public string childPath;

    [Header("리소스")]
    public ResourceType resourceType;
}
