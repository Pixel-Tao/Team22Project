using Defines;
using UnityEngine;

public class BuildingObject : MonoBehaviour
{
    [SerializeField] public BuildSO buildedSO;
    private BuildingCondition condition;
    public TileObject TileObj { get; private set; }

    public BuildingSO BuildingSO => buildedSO as BuildingSO;
    public int ProvidedPopulation => BuildingSO?.providedPopulation ?? 0;
    public int ConsumingPopulation => GetCumsumingPopulation();

    private string str;

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

    public void SetTile(TileObject tile)
    {
        TileObj = tile;
        if (TileObj != null)
            tile.building = this;
    }

    public string GetInfo()//외부에서 호출할 함수
    {
        str = $"{buildedSO.displayName}\n{buildedSO.description}\n";
        str = string.Empty;

        if (BuildingSO != null)
        {
            switch (BuildingSO.buildingType)
            {
                case BuildingType.None:
                    str = "Debug : 건물 타입 지정되지않음";
                    break;
                case BuildingType.Castle_Red://체력,제공인구
                    str += HealthInfo();
                    str += providedPopInfo();
                    break;
                case BuildingType.House_A_Red://체력,제공인구
                    str += HealthInfo();
                    str += providedPopInfo();
                    str += BuildingInfo();
                    break;
                case BuildingType.Tower_A_Red://체력,공격력,소모인구
                    str += HealthInfo();
                    str += AttackInfo();
                    str += consumingPopInfo();
                    str += BuildingInfo();
                    break;

            }
        }

        Debug.Log("Debug : 타입별 정보\n" + str);

        return str;
    }

    public string BuildingInfo()//(건설메뉴에서)보여줄 요구사항 정보
    {
        str = string.Empty;
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
        Debug.Log("Debug : 건설 요구사항\n" + str);
        return str;
    }


    private string HealthInfo()
    {
        return $"현재 내구도 : {condition.CurHealth} / {condition.MaxHealth}\n";
    }

    private string AttackInfo()
    {
        if (condition.CurAttackPower <= 0) Debug.Log("Debug : 공격력이 0이거나 0보다 작음");
        if (condition.CurAttackRange <= 0) Debug.Log("Debug : 사거리가 0이거나 0보다 작음");
        if (condition.CurAttackDelay <= 0) Debug.Log("Debug : 공격지연속도가 0이거나 0보다 작음");
        return $"현재 공격력 : {condition.CurAttackPower}\n현재 사거리 : {condition.CurAttackRange}\n현재 공격지연속도 : {condition.CurAttackDelay}\n";
    }

    private string providedPopInfo()
    {
        return $"인구 증가 : {(BuildingSO?.providedPopulation.ToString() ?? "없음")}\n";
    }

    private string consumingPopInfo()
    {
        return $"소모 인구 : {(BuildingSO?.consumingPopulation.ToString() ?? "없음")}\n";
    }

    private void productionInfo()
    {
        if (buildedSO.ProductPrefabs.Length <= 0)
        {
            Debug.Log("Debug : 생산품정보 없음");//해당 건물 SO의 생산건물스텟 확인
            return;
        }

        InteractableSO productItemSO;

        for (int i = 0; i < buildedSO.ProductPrefabs.Length; i++)
        {
            productItemSO = buildedSO.ProductPrefabs[i].GetComponent<ItemObject>().data;
            str += $"생산품 : {productItemSO.displayName} 생산주기 : {buildedSO.ProductiontDelay}\n";
        }

    }

    public void Destroy()
    {
        if (BuildingSO?.buildingType == BuildingType.House_A_Red)
        {
            // 만약에 최대 인구수 증가시키는 기능이 있다면 최대 인구수와 가용 인구수 차감 필요함
            GameManager.Instance.SubtractMaxPeople(BuildingSO.providedPopulation);
            TileObj.building = null;
        }

        PoolManager.Instance.Despawn(gameObject);
    }

}