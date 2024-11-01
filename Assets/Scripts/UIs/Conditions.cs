using UnityEngine;
using UnityEngine.UI;

public class Conditions : MonoBehaviour
{
    // 생명력
    public ConditionSlot health;
    // 마나
    public ConditionSlot stamina;
    // 배고픔
    public ConditionSlot hunger;
    // 목마름
    public ConditionSlot thirsty;

    private void Awake()
    {
        if (CharacterManager.Instance.Player.TryGetComponent(out Condition condition))
        {
            condition.HealthChangedEvent += health.ValueChanged;
            condition.StaminaChangedEvent += stamina.ValueChanged;
            condition.HungerChangedEvent += hunger.ValueChanged;
            condition.ThirstChangedEvent += thirsty.ValueChanged;

            condition.FullRecovery();
        }
    }
}