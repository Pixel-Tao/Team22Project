using Defines;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "S0_BuildData", menuName = "Datas/S0_BuildData")]
public class BuildSO : InteractableSO
{
    public BuildType buildType;
    
    [Header("건물 모델")]
    public GameObject modelPrefab;

    [Header("생산 건물 스탯")]
    public GameObject[] ProductPrefabs;
    public float ProductiontDelay;
}
