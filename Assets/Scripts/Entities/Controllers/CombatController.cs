using Assets.Scripts.Entities.Objects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour
{
    public GameObject FirePos;
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
        SoundManager.Instance.PlayOneShot("AttackPlayer");
        GameObject obj = PoolManager.Instance.SpawnProjectile("SlashProjectile", this.gameObject.transform);
        obj.gameObject.transform.position = this.transform.position + (Vector3.up * 0.25f);
        string[] tags = { "Monster" };
        obj.GetComponent<ProjectileController>().Init(FirePos, tags, condition.CurrentStat.damage, true);
        anim.AttackAnim();
    }

    public void Attacking(bool isAttacking)
    {
        this.isAttacking = isAttacking;
    }
}
