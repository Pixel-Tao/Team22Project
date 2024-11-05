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
        string rst = $"{(withoutPrefix ? string.Empty : prefabPrefix)}Construction/{(isPrefab ? string.Empty : "SO_")}Castle_Red";
        Debug.Log(rst);
        return rst;
    }
    public static string BuildingEnumToPath(NaturalObjectType naturalType, bool isPrefab, bool withoutPrefix = false)
    {
        string prefabPrefix = isPrefab ? "Prefabs/Buildings/" : "SO_Datas/Buildings/";
        string rst = $"{(withoutPrefix ? string.Empty : prefabPrefix)}Environment/{(isPrefab ? string.Empty : "SO_")}{naturalType}";
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

    public static BuildingType OriginResourceTypeToBuildingType(NaturalObjectType targetType)
    {
        switch (targetType)
        {
            case Defines.NaturalObjectType.GrainLand:
                return Defines.BuildingType.Windmill_Red;
            case Defines.NaturalObjectType.LoggingArea_A:
            case Defines.NaturalObjectType.LoggingArea_B:
                return Defines.BuildingType.Lumbermill_Red;
            case Defines.NaturalObjectType.MiningArea_A:
            case Defines.NaturalObjectType.MiningArea_B:
            case Defines.NaturalObjectType.MiningArea_C:
                return Defines.BuildingType.Quarry_Red;
            case Defines.NaturalObjectType.Well:
                return Defines.BuildingType.Watermill_Red;
        }

        return Defines.BuildingType.None;
    }
}