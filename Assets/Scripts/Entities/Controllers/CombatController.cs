using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour
{
    private Condition condition;
    private CharacterAnimController anim;
    private bool isAttacking;

    private float time;

    private void Awake()
    {
        condition = GetComponent<Condition>();
        anim = GetComponent<CharacterAnimController>();
    }

    private void Update()
    {
        if (isAttacking && Time.time - time > condition.CurrentStat.attackSpeed)
        {
            time = Time.time;
            Attack();
        }
    }

    private void Attack()
    {
        // 근접 공격 or 원거리 공격
        anim.AttackAnim();
    }

    public void Attacking(bool isAttacking)
    {
        this.isAttacking = isAttacking;
    }
}
