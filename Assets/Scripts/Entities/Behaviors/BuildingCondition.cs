using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingCondition : MonoBehaviour, IDamageable
{
    //핸들러역할 병합

    BuildSO buildedSO;

    public float curHealth { get; private set; }
    public float maxHealth { get; private set; }

    public float curAttackPower { get; private set; }


    private void Awake()
    {
        buildedSO = GetComponent<BuildSO>();
    }

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        maxHealth = buildedSO.health;
        curHealth = maxHealth;

        if (buildedSO.attackPower > 0) curAttackPower = buildedSO.attackPower;
    }

    public void Heal(int heal)
    {
        curHealth += heal;
    }

    public void TakeDamage(int damage)
    {
        curHealth -= damage;
    }
}
