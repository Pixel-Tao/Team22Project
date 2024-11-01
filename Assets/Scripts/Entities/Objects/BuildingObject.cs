using Defines;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BuildingObject : InteractableObject
{
    [SerializeField] private BuildSO buildedSO;
    private BuildingCondition condition;

    private string str;

    private void Start()
    {
        str = string.Empty;
    }

    public string BasicInfo()//일반 선택시 보여줄 정보
    {
        str = $"{buildedSO.displayName}\n{buildedSO.description}";
        return str;
    }

    public string DetailInfo()//(이미 건설된 건물)상세보기시 보여줄 정보
    {
        switch (buildedSO.buildingType)
        {
            case BuildingType.None:
                str= "타입 지정되지않음";
                break;
            case BuildingType.House://체력,제공인구
                str += HealthInfo();
                str += providedPopInfo();

                break;
            case BuildingType.Tower://체력,공격력,소모인구
                break;
            case BuildingType.Wall://체력
                break;
            case BuildingType.Gate://체력
                break;
            case BuildingType.Castle://체력,제공인구
                break;
        }
        return str;
    }

    public void BuildInfo()//(건설메뉴에서)보여줄 요구사항 정보
    {

    }

    private string HealthInfo()
    {
        return $"현재 내구도 : {condition.curHealth}\n";
    }

    private string AttackInfo()
    {

    }

    private string providedPopInfo()
    {

    }

    private string consumingPopInfo()
    {

    }



}