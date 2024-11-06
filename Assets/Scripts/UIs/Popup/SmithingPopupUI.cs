using Defines;
using System.Collections.Generic;
using UnityEngine;

public class SmithingPopupUI : UIPopup
{
    [SerializeField] private NeedResourceSlot woodSlot;
    [SerializeField] private NeedResourceSlot oreSlot;
    [SerializeField] private NeedResourceSlot foodSlot;

    private void Start()
    {
        foreach (ResourceData resource in GameManager.Instance.ItemList.NeedResources)
        {
            switch (resource.needResourceType)
            {
                case ResourceType.Wood:
                    woodSlot.SetData(resource, false);
                    continue;
                case ResourceType.Ore:
                    oreSlot.SetData(resource, false);
                    continue;
                case ResourceType.Food:
                    foodSlot.SetData(resource, false);
                    continue;
            }
        }
    }

    public void CreateWeaponButton()
    {
        CreateButtonHelper(EquipType.Weapon);
    }

    public void CreateArmorButton()
    {
        CreateButtonHelper(EquipType.Helmet);
    }

    public void CreateButtonHelper(EquipType type)
    {
        if (!GameManager.Instance.UseResources(GameManager.Instance.ItemList.NeedResources))
        {
            UIManager.Instance.Prompt($"소지 자원이 부족합니다.");
            return;
        }

        ItemSO item = GetRandomEquip(type);

        if (CharacterManager.Instance.AddItemSlotData(item))
        {
            UIManager.Instance.Prompt($"{item.displayName} 장비를 생성했습니다.");
        }
        else
        {
            UIManager.Instance.Prompt($"인벤토리가 가득 찼습니다.");
        }
    }

    public ItemSO GetRandomEquip(Defines.EquipType type)
    {
        if (type == EquipType.None) return null;
        var itemList = GameManager.Instance.ItemList;
        List<ItemSO> temp = type == EquipType.Weapon ?
            itemList.Weapons : itemList.Helmets;

        return temp[UnityEngine.Random.Range(0, temp.Count)];
    }
}