using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SO_ItemList", menuName = "Datas/SO_ItemList", order = 1)]
public class ItemListSO : ScriptableObject
{
    public List<ItemSO> Consumables;
    public List<ItemSO> Helmets;
    public List<ItemSO> Weapons;
    public List<ItemSO> Resources;
}
