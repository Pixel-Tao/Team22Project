
using Defines;
using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : Singleton<CharacterManager>
{
    private const string playerPrefabName = "Player";
    public const int maxItemSlotCount = 30;
    public const int maxQuickSlotCount = 4;

    public event Action<ItemSlotData> OnItemSlotDataChanged;
    public event Action<ItemSlotData> OnQuickSlotEquipped;
    public event Action<ItemSlotData> OnQuickSlotUnEquipped;

    private Player player;
    public Player Player { get { return player; } }

    private ItemSlotData[] itemSlotDatas;
    public int ItemSlotCount => itemSlotDatas?.Length ?? 0;
    private Dictionary<int, ItemSlotData> itemQuickSlots;
    public int QuickSlotCount => itemQuickSlots?.Count ?? 0;

    public void SetPlayer(Player player)
    {
        this.player = player;
    }

    public bool AddItem(ItemSO itemSO)
    {
        if (itemSO.itemType == Defines.ItemType.Resource)
        {
            // 자원 증가
            switch (itemSO.resourceType)
            {
                case ResourceType.Wood:
                    GameManager.Instance.AddWood(10);
                    return true;
                case ResourceType.Ore:
                    GameManager.Instance.AddOre(10);
                    return true;
                case ResourceType.Food:
                    GameManager.Instance.AddFood(10);
                    return true;
            }

            return false;
        }
        else
        {
            return AddItemSlotData(itemSO);
        }
    }

    public void LoadPlayer(JobType jobType)
    {
        // 플레이어 생성
        GameObject playerObj = ResourceManager.Instance.Instantiate(playerPrefabName);
        playerObj.name = "Player";
        Player player = Utils.GetOrAddComponent<Player>(playerObj);
        JobChange(JobType.None);
    }

    public void JobChange(JobType jobType)
    {
        JobSO jobSO = ResourceManager.Instance.GetSOJobData<JobSO>(jobType);
        Player.SetJob(jobSO);
    }

    public override void Init()
    {
        base.Init();
        InitItemSlotDatas();
        InitQuickSlotDatas();
    }

    private void InitItemSlotDatas()
    {
        itemSlotDatas = new ItemSlotData[maxItemSlotCount];
        for (int i = 0; i < maxItemSlotCount; i++)
        {
            itemSlotDatas[i] = new ItemSlotData();
            itemSlotDatas[i].itemSO = null;
            itemSlotDatas[i].itemCount = 0;
            itemSlotDatas[i].slotIndex = i;
            itemSlotDatas[i].quickSlotKey = -1;
            itemSlotDatas[i].isEquipped = false;
        }
    }
    private void InitQuickSlotDatas()
    {
        itemQuickSlots = new Dictionary<int, ItemSlotData>();
        for (int i = 0; i < maxQuickSlotCount; i++)
        {
            itemQuickSlots.Add(i + 1, null);
        }
    }

    public bool AddItemSlotData(ItemSO item)
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

        if (itemSlotData == null)
        {
            Debug.LogWarning("ItemSlot is Full");
            return false;
        }

        itemSlotData.itemSO = item;
        itemSlotData.itemCount += 1;
        OnItemSlotDataChanged?.Invoke(itemSlotData);
        return true;
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
            if (itemSlotDatas[index].isEquipped)
                UnEquipItem(index);
            itemQuickSlots[itemSlotDatas[index].quickSlotKey] = null;
            itemSlotDatas[index].itemSO = null;
            itemSlotDatas[index].itemCount = 0;
            itemSlotDatas[index].quickSlotKey = -1;
            itemSlotDatas[index].isEquipped = false;
            OnQuickSlotUnEquipped?.Invoke(itemSlotDatas[index]);
        }

        OnItemSlotDataChanged?.Invoke(itemSlotDatas[index]);
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

        RemoveItemSlotData(index);
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
            OnItemSlotDataChanged?.Invoke(itemSlotData);
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
            OnItemSlotDataChanged?.Invoke(itemSlotData);
        }
    }
    public void EquipQuickSlotItem(int itemIndex, int quickSlotKey)
    {
        ItemSlotData itemSlotData = GetItemSlotData(itemIndex);
        if (itemSlotData == null) return;

        ItemSlotData quickSlotData = GetQuickSlotData(quickSlotKey);
        if (quickSlotData != null)
        {
            UnEquipQuickSlotItem(quickSlotKey);
        }

        itemSlotData.quickSlotKey = quickSlotKey;
        itemQuickSlots[quickSlotKey] = itemSlotData;
        OnQuickSlotEquipped?.Invoke(itemSlotData);
        OnItemSlotDataChanged?.Invoke(itemSlotData);
    }
    public void UnEquipQuickSlotItem(int quickSlotKey)
    {
        ItemSlotData quickSlotData = GetQuickSlotData(quickSlotKey);
        if (quickSlotData == null) return;

        quickSlotData.quickSlotKey = -1;
        itemQuickSlots[quickSlotKey] = null;
        OnQuickSlotUnEquipped?.Invoke(quickSlotData);
        OnItemSlotDataChanged?.Invoke(quickSlotData);
    }

    public void InputQuickSlotKey(int quickSlotKey)
    {
        ItemSlotData itemSlotData = GetQuickSlotData(quickSlotKey);
        if (itemSlotData?.itemSO == null) return;

        if (itemSlotData.itemSO.itemType == ItemType.Consumable)
        {
            UseItem(itemSlotData.slotIndex);
        }
        else if (itemSlotData.itemSO.itemType == ItemType.Equipable)
        {
            if (itemSlotData.isEquipped)
            {
                UnEquipItem(itemSlotData.slotIndex);
            }
            else
            {
                EquipItem(itemSlotData.slotIndex);
            }
        }
    }

    public bool IsEmptySlot(int index)
    {
        if (index < 0 || index >= itemSlotDatas.Length)
            return false;

        return itemSlotDatas[index].itemSO == null;
    }
    public ItemSlotData GetQuickSlotData(int quickSlotKey)
    {
        if (itemQuickSlots.TryGetValue(quickSlotKey, out ItemSlotData itemSlotData))
        {
            return itemSlotData;
        }

        return null;
    }
    public ItemSlotData GetEquippedItemData(EquipType type)
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
    public ItemSlotData GetStackableSlotData(ItemSO item)
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
    public ItemSlotData GetEmptySlotData()
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

