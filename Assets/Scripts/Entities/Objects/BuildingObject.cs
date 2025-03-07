using Defines;
using System.Text;
using UnityEngine;

public class BuildingObject : MonoBehaviour
{
    [SerializeField] public BuildSO buildedSO;
    private BuildingCondition condition;
    public TileObject TileObj { get; private set; }

    public BuildingSO BuildingSO => buildedSO as BuildingSO;
    public BuildNaturalObjectSO BuildNaturalObjectSO => buildedSO as BuildNaturalObjectSO;
    public int ProvidedPopulation => BuildingSO?.providedPopulation ?? 0;
    public int ConsumingPopulation => GetCumsumingPopulation();

    private string rightClickMessage => GameManager.Instance.IsBuildMode ? "\n[마우스 우클릭 시 건물 회전]" : string.Empty;
    private string interactMessage => !GameManager.Instance.IsBuildMode ? "\n[E 키를 눌러 상호작용]" : string.Empty;
    private string attackMessage => !GameManager.Instance.IsBuildMode ? "\n[공격하여 아이템 획득]" : string.Empty;
    private string productionMessage => !GameManager.Instance.IsBuildMode ? "\n[자원을 일정 량 자동 획득]" : string.Empty;

    private int GetCumsumingPopulation()
    {
        if (BuildingSO?.NeedResources == null) return 0;
        foreach (var resource in BuildingSO.NeedResources)
        {
            if (resource.needResourceType == ResourceType.People)
            {
                return (int)resource.amount;
            }
        }

        return 0;
    }

    private void Awake()
    {
        condition = GetComponent<BuildingCondition>();
    }

    private void Start()
    {
        if (BuildingSO?.buildingType == BuildingType.Castle_Red)
        {
            UpdatePeople();
        }
    }


    public void SetTile(TileObject tile)
    {
        TileObj = tile;
        if (TileObj != null)
        {
            tile.building = this;
            UpdatePeople();
        }

    }

    public void StatReset()
    {
        condition?.Init();
    }

    public string GetInfo()//외부에서 호출할 함수
    {
        StringBuilder sb = new StringBuilder();

        sb.AppendLine(buildedSO.displayName);
        sb.AppendLine(buildedSO.description);
        if (BuildingSO != null)
        {
            switch (BuildingSO.buildingType)
            {
                case BuildingType.Castle_Red://체력,제공인구
                    sb.AppendLine(BuildingInfo());
                    sb.AppendLine(HealthInfo());
                    sb.AppendLine(ProvidedPopInfo());
                    break;
                case BuildingType.House_A_Red://체력,제공인구
                    sb.AppendLine(BuildingInfo());
                    sb.AppendLine(HealthInfo());
                    sb.AppendLine(ProvidedPopInfo());
                    break;
                case BuildingType.Tower_A_Red://체력,공격력,소모인구
                    sb.AppendLine(BuildingInfo());
                    sb.AppendLine(HealthInfo());
                    sb.AppendLine(AttackInfo());
                    break;
                case BuildingType.Windmill_Red://체력,생산품
                case BuildingType.Lumbermill_Red:
                case BuildingType.Quarry_Red:
                case BuildingType.Watermill_Red:
                    sb.AppendLine(BuildingInfo());
                    sb.AppendLine(HealthInfo());
                    sb.AppendLine(ProductionInfo());
                    sb.Append(productionMessage);
                    break;
                case BuildingType.Wall_Corner_A_Gate:
                case BuildingType.Wall_Corner_A_Inside:
                case BuildingType.Wall_Corner_A_Outside:
                case BuildingType.Wall_Corner_B_Inside:
                case BuildingType.Wall_Corner_B_Outside:
                case BuildingType.Wall_Straight:
                case BuildingType.Wall_Straight_Gate:
                    sb.AppendLine(BuildingInfo());
                    sb.AppendLine(HealthInfo());
                    break;
                case BuildingType.Blacksmith_Red:
                    sb.AppendLine(BuildingInfo());
                    sb.AppendLine(HealthInfo());
                    sb.Append(interactMessage);
                    break;
                case BuildingType.Market_Red:
                    sb.AppendLine(BuildingInfo());
                    sb.AppendLine(HealthInfo());
                    sb.Append(attackMessage);
                    break;
            }
            sb.Append(rightClickMessage);
        }
        return sb.ToString();
    }

    public string BuildingInfo()//(건설메뉴에서)보여줄 요구사항 정보
    {
        string str = string.Empty;
        if (BuildingSO != null)
        {
            for (int i = 0; i < BuildingSO.NeedResources.Length; i++)
            {
                if (BuildingSO.NeedResources[i].amount < 0) continue;
                str += $"필요 {BuildingSO.NeedResources[i].needResourceType.ToString()} : {BuildingSO.NeedResources[i].amount}\n";
            }
        }
        else
        {
            str = "없음";
        }

        return str;
    }

    private string HealthInfo()
    {
        return $"현재 내구도 : {condition.CurHealth:F0} / {condition.MaxHealth}\n";
    }

    private string AttackInfo()
    {
        if (condition.CurAttackPower <= 0) Debug.Log("Debug : 공격력이 0이거나 0보다 작음");
        if (condition.CurAttackRange <= 0) Debug.Log("Debug : 사거리가 0이거나 0보다 작음");
        if (condition.CurAttackDelay <= 0) Debug.Log("Debug : 공격지연속도가 0이거나 0보다 작음");
        return $"현재 공격력 : {condition.CurAttackPower}\n현재 사거리 : {condition.CurAttackRange}\n현재 공격지연속도 : {condition.CurAttackDelay}\n";
    }

    private string ProvidedPopInfo()
    {
        return $"인구 증가 : {(BuildingSO?.providedPopulation.ToString() ?? "없음")}\n";
    }

    private string ProductionInfo()
    {
        if (buildedSO.ProductPrefabs.Length <= 0)
        {
            Debug.Log("Debug : 생산품정보 없음");//해당 건물 SO의 생산건물스텟 확인
            return string.Empty;
        }

        InteractableSO productItemSO;
        string str = string.Empty;
        for (int i = 0; i < buildedSO.ProductPrefabs.Length; i++)
        {
            productItemSO = buildedSO.ProductPrefabs[i].GetComponent<ItemObject>().data;
            str += $"생산품 : {productItemSO.displayName}";
        }

        return str;
    }

    private void UpdatePeople()
    {
        if (
            BuildingSO?.buildingType == BuildingType.Castle_Red
            || BuildingSO?.buildingType == BuildingType.House_A_Red
            )
        {
            GameManager.Instance.AddMaxPeople(BuildingSO.providedPopulation);
        }
    }

    public void Destroy()
    {
        if (BuildingSO?.buildingType == BuildingType.House_A_Red)
        {
            // 만약에 최대 인구수 증가시키는 기능이 있다면 최대 인구수와 가용 인구수 차감 필요함
            GameManager.Instance.SubtractMaxPeople(BuildingSO.providedPopulation);
        }
        TileObj.building = null;

        PoolManager.Instance.Despawn(gameObject);
    }

}