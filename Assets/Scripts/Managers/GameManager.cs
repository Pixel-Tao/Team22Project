using Defines;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public event Action<int> OnWoodCountChanged;
    public event Action<int> OnOreCountChanged;
    public event Action<int, int> OnPeopleCountChanged;
    public event Action<int> OnFoodCountChanged;

    public bool IsBuildMode { get; private set; } = false;
    public bool IsBuilding { get; private set; } = false;

    public Goal Goal { get; private set; }

    public int WoodCount { get; private set; }
    public int OreCount { get; private set; }
    public int FoodCount { get; private set; }
    public int PeopleCount { get; private set; }
    public int MaxPeopleCount { get; private set; }

    public ItemListSO ItemList { get; private set; }

    public void AddGoal(Goal goal)
    {
        this.Goal = goal;
    }
    public void ToggleBuildMode()
    {
        IsBuildMode = !IsBuildMode;
        if (IsBuildMode)
        {
            // 건설모드일때 카메라 시점 변경
            CharacterManager.Instance.Player.BuildMode();
            UIManager.Instance.PeekPopupUI<BuildPopupUI>()?.OnCloseButton();
        }
        else
        {
            // 건설모드가 아닐때 카메라 시점 변경
            CharacterManager.Instance.Player.NormalMode();
        }
    }
    public void ToggleBuilding()
    {
        IsBuilding = !IsBuilding;
    }
    public override void Init()
    {
        base.Init();

        AddWood(100);
        AddOre(100);
        AddFood(100);
        IsBuildMode = false;

        ItemList = ResourceManager.Instance.GetSOData<ItemListSO>("Item/SO_ItemList");
    }

    public void AddWood(int amount)
    {
        WoodCount += amount;
        OnWoodCountChanged?.Invoke(WoodCount);
    }
    public bool CanUseWood(int amount)
    {
        return WoodCount - amount >= 0;
    }
    public void SubtractWood(int amount)
    {
        WoodCount -= amount;
        if (WoodCount < 0)
            WoodCount = 0;
        OnWoodCountChanged?.Invoke(WoodCount);
    }

    public void AddOre(int amount)
    {
        OreCount += amount;
        OnOreCountChanged?.Invoke(OreCount);
    }
    public void SubtractOre(int amount)
    {
        OreCount -= amount;
        if (OreCount < 0)
            OreCount = 0;
        OnOreCountChanged?.Invoke(OreCount);
    }
    public bool CanUseOre(int amount)
    {
        return OreCount - amount >= 0;
    }

    public void AddFood(int amount)
    {
        FoodCount += amount;
        OnFoodCountChanged?.Invoke(FoodCount);
    }
    public void SubtractFood(int amount)
    {
        FoodCount -= amount;
        if (FoodCount < 0)
            FoodCount = 0;
        OnFoodCountChanged?.Invoke(FoodCount);
    }
    public bool CanUseFood(int amount)
    {
        return FoodCount - amount >= 0;
    }

    public void AddPeople(int amount)
    {
        PeopleCount += amount;
        if (PeopleCount > MaxPeopleCount)
            MaxPeopleCount = PeopleCount;

        OnPeopleCountChanged?.Invoke(PeopleCount, MaxPeopleCount);
    }
    public void SubtractPeople(int amount)
    {
        PeopleCount -= amount;
        if (PeopleCount < 0)
            PeopleCount = 0;
        OnPeopleCountChanged?.Invoke(PeopleCount, MaxPeopleCount);
    }
    public bool CanUsePeople(int amount)
    {
        return PeopleCount - amount >= 0;
    }

    public void AddMaxPeople(int amount)
    {
        MaxPeopleCount += amount;
        PeopleCount += amount;
        OnPeopleCountChanged?.Invoke(PeopleCount, MaxPeopleCount);
    }
    public void SubtractMaxPeople(int amount)
    {
        MaxPeopleCount -= amount;
        PeopleCount -= amount;
        if (PeopleCount < 0)
            PeopleCount = 0;
        OnPeopleCountChanged?.Invoke(PeopleCount, MaxPeopleCount);
    }

    public bool UseResources(ResourceData[] resources)
    {
        if (!IsResourcesUseable(resources))
            return false;

        ApplyUseResources(resources);
        return true;
    }
    public void ReturnResources(ResourceData[] resources, bool isHalfAmount)
    {
        foreach (ResourceData resourceData in resources)
        {
            int amount = (int)(isHalfAmount ? resourceData.amount / 2 : resourceData.amount);
            switch (resourceData.needResourceType)
            {
                case ResourceType.Wood:
                    GameManager.Instance.AddWood(amount);
                    break;
                case ResourceType.Ore:
                    GameManager.Instance.AddOre(amount);
                    break;
                case ResourceType.Food:
                    GameManager.Instance.AddFood(amount);
                    break;
                case ResourceType.People:
                    GameManager.Instance.AddPeople(amount);
                    break;
            }
        }
    }

    private bool IsResourcesUseable(ResourceData[] resources)
    {
        if (resources == null || resources.Length == 0)
            return false;

        bool isUseable = false;

        foreach (var resource in resources)
        {
            switch (resource.needResourceType)
            {
                case Defines.ResourceType.Wood:
                    isUseable = CanUseWood((int)resource.amount);
                    break;
                case Defines.ResourceType.Ore:
                    isUseable = CanUseOre((int)resource.amount);
                    break;
                case Defines.ResourceType.Food:
                    isUseable = CanUseFood((int)resource.amount);
                    break;
                case Defines.ResourceType.People:
                    isUseable = CanUsePeople((int)resource.amount);
                    break;
            }

            if (!isUseable) break;
        }

        return isUseable;
    }
    private void ApplyUseResources(ResourceData[] resources)
    {
        if (resources == null || resources.Length == 0)
            return;

        foreach (var resource in resources)
        {
            switch (resource.needResourceType)
            {
                case Defines.ResourceType.Wood:
                    SubtractWood((int)resource.amount);
                    break;
                case Defines.ResourceType.Ore:
                    SubtractOre((int)resource.amount);
                    break;
                case Defines.ResourceType.Food:
                    SubtractFood((int)resource.amount);
                    break;
                case Defines.ResourceType.People:
                    SubtractPeople((int)resource.amount);
                    break;
            }
        }
    }

    public ItemSO GetRandomEquip(Defines.EquipType type)
    {
        if (type == EquipType.None) return null;
        
        List<ItemSO> temp = type == EquipType.Weapon ?
            ItemList.Weapons : ItemList.Helmets;
        
        return temp[UnityEngine.Random.Range(0, temp.Count)];    
    }
}
