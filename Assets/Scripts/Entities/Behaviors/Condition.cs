using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Condition : MonoBehaviour, IDamageable
{
    public event Action<float, float> HealthChangedEvent;
    public event Action<float, float> StaminaChangedEvent;
    public event Action<float, float> HungerChangedEvent;
    public event Action<float, float> ThirstChangedEvent;

    [SerializeField] private StatData baseStat;
    [SerializeField] private StatData currentStat;

    public StatData CurrentStat => currentStat;

    public void SetData(StatData stat, bool isRecovery = false)
    {
        this.baseStat = stat;
        currentStat.maxStamina = stat.maxStamina;
        currentStat.passiveStamina = stat.passiveStamina;
        currentStat.maxHunger = stat.maxHunger;
        currentStat.passiveHunger = stat.passiveHunger;
        currentStat.maxThirst = stat.maxThirst;
        currentStat.passiveThirst = stat.passiveThirst;
        currentStat.maxHealth = stat.maxHealth;
        currentStat.damage = stat.damage;
        currentStat.moveSpeed = stat.moveSpeed;
        currentStat.attackSpeed = stat.attackSpeed;
        currentStat.attackRange = stat.attackRange;
        if (isRecovery)
        {
            FullRecovery();
        }
    }
    public void EquipWeapon(int itemVelue)
    {
        currentStat.damage += itemVelue;
    }
    public void FullRecovery()
    {
        currentStat.health = currentStat.maxHealth;
        currentStat.stamina = currentStat.maxStamina;
        currentStat.hunger = currentStat.maxHunger;
        currentStat.thirst = currentStat.maxThirst;

        HealthChangedEvent?.Invoke(currentStat.health, currentStat.maxHealth);
        StaminaChangedEvent?.Invoke(currentStat.stamina, currentStat.maxStamina);
        HungerChangedEvent?.Invoke(currentStat.hunger, currentStat.maxHunger);
        ThirstChangedEvent?.Invoke(currentStat.thirst, currentStat.maxThirst);
    }

    public void Heal(int heal)
    {
        currentStat.health -= Mathf.Clamp(heal, 0, currentStat.maxHealth);
        HealthChangedEvent?.Invoke(currentStat.health, currentStat.maxHealth);
    }

    public void TakeDamage(float damage)
    {
        SoundManager.Instance.PlayOneShot("Damaged");
        SubtractHealth(damage);
    }

    public void SubtractHealth(float amount)
    {
        currentStat.health -= Mathf.Clamp(amount, 0, currentStat.maxHealth);
        HealthChangedEvent?.Invoke(currentStat.health, currentStat.maxHealth);
        if (currentStat.health <= 0)
        {
            Die();
        }
    }

    public bool UseStamina(float cost)
    {
        if (cost > currentStat.stamina)
        {
            return false;
        }

        currentStat.stamina -= cost;
        StaminaChangedEvent?.Invoke(currentStat.stamina, currentStat.maxStamina);

        return true;
    }

    public void StaminaRecovery(float amount)
    {
        currentStat.stamina = Mathf.Clamp(currentStat.stamina + amount, 0, currentStat.maxStamina);
        StaminaChangedEvent?.Invoke(currentStat.stamina, currentStat.maxStamina);
    }

    public void Hungry(float amount)
    {
        currentStat.hunger = Mathf.Clamp(currentStat.hunger - amount, 0, currentStat.maxHunger);
        if (currentStat.hunger <= 0)
            SubtractHealth(amount);

        HungerChangedEvent?.Invoke(currentStat.hunger, currentStat.maxHunger);
    }

    public void Eat(float amount)
    {
        currentStat.hunger = Mathf.Clamp(currentStat.hunger + amount, 0, currentStat.maxHunger);
        HungerChangedEvent?.Invoke(currentStat.hunger, currentStat.maxHunger);
    }

    public void Thirsty(float amount)
    {
        currentStat.thirst = Mathf.Clamp(currentStat.thirst - amount, 0, currentStat.maxThirst);
        if (currentStat.thirst <= 0)
            SubtractHealth(amount);

        ThirstChangedEvent?.Invoke(currentStat.thirst, currentStat.maxThirst);
    }

    public void Drink(float amount)
    {
        currentStat.thirst = Mathf.Clamp(currentStat.thirst + amount, 0, currentStat.maxThirst);
        ThirstChangedEvent?.Invoke(currentStat.thirst, currentStat.maxThirst);
    }

    private void Update()
    {
        if (currentStat.passiveHunger >= 0)
            Hungry(currentStat.passiveHunger * Time.deltaTime);
        if (currentStat.passiveThirst > 0)
            Thirsty(currentStat.passiveThirst * Time.deltaTime);
        if (currentStat.passiveStamina > 0)
            StaminaRecovery(currentStat.passiveStamina * Time.deltaTime);
    }

    private void Die()
    {
        // TODO : 사망처리
        Debug.Log("Die");
        transform.position = Vector3.zero;
        FullRecovery();
        TakeDamage(CurrentStat.maxHealth / 2);
    }

    public void KnockBack(Transform dest)
    {
        // 미구현
    }
}
