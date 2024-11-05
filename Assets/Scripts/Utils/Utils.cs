using Defines;
using UnityEngine;

public class Utils
{
    public static T GetOrAddComponent<T>(GameObject go) where T : Component
    {
        T component = go.GetComponent<T>();
        if (component == null)
            component = go.AddComponent<T>();

        return component;
    }

    public static string BuildingEnumToPrefabPath(BuildingType type, bool withoutPrefix = false)
    {
        return BuildingEnumToPath(type, true, withoutPrefix);
    }
    public static string BuildingEnumToSODataPath(BuildingType type, bool withoutPrefix = false)
    {
        return BuildingEnumToPath(type, false, withoutPrefix);
    }
    public static string BuildingEnumToPrefabPath(NaturalObjectType type, bool withoutPrefix = false)
    {
        return BuildingEnumToPath(type, true, withoutPrefix);
    }
    public static string BuildingEnumToSODataPath(NaturalObjectType type, bool withoutPrefix = false)
    {
        return BuildingEnumToPath(type, false, withoutPrefix);
    }
    public static string BuildingEnumToPrefabPath(EnvironmentType type, bool withoutPrefix = false)
    {
        return BuildingEnumToPath(type, true, withoutPrefix);
    }
    public static string BuildingEnumToSODataPath(EnvironmentType type, bool withoutPrefix = false)
    {
        return BuildingEnumToPath(type, false, withoutPrefix);
    }

    public static string BuildingEnumToPath(BuildingType buildingType, bool isPrefab, bool withoutPrefix = false)
    {
        string prefabPrefix = isPrefab ? "Prefabs/Buildings/" : "SO_Datas/Buildings/";
        string rst = $"{(withoutPrefix ? string.Empty : prefabPrefix)}Construction/{(isPrefab ? string.Empty : "SO_")}{buildingType}";
        Debug.Log(rst);
        return rst;
    }
    public static string BuildingEnumToPath(NaturalObjectType naturalType, bool isPrefab, bool withoutPrefix = false)
    {
        string prefabPrefix = isPrefab ? "Prefabs/Buildings/" : "SO_Datas/Buildings/";
        string rst = $"{(withoutPrefix ? string.Empty : prefabPrefix)}NaturalObjects/{(isPrefab ? string.Empty : "SO_")}{naturalType}";
        Debug.Log(rst);
        return rst;
    }
    public static string BuildingEnumToPath(EnvironmentType envType, bool isPrefab, bool withoutPrefix = false)
    {
        string prefabPrefix = isPrefab ? "Prefabs/Buildings/" : "SO_Datas/Buildings/";
        string rst = $"{(withoutPrefix ? string.Empty : prefabPrefix)}Environment/{(isPrefab ? string.Empty : "SO_")}{envType}";
        Debug.Log(rst);
        return rst;
    }

}