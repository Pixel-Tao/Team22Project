using Defines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

    public static string BuildingEnumToPrefabPath(Defines.BuildingType buildingType, bool withoutPrefix = false)
    {
        return BuildingEnumToPath(buildingType, true, withoutPrefix);
    }
    public static string BuildingEnumToSODataPath(Defines.BuildingType buildingType, bool withoutPrefix = false)
    {
        return BuildingEnumToPath(buildingType, false, withoutPrefix);
    }
    public static string BuildingEnumToPath(Defines.BuildingType buildingType, bool isPrefab, bool withoutPrefix = false)
    {
        string prefabPrefix = isPrefab ? "Prefabs/Buildings/" : "SO_Datas/Buildings/";

        switch (buildingType)
        {
            //기본 배치되는 성
            case Defines.BuildingType.Castle:
                return $"{(withoutPrefix ? string.Empty : prefabPrefix)}Construction/{(isPrefab ? string.Empty : "SO_")}Castle_Red";

            //플레이어가 건설할 목록
            case Defines.BuildingType.House:
                return $"{(withoutPrefix ? string.Empty : prefabPrefix)}Construction/{(isPrefab ? string.Empty : "SO_")}House_A_Red";
            case Defines.BuildingType.Tower:
                return $"{(withoutPrefix ? string.Empty : prefabPrefix)}Construction/{(isPrefab ? string.Empty : "SO_")}Tower_A_Red";
            case Defines.BuildingType.Wall_Straight:
                return $"{(withoutPrefix ? string.Empty : prefabPrefix)}Construction/{(isPrefab ? string.Empty : "SO_")}Wall_Straight";
            case Defines.BuildingType.Wall_Corner_A_Inside:
                return $"{(withoutPrefix ? string.Empty : prefabPrefix)}Construction/{(isPrefab ? string.Empty : "SO_")}Wall_Corner_A_Inside";
            case Defines.BuildingType.Wall_Corner_A_Outside:
                return $"{(withoutPrefix ? string.Empty : prefabPrefix)}Construction/{(isPrefab ? string.Empty : "SO_")}Wall_Corner_A_Outside";
            case Defines.BuildingType.Wall_Corner_B_Inside:
                return $"{(withoutPrefix ? string.Empty : prefabPrefix)}Construction/{(isPrefab ? string.Empty : "SO_")}Wall_Corner_B_Inside";
            case Defines.BuildingType.Wall_Corner_B_Outside:
                return $"{(withoutPrefix ? string.Empty : prefabPrefix)}Construction/{(isPrefab ? string.Empty : "SO_")}Wall_Corner_B_Outside";
            case Defines.BuildingType.Wall_Straight_Gate:
                return $"{(withoutPrefix ? string.Empty : prefabPrefix)}Construction/{(isPrefab ? string.Empty : "SO_")}Wall_Straight_Gate";
            case Defines.BuildingType.Wall_Corner_A_Gate:
                return $"{(withoutPrefix ? string.Empty : prefabPrefix)}Construction/{(isPrefab ? string.Empty : "SO_")}Wall_Corner_A_Gate";

            case Defines.BuildingType.Bridge:
                return $"{(withoutPrefix ? string.Empty : prefabPrefix)}Construction/{(isPrefab ? string.Empty : "SO_")}Bridge";
            case Defines.BuildingType.Market:
                return $"{(withoutPrefix ? string.Empty : prefabPrefix)}Construction/{(isPrefab ? string.Empty : "SO_")}Market";
            case Defines.BuildingType.Blacksmith:
                return $"{(withoutPrefix ? string.Empty : prefabPrefix)}Construction/{(isPrefab ? string.Empty : "SO_")}Blacksmith";

            case Defines.BuildingType.Windmill:
                return $"{(withoutPrefix ? string.Empty : prefabPrefix)}Construction/{(isPrefab ? string.Empty : "SO_")}Windmill_Red";
            case Defines.BuildingType.Lumbermill:
                return $"{(withoutPrefix ? string.Empty : prefabPrefix)}Construction/{(isPrefab ? string.Empty : "SO_")}Lumbermill_Red";
            case Defines.BuildingType.Quarry:
                return $"{(withoutPrefix ? string.Empty : prefabPrefix)}Construction/{(isPrefab ? string.Empty : "SO_")}Quarry_Red";
            case Defines.BuildingType.Watermill:
                return $"{(withoutPrefix ? string.Empty : prefabPrefix)}Construction/{(isPrefab ? string.Empty : "SO_")}Watermill_Red";


            //천연 자원생성지역
            case Defines.BuildingType.GrainLand:
                return $"{(withoutPrefix ? string.Empty : prefabPrefix)}NaturalObjects/{(isPrefab ? string.Empty : "SO_")}GrainLand";
            case Defines.BuildingType.LoggingArea_A:
                return $"{(withoutPrefix ? string.Empty : prefabPrefix)}NaturalObjects/{(isPrefab ? string.Empty : "SO_")}LoggingArea_A";
            case Defines.BuildingType.LoggingArea_B:
                return $"{(withoutPrefix ? string.Empty : prefabPrefix)}NaturalObjects/{(isPrefab ? string.Empty : "SO_")}LoggingArea_B";
            case Defines.BuildingType.MiningArea_A:
                return $"{(withoutPrefix ? string.Empty : prefabPrefix)}NaturalObjects/{(isPrefab ? string.Empty : "SO_")}MiningArea_A";
            case Defines.BuildingType.MiningArea_B:
                return $"{(withoutPrefix ? string.Empty : prefabPrefix)}NaturalObjects/{(isPrefab ? string.Empty : "SO_")}MiningArea_B";
            case Defines.BuildingType.MiningArea_C:
                return $"{(withoutPrefix ? string.Empty : prefabPrefix)}NaturalObjects/{(isPrefab ? string.Empty : "SO_")}MiningArea_C";
            case Defines.BuildingType.Well:
                return $"{(withoutPrefix ? string.Empty : prefabPrefix)}NaturalObjects/{(isPrefab ? string.Empty : "SO_")}Well";

            //자연환경
            case Defines.BuildingType.Dirt:
                return $"{(withoutPrefix ? string.Empty : prefabPrefix)}NaturalObjects/{(isPrefab ? string.Empty : "SO_")}Environment/Dirt";
            case Defines.BuildingType.Mountain_A:
                return $"{(withoutPrefix ? string.Empty : prefabPrefix)}NaturalObjects/{(isPrefab ? string.Empty : "SO_")}Environment/Mountain_A";
            case Defines.BuildingType.Mountain_A_Grass:
                return $"{(withoutPrefix ? string.Empty : prefabPrefix)}NaturalObjects/{(isPrefab ? string.Empty : "SO_")}Environment/Mountain_A_Grass";
            case Defines.BuildingType.Mountain_A_Grass_Trees:
                return $"{(withoutPrefix ? string.Empty : prefabPrefix)}NaturalObjects/{(isPrefab ? string.Empty : "SO_")}Environment/Mountain_A_Grass_Trees";
            case Defines.BuildingType.Mountain_B:
                return $"{(withoutPrefix ? string.Empty : prefabPrefix)}NaturalObjects/{(isPrefab ? string.Empty : "SO_")}Environment/Mountain_B";
            case Defines.BuildingType.Mountain_B_Grass:
                return $"{(withoutPrefix ? string.Empty : prefabPrefix)}NaturalObjects/{(isPrefab ? string.Empty : "SO_")}Environment/Mountain_B_Grass";
            case Defines.BuildingType.Mountain_B_Grass_Trees:
                return $"{(withoutPrefix ? string.Empty : prefabPrefix)}NaturalObjects/{(isPrefab ? string.Empty : "SO_")}Environment/Mountain_B_Grass_Trees";
            case Defines.BuildingType.Mountain_C:
                return $"{(withoutPrefix ? string.Empty : prefabPrefix)}NaturalObjects/{(isPrefab ? string.Empty : "SO_")}Environment/Mountain_C";
            case Defines.BuildingType.Mountain_C_Grass:
                return $"{(withoutPrefix ? string.Empty : prefabPrefix)}NaturalObjects/{(isPrefab ? string.Empty : "SO_")}Environment/Mountain_C_Grass";
            case Defines.BuildingType.Mountain_C_Grass_Trees:
                return $"{(withoutPrefix ? string.Empty : prefabPrefix)}NaturalObjects/{(isPrefab ? string.Empty : "SO_")}Environment/Mountain_C_Grass_Trees";

        }

        return null;
    }


    public static bool IsResourceBuildable(Defines.BuildingType wantType, Defines.BuildingType targetType)
    {
        switch (targetType)
        {
            case Defines.BuildingType.GrainLand:
                return wantType == Defines.BuildingType.Windmill;
            case Defines.BuildingType.LoggingArea_A:
            case Defines.BuildingType.LoggingArea_B:
                return wantType == Defines.BuildingType.Lumbermill;
            case Defines.BuildingType.MiningArea_A:
            case Defines.BuildingType.MiningArea_B:
            case Defines.BuildingType.MiningArea_C:
                return wantType == Defines.BuildingType.Quarry;
            case Defines.BuildingType.Well:
                return wantType == Defines.BuildingType.Watermill;
        }

        return false;
    }
    public static bool IsGroundBuildabe(Defines.BuildingType targetType)
    {
        switch (targetType)
        {
            case Defines.BuildingType.House:
            case Defines.BuildingType.Tower:
            case Defines.BuildingType.Bridge:
            case Defines.BuildingType.Market:
            case Defines.BuildingType.Blacksmith:
            case Defines.BuildingType.Wall_Straight:
            case Defines.BuildingType.Wall_Corner_A_Inside:
            case Defines.BuildingType.Wall_Corner_A_Outside:
            case Defines.BuildingType.Wall_Corner_B_Inside:
            case Defines.BuildingType.Wall_Corner_B_Outside:
            case Defines.BuildingType.Wall_Straight_Gate:
            case Defines.BuildingType.Wall_Corner_A_Gate:
                return true;
        }

        return false;
    }
    public static bool IsNaturalResource(Defines.BuildingType targetType)
    {
        switch (targetType)
        {
            case Defines.BuildingType.GrainLand:
            case Defines.BuildingType.LoggingArea_A:
            case Defines.BuildingType.LoggingArea_B:
            case Defines.BuildingType.MiningArea_A:
            case Defines.BuildingType.MiningArea_B:
            case Defines.BuildingType.MiningArea_C:
            case Defines.BuildingType.Well:
                return true;
        }

        return false;
    }

    public static Defines.BuildingType OriginResourceTypeToBuildingType(Defines.BuildingType targetType)
    {
        switch (targetType)
        {
            case Defines.BuildingType.GrainLand:
                return Defines.BuildingType.Windmill;
            case Defines.BuildingType.LoggingArea_A:
            case Defines.BuildingType.LoggingArea_B:
                return Defines.BuildingType.Lumbermill;
            case Defines.BuildingType.MiningArea_A:
            case Defines.BuildingType.MiningArea_B:
            case Defines.BuildingType.MiningArea_C:
                return Defines.BuildingType.Quarry;
            case Defines.BuildingType.Well:
                return Defines.BuildingType.Watermill;
        }

        return Defines.BuildingType.None;
    }

    public static Defines.BuildingType BuidingTypeToOriginResourceType(Defines.BuildingType buildingType)
    {
        switch (buildingType)
        {
            case Defines.BuildingType.Windmill:
                return Defines.BuildingType.GrainLand;
            case Defines.BuildingType.Lumbermill:
                return Defines.BuildingType.LoggingArea_A;
            case Defines.BuildingType.Quarry:
                return Defines.BuildingType.MiningArea_A;
            case Defines.BuildingType.Watermill:
                return Defines.BuildingType.Well;
        }

        return Defines.BuildingType.None;
    }
}