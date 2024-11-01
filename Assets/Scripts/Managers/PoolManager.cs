using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : Singleton<PoolManager>
{
    private Dictionary<string, ObjectPool> poolDict = new Dictionary<string, ObjectPool>();

    private int defaultCapacity = 10;
    private int maxSize = 100;

    private ObjectPool CreatePool(string path, Transform parent)
    {
        GameObject go = Resources.Load<GameObject>(path);
        ObjectPool pool = new ObjectPool(name, go, parent, defaultCapacity, maxSize);
        poolDict.Add(name, pool);
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
    public GameObject SpawnMonster(string name, Transform parent = null)
    {
        return Spawn($"Prefabs/Monsters/{name}", parent);
    }

    public GameObject SpawnItem(string name, Transform parent = null)
    {
        return Spawn($"Prefabs/Items/{name}", parent);
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

    public void Init(int defaultCapacity = 10, int maxSize = 100)
    {
        Debug.Log("PoolManager Init");
        this.defaultCapacity = defaultCapacity;
        this.maxSize = maxSize;
    }
}
