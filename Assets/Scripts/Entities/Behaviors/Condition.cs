using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Condition : MonoBehaviour, IDamageable
{
    public event Action<int> HealthChangedEvent;

    private int hp;
    private int maxHp;

    public void Heal(int heal)
    {
        hp += heal;
        HealthChangedEvent?.Invoke(hp);
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        HealthChangedEvent?.Invoke(hp);
    }
}
