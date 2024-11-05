using UnityEngine;

[CreateAssetMenu(fileName = "S0_MobData", menuName = "Datas/SO_MobData")]
public class MonsterSO : ScriptableObject
{
    [Header("기본 수치")]
    public float health;
    public float speed;
    
    [Header("공격 수치")]
    public float attackDelay;
    public float attackDamage;
    public float attackRange;
    public float detectiveLength;

    [Header("공격 유형")]
    public bool isRangedWeapon;
    public string projectileName;
    public string projectileSound;
    //[SerializeField]public string[] enemyObjectTags;
}
