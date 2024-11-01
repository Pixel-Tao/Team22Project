using UnityEngine;

[CreateAssetMenu(fileName = "S0_MobData", menuName = "Datas/SO_MobData")]
public class MonsterSO : ScriptableObject
{
    public string name; 
    public int health;
    public int attack;   
    public float speed;
    public float attackDelay;
    public float detectiveLength;
    public float attackRange;
    //public GameObject mobPrefabs;
}
