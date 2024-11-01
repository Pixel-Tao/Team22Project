using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingCondition : MonoBehaviour, IDamageable
{
    BuildSO buildedSO;

    public float CurHealth { get; private set; }
    public float MaxHealth { get; private set; }

    public float CurAttackPower { get; private set; }
    public float CurAttackRange { get; private set; }
    public float CurAttackDelay { get; private set; }

    public float CurProductiontDelay { get; private set; }

    private void Awake()
    {
        buildedSO = GetComponent<BuildingObject>().buildedSO;
    }

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        MaxHealth = buildedSO.health;
        CurHealth = MaxHealth;

        CurAttackPower = buildedSO.attackPower;
        CurAttackRange = buildedSO.attackRange;
        CurAttackDelay = buildedSO.attackDelay;

        CurProductiontDelay = buildedSO.ProductiontDelay;
    }

    public void Heal(int heal)
    {
        CurHealth += heal;
        if(CurHealth > MaxHealth) CurHealth = MaxHealth;
    }

    public void TakeDamage(int damage)
    {
        CurHealth -= damage;
        if (CurHealth < 0) CurHealth = 0;
    }
}
