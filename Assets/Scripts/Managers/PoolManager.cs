using Defines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : Singleton<PoolManager>
{
    private Dictionary<string, ObjectPool> poolDict = new Dictionary<string, ObjectPool>();

    private int defaultCapacity = 10;
    private int maxSize = 100;

    private GameObject root;
    public GameObject Root
    {
        get
        {
            if (root == null)
            {
                root = GameObject.Find("Pool_Root");
                if (root == null)
                    root = new GameObject { name = "Pool_Root" };
            }
            return root;
        }
    }

    public void Init(int defaultCapacity = 10, int maxSize = 100)
    {
        Debug.Log("PoolManager Init");
        this.defaultCapacity = defaultCapacity;
        this.maxSize = maxSize;
    }

    private Transform GetTypeRoot(string name)
    {
        Transform buildingRoot = Root.transform.Find(name);
        if (buildingRoot == null)
        {
            buildingRoot = new GameObject { name = name }.transform;
            buildingRoot.SetParent(Root.transform, false);
        }

        return buildingRoot;
    }

    private ObjectPool CreatePool(string path, Transform parent)
    {
        GameObject go = Resources.Load<GameObject>(path);
        ObjectPool pool = new ObjectPool(path, go, parent, defaultCapacity, maxSize);
        poolDict.Add(path, pool);
        return pool;
    }

    public GameObject Spawn(string path, Transform parent = null)
    {
        if (!poolDict.TryGetValue(path, out ObjectPool pool))
        {
            pool = CreatePool(path, parent);
        }

        return pool.Spawn();
    }
    public GameObject SpawnBuilding(BuildingType type, Transform parent = null)
    {
        GameObject go = Spawn($"Prefabs/Buildings/Construction/{type}", parent == null ? GetTypeRoot("Buildings") : parent);
        return go;
    }
    public GameObject SpawnMonster(MOBTYPE type, Transform parent = null)
    {
        GameObject go = Spawn($"Prefabs/Monsters/{type}", parent == null ? GetTypeRoot("Monsters") : parent);
        return go;
    }
    public GameObject SpawnItem(string name, Transform parent = null)
    {
        GameObject go = Spawn($"Prefabs/Item/{name}", parent == null ? GetTypeRoot("Items") : parent);
        return go;
    }
    public GameObject SpawnProjectile(string name, Transform parent = null)
    {
        GameObject go = Spawn($"Prefabs/Projectile/{name}", parent == null ? GetTypeRoot("Projectiles") : parent);
        return go;
    }



    public void Despawn(GameObject go)
    {
        if (poolDict.TryGetValue(go.name, out ObjectPool pool))
        {
            pool.Despawn(go);
        }
        else
        {
            Destroy(go);
        }
    }
}
