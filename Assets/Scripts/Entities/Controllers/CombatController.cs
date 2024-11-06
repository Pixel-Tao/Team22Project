using Assets.Scripts.Entities.Objects;
using Defines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour
{
    public GameObject FirePos;
    private Condition condition;
    private CharacterAnimController anim;
    private Equipment equipment;
    private bool isAttacking;

    public float AttackDamage => (equipment?.EquipWeaponDate?.attack ?? 0) + (condition?.CurrentStat.damage ?? 0);
    // 기본 공격속도 + 장비 공격속도
    public float AttackSpeed => (condition?.CurrentStat.attackSpeed ?? 0) + (equipment?.EquipWeaponDate?.attackSpeed ?? 0);
    public float AttackScale => 1 + (equipment?.EquipWeaponDate?.attackScale ?? 0);

    private float time;

    private void Awake()
    {
        condition = GetComponent<Condition>();
        anim = GetComponent<CharacterAnimController>();
        equipment = GetComponent<Equipment>();
    }

    private void Update()
    {
        if (isAttacking && Time.time - time > AttackSpeed)
        {
            time = Time.time;
            Attack();
        }
    }

    private void Attack()
    {
        // 근접 공격 or 원거리 공격
        SoundManager.Instance.PlayOneShotPoint("AttackPlayer", transform.position);

        if (equipment?.EquipWeaponDate != null)
        {
            switch (equipment.EquipWeaponDate.combatMotionType)
            {
                case CharacterAnimCombatLayerType.Melee_1H_Layer:
                case CharacterAnimCombatLayerType.Melee_2H_Layer:
                    MeleeAttack();
                    break;
                case CharacterAnimCombatLayerType.Ranged_1H_Layer:
                case CharacterAnimCombatLayerType.Ranged_2H_Layer:
                    RangedAttack();
                    break;
                case CharacterAnimCombatLayerType.Spellcasting_Layer:
                    MagicAttack();
                    break;
            }
        }
        else
        {
            MeleeAttack();
        }
    }

    private void MeleeAttack()
    {
        Attack("SlashProjectile", transform);
    }
    private void RangedAttack()
    {
        Attack("PlayerArrowProjectile", null);
    }
    private void MagicAttack()
    {
        Attack("MagicMissleProjectile", null);
    }

    private void Attack(string projectileName, Transform transform)
    {
        GameObject obj = PoolManager.Instance.SpawnProjectile(projectileName, transform);
        obj.gameObject.transform.position = this.transform.position + (Vector3.up * 0.25f);
        obj.transform.localScale = new Vector3(AttackScale, 1, AttackScale);
        string[] tags = { "Monster" };
        obj.GetComponent<ProjectileController>().Init(FirePos, tags, AttackDamage, true);
        anim.AttackAnim();
    }

    public void Attacking(bool isAttacking)
    {
        this.isAttacking = isAttacking;
    }
}
