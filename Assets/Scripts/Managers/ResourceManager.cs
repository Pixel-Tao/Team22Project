using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : Singleton<ResourceManager>
{
    private Dictionary<string, GameObject> prefabDict = new Dictionary<string, GameObject>();

    private readonly string _prefabDir = "Prefabs/";

    public void Init()
    {
        prefabDict.Clear();
        GameObject[] prefabs = Resources.LoadAll<GameObject>(_prefabDir);
        for (int i = 0; i < prefabs.Length; i++)
        {
            // Class 이름 = Prefab 이름 완전 동일해야함
            prefabDict.Add(prefabs[i].name, prefabs[i]);
        }
    }

    public void Generate<T>() where T : Object
    {

    }

    public T Load<T>(string path) where T : Object
    {
        return null;
    }
}
