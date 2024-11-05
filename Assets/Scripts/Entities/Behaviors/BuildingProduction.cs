using UnityEditor.Search;
using UnityEngine;

public class BuildingProduction : MonoBehaviour
{
    BuildingObject building;
    GameObject productObject;
    private float delay;
    private float localTimer;
    Rigidbody rigid;



    private void Start()
    {
        Init();
    }
    private void Init()
    {
        building = gameObject.GetComponent<BuildingObject>();
        delay = building.buildedSO.ProductiontDelay;
    }

    private void Update()
    {
        localTimer += Time.deltaTime;
        if (localTimer > delay)
        {
            if (building.buildedSO.buildType == Defines.BuildType.Building)
            {
                BuildingSO buildingSO = building.buildedSO as BuildingSO;
                if (buildingSO == null)
                    return;

                switch (buildingSO.buildingType)
                {
                    case Defines.BuildingType.Windmill_Red:
                        CharacterManager.Instance.AddItem(CreateTempItem(Defines.ResourceType.Food));
                        break;
                    case Defines.BuildingType.Lumbermill_Red:
                        CharacterManager.Instance.AddItem(CreateTempItem(Defines.ResourceType.Wood));
                        break;
                    case Defines.BuildingType.Quarry_Red:
                        CharacterManager.Instance.AddItem(CreateTempItem(Defines.ResourceType.Ore));
                        break;
                    case Defines.BuildingType.Market_Red:
                        MakeProduct();
                        break;
                    default:
                        break;
                }
            }
            localTimer = 0;
        }
    }

    private ItemSO CreateTempItem(Defines.ResourceType type)
    {
        ItemSO temp = new ItemSO();
        temp.itemType = Defines.ItemType.Resource;
        temp.resourceType = type;
        return temp;
    }
    private void MakeProduct()
    {
        for (int i = 0; i < building.buildedSO.ProductPrefabs.Length; i++)
        {
            GameObject obj = Instantiate(building.buildedSO.ProductPrefabs[i], transform);
            float axisX = Random.Range(-1f, 1f);
            float axisZ = Random.Range(-1f, 1f);
            obj.transform.position = this.gameObject.transform.position;
            Vector3 dir = ((new Vector3(axisX, 3f, axisZ) + transform.position) - transform.position).normalized;

            rigid = obj.GetComponent<Rigidbody>();
            rigid.AddForce(dir * 5f, ForceMode.Impulse);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerProjectile") && building?.buildedSO?.buildType == Defines.BuildType.NaturalObject)
        {
            SoundManager.Instance.PlayOneShotPoint("HitResource", transform.position);
            MakeProduct();
        }
    }
}
