using Defines;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public event Action<int> OnWoodCountChanged;
    public event Action<int> OnOreCountChanged;
    public event Action<int, int> OnPeopleCountChanged;
    public event Action<int> OnFoodCountChanged;

    public bool IsBuildMode { get; private set; } = false;
    public bool IsInteracting => UIManager.Instance.IsPopupOpeing;

    public Goal Goal { get; private set; }
    public DayCycle currentDayCycle = DayCycle.DAY;
    private float localTimer = 0f;
    private int dayCount = 1;
    //

    public int WoodCount { get; private set; }
    public int OreCount { get; private set; }
    public int FoodCount { get; private set; }
    public int PeopleCount { get; private set; }
    public int MaxPeopleCount { get; private set; }

    public ItemListSO ItemList { get; private set; }
    public List<MobSpawnMachine> machines = new List<MobSpawnMachine>();

    //===========================스포너 제어========================================================
    /// <summary>
    /// 맵에 배치되어 있는 스포너는 자동으로 해당 함수를 호출 합니다, 외부에서 호출금지XXXXX
    /// </summary>
    /// <param name="m"></param>
    public void AddMachine(MobSpawnMachine m)
    {
        machines.Add(m);
    }
    /// <summary>
    /// 모든 스포너가 작동을 중지/실행 하도록 트리거 합니다. 초기값은 실행입니다.
    /// </summary>
    public void TriggerAllMachine()
    {
        foreach (MobSpawnMachine machine in machines)
        {
            machine.TriggerMachineState();
        }
    }
    /// <summary>
    /// 모든 스포너 실행/중지 지정명령.
    /// </summary>
    /// <param name="val"></param>
    public void SetAllMachine(SPAWNSTATE val)
    {
        foreach (MobSpawnMachine machine in machines)
        {
            machine.TriggerMachineState(val);
        }
    }
    /// <summary>
    /// 모든 몬스터 스포너에 난이도를 향상시키도록 명령합니다.
    /// </summary>
    public void ScalingAllMachine()
    {
        foreach (MobSpawnMachine machine in machines)
        {
            machine.DecreaseScale();
        }
    }
    /// <summary>
    /// 매니저에서 동작하는 스포너 명령 업데이트
    /// </summary>
    public void ControllMachine(DayCycle cycle, bool isGameEnd = false)
    {
        if (isGameEnd)
        {
            //성이 파괴됨.
            SetAllMachine(SPAWNSTATE.WAITING);
            return;
        }

        switch (cycle)
        {
            case DayCycle.NONE:
                //TODO : 적들의 수 증가.
                UIManager.Instance.SystemMessage($"[{dayCount}일째 생존..]\n앞으로 밤에 더 많은 몬스터가 생성됩니다.");
                dayCount++;
                ScalingAllMachine();
                break;
            case DayCycle.DAY:
                //TODO : 아침이됨.
                UIManager.Instance.SystemMessage("[아침이 되었습니다]\n더이상 몬스터가 스폰되지 않습니다.");
                SetAllMachine(SPAWNSTATE.WAITING);
                break;
            case DayCycle.NIGHT:
                //TODO : 저녁이됨.
                UIManager.Instance.SystemMessage("[저녁이 되었습니다]\n무덤에서 몬스터가 기어나옵니다!!");
                SetAllMachine(SPAWNSTATE.WORKING);
                break;
        }

        UIManager.Instance.DayNightChange(cycle);
    }
    //==============================================================================================

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
        }
        else
        {
            // 건설모드가 아닐때 카메라 시점 변경
            CharacterManager.Instance.Player.NormalMode();
        }
        UIManager.Instance.CloseAllPopupUI();
        UIManager.Instance.ModeChange(IsBuildMode);
    }
    public override void Init()
    {
        base.Init();

        AddWood(100);
        AddOre(100);
        AddFood(100);
        //AddMaxPeople(1000);
        IsBuildMode = false;

        ItemList = ResourceManager.Instance.GetSOData<ItemListSO>("Item/SO_ItemList");
    }
    public void GameOver()
    {
        UIManager.Instance.SingletonDestroy();
        SoundManager.Instance.SingletonDestroy();
        SceneManager.LoadScene("TitleScene");
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
                    ReturnPeople(resourceData);
                    break;
            }
        }
    }
    public void ReturnPeople(ResourceData[] resources)
    {
        if (resources == null || resources.Length == 0)
            return;

        foreach (ResourceData resource in resources)
        {
            if (resource.needResourceType == ResourceType.People)
            {
                ReturnPeople(resource);
                return;
            }
        }
    }
    public void ReturnPeople(ResourceData resource)
    {
        if (resource.needResourceType != ResourceType.People)
            return;

        AddPeople((int)resource.amount);
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

    //디버그를 위한 임시기능..
    public void AcceptConsole(string val)
    {
        if (val?.ToUpper() == "WOOD") AddWood(10000);
        else if (val?.ToUpper() == "FOOD") AddFood(10000);
        else if (val?.ToUpper() == "ORE") AddOre(10000);
        else if (val?.ToUpper() == "SHOW ME THE MONEY")
        {
            AddWood(10000);
            AddFood(10000);
            AddOre(10000);
            //TODO : GODMOD
        }
    }
}
