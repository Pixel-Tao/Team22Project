using UnityEngine;

public class BuildingCondition : MonoBehaviour, IDamageable
{
    BuildingObject buildingObject;

    public float CurHealth { get; private set; }
    public float MaxHealth { get; private set; }

    public float CurAttackPower { get; private set; }
    public float CurAttackRange { get; private set; }
    public float CurAttackDelay { get; private set; }

    public float CurProductiontDelay { get; private set; }

    private Goal goal;

    private void Awake()
    {
        buildingObject = GetComponent<BuildingObject>();
        goal = GetComponent<Goal>();
    }

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        if (buildingObject.BuildingSO == null) return;

        MaxHealth = buildingObject.BuildingSO.health;
        CurHealth = MaxHealth;

        CurAttackPower = buildingObject.BuildingSO.attackPower;
        CurAttackRange = buildingObject.BuildingSO.attackRange;
        CurAttackDelay = buildingObject.BuildingSO.attackDelay;

        CurProductiontDelay = buildingObject.buildedSO.ProductiontDelay;
    }

    public void Heal(int heal)
    {
        CurHealth += heal;
        if (CurHealth > MaxHealth) CurHealth = MaxHealth;
    }

    public void TakeDamage(float damage)
    {
        CurHealth -= damage;
        if (CurHealth < 0)
        {
            CurHealth = 0;

            if (goal != null)
            {
                GameManager.Instance.GameOver();
            }
            else
            {
                buildingObject.Destroy();
                GameManager.Instance.ReturnPeople(buildingObject.BuildingSO?.NeedResources);
                buildingObject.TileObj?.ReturnNaturalObject();
            }
        }
    }
}
