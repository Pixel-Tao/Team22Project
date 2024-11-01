using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "S0_BuildData", menuName = "Datas/S0_BuildData")]
public class BuildSO : InteractableSO
{
    //건축할 재료, 체력, 공격력, 인구수 증가량, 인구수 소모량, 자원, 모델 

    [Header("해당 건물의 분류")]
    public Defines.BuildingType buildingType;

    [Header("건설에 필요한 자원량(int) 목재,석재,인구 순")]
    public int[] requiredResources = new int[3];

    [Header("건물 스텟")]
    public float health;
    public float attackPower;
    public int consumingPopulation;
    public int providedPopulation;
    
    [Header("해당 건물로부터 드랍되는 목록")]
    public GameObject[] dropPrefabs;

    [Header("건물 모델")]
    public GameObject modelPrefab;

    

}
