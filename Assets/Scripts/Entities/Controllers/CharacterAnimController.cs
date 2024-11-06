using Defines;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimController : MonoBehaviour
{
    private readonly int AttackHash = Animator.StringToHash("Attack");
    private readonly int HitHash = Animator.StringToHash("Hit");
    private readonly int DeadHash = Animator.StringToHash("IsDead");
    private readonly int SpeedMultiplierHash = Animator.StringToHash("SpeedMultiplier");

    private Animator animator;
    private float defaultAttackSpeed = 1f;

    private List<CharacterAnimCombatLayerType> layers;

    public CharacterAnimCombatLayerType CurrentCombatLayerType { get; private set; } = CharacterAnimCombatLayerType.Base_Layer;

    private void Start()
    {
        ResetLayerWeight();
    }

    public void SetAnimator(Animator animator)
    {
        this.animator = animator;
    }
    private void InitAvailableLayers()
    {
        Array array = Enum.GetValues(typeof(CharacterAnimCombatLayerType));
        layers = new List<CharacterAnimCombatLayerType>();
        foreach (CharacterAnimCombatLayerType layer in array)
        {
            if (layer == CharacterAnimCombatLayerType.Base_Layer) continue;
            layers.Add(layer);
        }
    }
    public void ResetLayerWeight()
    {
        if (layers == null)
            InitAvailableLayers();

        for (int i = 0; i < layers.Count; i++)
        {
            animator.SetLayerWeight((int)layers[i], 0);
        }
        CurrentCombatLayerType = CharacterAnimCombatLayerType.Melee_1H_Layer;
    }

    // 장비 장착할때 함께 설정해야함.
    public void UseCombatLayer(CharacterAnimCombatLayerType layerType)
    {
        ResetLayerWeight();
        // 레이어 가중치를 활용하여 공격 모션을 변경 함
        animator.SetLayerWeight((int)layerType, 1);
        CurrentCombatLayerType = layerType;
    }

    public void AttackAnim()
    {
        if (animator == null)
        {
            Debug.LogWarning("Animator is null");
            return;
        }
        if (CurrentCombatLayerType == CharacterAnimCombatLayerType.Base_Layer)
        {
            Debug.LogWarning("CurrentCombatLayerType is not set");
            return;
        }
        
        animator.SetFloat(SpeedMultiplierHash, defaultAttackSpeed);
        animator.SetTrigger(AttackHash);
    }
    public void HitAnim()
    {
        if (animator == null)
        {
            Debug.LogWarning("Animator is null");
            return;
        }
        animator.SetTrigger(HitHash);
    }
    public void DeadAnim()
    {
        if (animator == null)
        {
            Debug.LogWarning("Animator is null");
            return;
        }
        if (animator.GetBool(DeadHash) == true) return;
        animator.SetBool(DeadHash, true);
    }
    public void AliveAnim()
    {
        if (animator == null)
        {
            Debug.LogWarning("Animator is null");
            return;
        }
        if (animator.GetBool(DeadHash) == false) return;
        animator.SetBool(DeadHash, false);
    }
    public void MoveAnim(CharacterMoveStepType type)
    {
        if (animator == null)
        {
            Debug.LogWarning("Animator is null");
            return;
        }

        animator.SetInteger("Step", (int)type);
    }
    public void IdleAnim()
    {
        if (animator == null)
        {
            Debug.LogWarning("Animator is null");
            return;
        }
        animator.SetInteger("Step", (int)CharacterMoveStepType.Idle);
    }
}
