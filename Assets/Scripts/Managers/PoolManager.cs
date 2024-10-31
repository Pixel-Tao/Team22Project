using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : Singleton<PoolManager>
{
    Dictionary<string, ObjectPool> poolDict = new Dictionary<string, ObjectPool>();

    private ObjectPool CreatePool()
    {
        return null;
    }

    public void Spawn(string name)
    {

    }

    public void Despawn(GameObject go)
    {

    }
}
