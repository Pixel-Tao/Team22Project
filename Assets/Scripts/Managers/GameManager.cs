using System;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public event Action<int> OnWoodCountChanged;
    public event Action<int> OnOreCountChanged;
    public event Action<int, int> OnPeopleCountChanged;
    public event Action<int> OnFoodCountChanged;

    public bool IsBuildMode { get; private set; } = false;

    public int WoodCount { get; private set; }
    public int OreCount { get; private set; }
    public int FoodCount { get; private set; }
    public int PeopleCount { get; private set; }
    public int MaxPeopleCount { get; private set; }

    public void ToggleBuildMode()
    {
        IsBuildMode = !IsBuildMode;
        if (IsBuildMode)
        {
            // 건설모드일때 카메라 시점 변경
            CharacterManager.Instance.Player.BuildMode();
        }
        else
        {
            // 건설모드가 아닐때 카메라 시점 변경
            CharacterManager.Instance.Player.NormalMode();
        }
    }
    public override void Init()
    {
        base.Init();

        AddWood(100);
        AddOre(100);
        AddFood(100);
        IsBuildMode = false;
    }

    public void AddWood(int amount)
    {
        WoodCount += amount;
        OnWoodCountChanged?.Invoke(WoodCount);
    }
    public bool UseWood(int amount)
    {
        if (WoodCount - amount < 0)
            return false;

        WoodCount -= amount;
        OnWoodCountChanged?.Invoke(WoodCount);

        return true;
    }

    public void AddOre(int amount)
    {
        OreCount += amount;
        OnOreCountChanged?.Invoke(OreCount);
    }
    public bool UseOre(int amount)
    {
        if (OreCount - amount < 0)
            return false;

        OreCount -= amount;
        OnOreCountChanged?.Invoke(OreCount);

        return true;
    }

    public void AddFood(int amount)
    {
        FoodCount += amount;
        OnFoodCountChanged?.Invoke(FoodCount);
    }
    public bool UseFood(int amount)
    {
        if (FoodCount - amount < 0)
            return false;

        FoodCount -= amount;
        OnFoodCountChanged?.Invoke(FoodCount);

        return true;
    }

    public void AddPeople(int amount)
    {
        PeopleCount += amount;
        if (PeopleCount > MaxPeopleCount)
            MaxPeopleCount = PeopleCount;

        OnPeopleCountChanged?.Invoke(PeopleCount, MaxPeopleCount);
    }
    public bool UsePeople(int amount)
    {
        if (PeopleCount - amount < 0)
            return false;

        PeopleCount -= amount;
        OnPeopleCountChanged?.Invoke(PeopleCount, MaxPeopleCount);
        return true;
    }

    public void AddMaxPeople(int amount)
    {
        MaxPeopleCount += amount;
        PeopleCount += amount;
        OnPeopleCountChanged?.Invoke(PeopleCount, MaxPeopleCount);
    }
    public void SubtractPeople(int amount)
    {
        MaxPeopleCount -= amount;
        PeopleCount -= amount;
        if (PeopleCount < 0)
            PeopleCount = 0;
        OnPeopleCountChanged?.Invoke(PeopleCount, MaxPeopleCount);
    }

}
