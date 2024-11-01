using Defines;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BuildingObject : MonoBehaviour
{
    [SerializeField] public BuildSO buildedSO;
    private BuildingCondition condition;

    private string str;

    private void Awake()
    {
        condition = GetComponent<BuildingCondition>();
    }

    private void Start()
    {
        str = string.Empty;
        Invoke("GetInfo", 1f);
        
    }

    public string GetInfo()//외부에서 호출할 함수
    {
        str = $"{buildedSO.displayName}\n{buildedSO.description}\n";
        str = string.Empty;

        switch (buildedSO.buildingType)
        {
            case BuildingType.None:
                str = "Debug : 건물 타입 지정되지않음";
                break;
            case BuildingType.Castle://체력,제공인구
                str += HealthInfo();
                str += providedPopInfo();
                break;
            case BuildingType.House://체력,제공인구
                str += HealthInfo();
                str += providedPopInfo();
                str += BuildingInfo();
                break;
            case BuildingType.Tower://체력,공격력,소모인구
                str += HealthInfo();
                str += AttackInfo();
                str += consumingPopInfo();
                str += BuildingInfo();
                break;
            case BuildingType.Defence://체력
                str += HealthInfo();
                str += BuildingInfo();
                break;
            case BuildingType.NaturalResources://생산품정보,생산주기
                productionInfo();
                break;
            case BuildingType.ResourceBuilding://체력,생산품정보,생산주기
                str += HealthInfo();
                str += BuildingInfo();
                break;

        }
        Debug.Log("Debug : 타입별 정보\n"+str);
        
        return str;
    }

    public string BuildingInfo()//(건설메뉴에서)보여줄 요구사항 정보
    {
        str = string.Empty;
        for (int i = 0; i < buildedSO.NeedResources.Length; i++)
        {
            if (buildedSO.NeedResources[i].amount < 0) continue;
            str += $"필요 {buildedSO.NeedResources[i].needResourceType.ToString()} : {buildedSO.NeedResources[i].amount}\n";
        }
        Debug.Log("Debug : 건설 요구사항\n"+str);
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
        return $"인구 증가 : {buildedSO.providedPopulation}\n";
    }

    private string consumingPopInfo()
    {
        return $"소모 인구 : {buildedSO.consumingPopulation}\n";
    }

    private void productionInfo()
    {
        if (buildedSO.ProductPrefabs.Length <= 0)
        {
            Debug.Log("Debug : 생산품정보 없음");//해당 건물 SO의 생산건물스텟 확인
            return;
        }

        InteractableSO productItemSO; 

        for (int i = 0;  i < buildedSO.ProductPrefabs.Length; i++)
        {
            productItemSO = buildedSO.ProductPrefabs[i].GetComponent<ItemObject>().data;
            str += $"생산품 : {productItemSO.displayName} 생산주기 : {buildedSO.ProductiontDelay}\n";
        }

    }

}