using Defines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingCondition : MonoBehaviour, IDamageable
{
    BuildingObject buildingObject;

    public float CurHealth { get; private set; }
    public float MaxHealth { get; private set; }

    public float CurAttackPower { get; private set; }
    public float CurAttackRange { get; private set; }
    public float CurAttackDelay { get; private set; }

    public float CurProductiontDelay { get; private set; }

    private void Awake()
    {
        buildingObject = GetComponent<BuildingObject>();
    }

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        if (buildingObject.buildedSO == null) return;

        MaxHealth = buildingObject.buildedSO.health;
        CurHealth = MaxHealth;

        CurAttackPower = buildingObject.buildedSO.attackPower;
        CurAttackRange = buildingObject.buildedSO.attackRange;
        CurAttackDelay = buildingObject.buildedSO.attackDelay;

        CurProductiontDelay = buildingObject.buildedSO.ProductiontDelay;
    }

    public void Heal(int heal)
    {
        CurHealth += heal;
        if (CurHealth > MaxHealth) CurHealth = MaxHealth;
    }

    public void TakeDamage(float damage)
    {
        CurHealth -= damage;
        if (CurHealth < 0) CurHealth = 0;
    }

    public void KnockBack(Transform dest)
    {

    }

    public void BuildingDestroy()
    {
        if (buildingObject.TileObj != null)
        {
            if (buildingObject.buildedSO.buildingType == BuildingType.House)
            {
                // 만약에 최대 인구수 증가시키는 기능이 있다면 최대 인구수와 가용 인구수 차감 필요함
                GameManager.Instance.SubtractMaxPeople(buildingObject.buildedSO.providedPopulation);
            }
            else if (Utils.IsResourceBuilding(buildingObject.buildedSO.buildingType))
            {
                BuildingType originResourceType = Utils.BuidingTypeToOriginResourceType(buildingObject.buildedSO.buildingType);
                BuildSO buildSO = ResourceManager.Instance.Load<BuildSO>(Utils.BuildingEnumToSODataPath(originResourceType));
                buildingObject.TileObj.Build(buildSO);
            }
        }

        PoolManager.Instance.Despawn(gameObject);
    }
}
