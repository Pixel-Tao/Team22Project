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
        if (other.CompareTag("PlayerProjectile"))
        {
            SoundManager.Instance.PlayOneShotPoint("HitResource", transform.position);
            MakeProduct();
        }
    }
}
