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
    public List<ConsumableData> consumables;
    public GameObject equipPrefab;
    public GameObject dropItemPrefab;
    public string childPath;
}
