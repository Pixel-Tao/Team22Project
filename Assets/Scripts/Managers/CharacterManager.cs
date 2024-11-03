
using Defines;
using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : Singleton<CharacterManager>
{
    private const string playerPrefabName = "Player";
    public const int maxItemSlotCount = 30;
    public const int maxQuickSlotCount = 2;

    public event Action<ItemSlotData> OnAddItemSlotData;
    public event Action<ItemSlotData> OnRemoveItemSlotData;
    public event Action<ItemSlotData> OnEquipItem;
    public event Action<ItemSlotData> OnUnEquipItem;
    public event Action<ItemSlotData> OnEquipQuickSlotItem;
    public event Action<ItemSlotData> OnUnEquipQuickSlotItem;

    private Player player;
    public Player Player { get { return player; } }

    private ItemSlotData[] itemSlotDatas;
    public int ItemSlotCount => itemSlotDatas?.Length ?? 0;
    private ItemSlotData[] quickSlotDatas;
    public int QuickSlotCount => quickSlotDatas?.Length ?? 0;

    public void SetPlayer(Player player)
    {
        this.player = player;
    }

    public void AddItem(ItemSO itemSO)
    {
        if (itemSO.itemType == Defines.ItemType.Resource)
        {
            // 자원 증가
        }
        else
        {
            AddItemSlotData(itemSO);
        }
    }

    public void LoadPlayer(JobType jobType)
    {
        // 플레이어 생성
        GameObject playerObj = ResourceManager.Instance.Instantiate(playerPrefabName);
        playerObj.name = "Player";
        Player player = Utils.GetOrAddComponent<Player>(playerObj);
        JobChange(JobType.Babarian);
        InitItemSlotDatas();
    }

    public void JobChange(JobType jobType)
    {
        JobSO jobSO = ResourceManager.Instance.GetSOJobData<JobSO>(JobType.Babarian);
        Player.SetJob(jobSO);
    }

    public void InitItemSlotDatas()
    {
        itemSlotDatas = new ItemSlotData[maxItemSlotCount];
        for (int i = 0; i < maxItemSlotCount; i++)
        {
            itemSlotDatas[i] = new ItemSlotData();
            itemSlotDatas[i].itemSO = null;
            itemSlotDatas[i].itemCount = 0;
            itemSlotDatas[i].slotIndex = i;
            itemSlotDatas[i].quickSlotIndex = -1;
            itemSlotDatas[i].isEquipped = false;
        }
    }

    public void AddItemSlotData(ItemSO item)
    {
        ItemSlotData itemSlotData = null;
        if (item.isStackable)
        {
            itemSlotData = GetStackableSlotData(item);
        }

        if (itemSlotData == null)
        {
            itemSlotData = GetEmptySlotData();
        }
        itemSlotData.itemSO = item;
        itemSlotData.itemCount += 1;
        OnAddItemSlotData?.Invoke(itemSlotData);
    }
    public ItemSlotData GetItemSlotData(int index)
    {
        if (index < 0 || index >= itemSlotDatas.Length)
            return null;

        return itemSlotDatas[index];
    }
    public void RemoveItemSlotData(int index)
    {
        itemSlotDatas[index].itemCount -= 1;
        if (itemSlotDatas[index].itemCount <= 0)
        {
            itemSlotDatas[index].itemSO = null;
            itemSlotDatas[index].itemCount = 0;
            itemSlotDatas[index].quickSlotIndex = -1;
            itemSlotDatas[index].isEquipped = false;
        }

        OnRemoveItemSlotData?.Invoke(itemSlotDatas[index]);
    }

    public void UseItem(int index)
    {
        if (IsEmptySlot(index)) return;
        ItemSlotData itemSlotData = GetItemSlotData(index);
        if (itemSlotData.itemSO.itemType != ItemType.Consumable) return;

        Condition condition = Player.GetComponent<Condition>();
        if (condition == null) return;

        foreach (ConsumableData consumable in itemSlotData.itemSO.consumables)
        {
            switch (consumable.consumeableType)
            {
                case ConsumeableType.Health:
                    condition.Heal((int)consumable.amount);
                    break;
                case ConsumeableType.Stamina:
                    condition.StaminaRecovery(consumable.amount);
                    break;
                case ConsumeableType.Water:
                    condition.Drink(consumable.amount);
                    break;
                case ConsumeableType.Hunger:
                    condition.Eat(consumable.amount);
                    break;
            }
        }
    }
    public void EquipItem(int index)
    {
        ItemSlotData itemSlotData = GetItemSlotData(index);
        if (itemSlotData == null) return;

        ItemSlotData equippedItemSlotData = GetEquippedItemData(itemSlotData.itemSO.equipType);
        if (equippedItemSlotData != null)
            UnEquipItem(equippedItemSlotData.slotIndex);

        if (!itemSlotData.isEquipped)
        {
            Player.Equip(itemSlotData.itemSO);
            itemSlotData.isEquipped = true;
            OnEquipItem?.Invoke(itemSlotData);
        }
    }
    public void UnEquipItem(int index)
    {
        ItemSlotData itemSlotData = GetItemSlotData(index);
        if (itemSlotData == null) return;
        if (itemSlotData.isEquipped)
        {
            Player.UnEquip(itemSlotData.itemSO.equipType);
            itemSlotData.isEquipped = false;
            OnUnEquipItem?.Invoke(itemSlotData);
        }
    }
    public void EquipQuickSlotItem(int itemIndex, int quickSlotIndex)
    {
        ItemSlotData itemSlotData = GetItemSlotData(itemIndex);
        if (itemSlotData == null) return;

        ItemSlotData quickSlotData = GetQuickSlotData(quickSlotIndex);
        if (quickSlotData != null)
        {
            UnEquipQuickSlotItem(quickSlotIndex);
        }

        itemSlotData.quickSlotIndex = quickSlotIndex;
        OnEquipQuickSlotItem?.Invoke(itemSlotData);
    }
    public void UnEquipQuickSlotItem(int quickSlotIndex)
    {
        ItemSlotData quickSlotData = GetQuickSlotData(quickSlotIndex);
        if (quickSlotData == null) return;

        quickSlotData.quickSlotIndex = -1;
        OnUnEquipQuickSlotItem?.Invoke(quickSlotData);
    }

    private bool IsEmptySlot(int index)
    {
        if (index < 0 || index >= itemSlotDatas.Length)
            return false;

        return itemSlotDatas[index].itemSO == null;
    }
    private ItemSlotData GetQuickSlotData(int quickSlotIndex)
    {
        for (int i = 0; i < itemSlotDatas.Length; i++)
        {
            if (itemSlotDatas[i].quickSlotIndex == quickSlotIndex)
            {
                return itemSlotDatas[i];
            }
        }
        return null;
    }
    private ItemSlotData GetEquippedItemData(EquipType type)
    {
        foreach (ItemSlotData itemSlotData in itemSlotDatas)
        {
            if (itemSlotData.itemSO != null
                && itemSlotData.itemSO.equipType == type
                && itemSlotData.isEquipped)
            {
                return itemSlotData;
            }
        }

        return null;
    }
    private ItemSlotData GetStackableSlotData(ItemSO item)
    {
        for (int i = 0; i < itemSlotDatas.Length; i++)
        {
            if (itemSlotDatas[i].itemSO == item)
            {
                if (itemSlotDatas[i].itemCount < item.stackSize)
                    return itemSlotDatas[i];
            }
        }
        return null;
    }
    private ItemSlotData GetEmptySlotData()
    {
        for (int i = 0; i < itemSlotDatas.Length; i++)
        {
            if (itemSlotDatas[i].itemSO == null)
            {
                return itemSlotDatas[i];
            }
        }
        return null;
    }
}

