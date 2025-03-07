using UnityEngine;

[System.Serializable]
public struct StatData
{
    [Header("생명력")]
    public float maxHealth;
    public float health;

    [Header("스태미나")]
    public float stamina;
    public float maxStamina;
    public float passiveStamina;

    [Header("배고픔")]
    public float hunger;
    public float maxHunger;
    public float passiveHunger;

    [Header("목마름")]
    public float thirst;
    public float maxThirst;
    public float passiveThirst;

    [Header("기본 스탯")]
    public int damage;
    public float moveSpeed;
    public float attackSpeed;
    public float attackRange;
}