using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SO_BuildingData", menuName = "Datas/Build/SO_BuildingData")]
public class BuildingSO : BuildSO
{
    [Header("해당 건물의 분류")]
    public Defines.BuildingType buildingType;
    public Sprite icon;

    //건축할 재료, 체력, 공격력, 인구수 증가량, 인구수 소모량, 자원, 모델 
    [Header("건설에 필요한 자원량(int)")]
    public ResourceData[] NeedResources;

    [Header("건물 체력")]
    public float health;

    [Header("인구 증감")]
    public int providedPopulation;
    public int consumingPopulation;

    [Header("공격 건물 스탯")]
    public float attackPower;
    public float attackRange;
    public float attackDelay;
}
