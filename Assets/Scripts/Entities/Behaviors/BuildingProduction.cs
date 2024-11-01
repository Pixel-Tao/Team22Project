using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BuildingProduction : MonoBehaviour
{
    BuildSO data;
    GameObject productObject;
    private float delay;
    private float lastTime;
    Rigidbody rigid;

    private void Start()
    {
        Init();
    }


    private void Init()
    {
        data = gameObject.GetComponent<BuildingObject>().buildedSO;
        delay = data.ProductiontDelay;
        InvokeRepeating("MakeProduct", 0, delay);
    }
    private void MakeProduct()
    {
        for (int i = 0; i < data.ProductPrefabs.Length; i++)
        {
            GameObject obj = Instantiate(data.ProductPrefabs[i],transform);
            obj.transform.localPosition = Vector3.zero;

            rigid = obj.GetComponent<Rigidbody>();
            rigid.AddForce(Vector3.up * 4, ForceMode.Impulse);
        }
    }
}
