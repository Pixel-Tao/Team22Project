using Defines;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : Singleton<ResourceManager>
{
    private Dictionary<string, GameObject> prefabDict = new Dictionary<string, GameObject>();
    private Dictionary<string, Sprite> spriteDict = new Dictionary<string, Sprite>();
    private Dictionary<string, ScriptableObject> soDict = new Dictionary<string, ScriptableObject>();

    /// <summary>
    ///  파일 로드
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path"></param>
    /// <returns></returns>
    public T Load<T>(string path) where T : Object
    {
        if (typeof(T) == typeof(Sprite))
        {
            if (spriteDict.TryGetValue(path, out Sprite sprite))
                return sprite as T;

            Sprite sp = Resources.Load<Sprite>(path);
            spriteDict.Add(path, sp);
            return sp as T;
        }
        else if(typeof(T) == typeof(ScriptableObject))
        {
            if (soDict.TryGetValue(path, out ScriptableObject so))
                return so as T;

            ScriptableObject soData = Resources.Load<ScriptableObject>(path);
            soDict.Add(path, soData);
            return soData as T;
        }

        return Resources.Load<T>(path);
    }

    /// <summary>
    /// 게임오브젝트 인스턴스화
    /// </summary>
    /// <param name="path"></param>
    /// <param name="parent"></param>
    /// <returns></returns>
    public GameObject Instantiate(string path, Transform parent = null)
    {
        GameObject prefab = Load<GameObject>($"Prefabs/{path}");
        if (prefab == null)
        {
            Debug.Log($"Failed to load prefab : {path}");
            return null;
        }

        return Instantiate(prefab, parent);
    }

    /// <summary>
    /// 게임오브젝트 인스턴스화
    /// </summary>
    /// <param name="prefab"></param>
    /// <param name="parent"></param>
    /// <returns></returns>
    public GameObject Instantiate(GameObject prefab, Transform parent = null)
    {
        GameObject go = Object.Instantiate(prefab, parent);
        go.name = prefab.name;
        return go;
    }

    /// <summary>
    ///  SOData를 로드합니다.
    ///  직접 경로로 가져오는 경우에는 dataType 을 None으로 설정하고 path에 상대경로를 넣어주면 됩니다.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path">path or filename</param>
    /// <param name="dataType">enum 이름과 폴더 이름이 동일해야함</param>
    /// <param name="itemType">enum 이름과 폴더 이름이 동일해야함</param>
    /// <returns></returns>
    public T GetSOData<T>(string path, SODataType dataType = SODataType.None, SOItemDataType itemType = SOItemDataType.None) where T : ScriptableObject
    {
        if (dataType == SODataType.Item && itemType != SOItemDataType.None)
        {
            return Load<T>($"SO_Datas/{dataType}/{itemType}/{path}");
        }
        else if (dataType != SODataType.None)
        {
            return Load<T>($"SO_Datas/{dataType}/{path}");
        }

        return Load<T>($"SO_Datas/{path}");
    }

    public T GetSOBuildingData<T>(BuildingType buildingType) where T : ScriptableObject
    {
        string name = Utils.BuildingEnumToSODataPath(buildingType, true);
        return GetSOData<T>(name, SODataType.Buildings, SOItemDataType.None);
    }

    public T GetSOJobData<T>(JobType jobType) where T : ScriptableObject
    {
        return GetSOData<T>($"SO_{jobType}Data", SODataType.Job, SOItemDataType.None);
    }

    public T GetSOItemData<T>(string name, SOItemDataType itemType) where T : ScriptableObject
    {
        return GetSOData<T>(name, SODataType.Item, itemType);
    }

    public T GetSOMobData<T>(string name) where T : ScriptableObject
    {
        return GetSOData<T>(name, SODataType.MobData, SOItemDataType.None);
    }

    public T GetSOTileData<T>(string name) where T : ScriptableObject
    {
        return GetSOData<T>(name, SODataType.Tile, SOItemDataType.None);
    }
}
