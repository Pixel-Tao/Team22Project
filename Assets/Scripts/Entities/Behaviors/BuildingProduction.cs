using UnityEditor.Search;
using UnityEngine;

public class BuildingProduction : MonoBehaviour
{
    BuildSO data;
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
        data = gameObject.GetComponent<BuildingObject>().buildedSO;
        delay = data.ProductiontDelay;
    }

    private void Update()
    {
        localTimer += Time.deltaTime;
        if (localTimer > delay)
        {
            switch (data.buildingType)
            {
                case Defines.BuildingType.Windmill:
                    CharacterManager.Instance.AddItem(CreateTempItem(Defines.ResourceType.Food));
                    break;
                case Defines.BuildingType.Lumbermill:
                    CharacterManager.Instance.AddItem(CreateTempItem(Defines.ResourceType.Wood));
                    break;
                case Defines.BuildingType.Quarry:
                    CharacterManager.Instance.AddItem(CreateTempItem(Defines.ResourceType.Ore));
                    break;
                case Defines.BuildingType.Market:
                    MakeProduct();
                    break;
                default:
                    break;
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
        for (int i = 0; i < data.ProductPrefabs.Length; i++)
        {
            GameObject obj = Instantiate(data.ProductPrefabs[i],transform);
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
        if(other.CompareTag("PlayerProjectile"))
        {
            SoundManager.Instance.PlayOneShotPoint("HitResource", transform.position);
            MakeProduct();
        }
    }
}
