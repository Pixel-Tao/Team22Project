using Defines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "S0_ItemData", menuName = "Datas/S0_ItemData")]
public class ItemSO : InteractableSO
{
    public ItemType itemType;

    public List<ConsumableData> consumables;
}
