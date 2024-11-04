using Defines;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TileObject))]
public class TileChangeBox : Editor
{
    int index;
    GameObject go = null;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (!Application.isPlaying)
        {
            string[] buildings = System.Enum.GetNames(typeof(Defines.BuildingType));

            GUILayout.Space(20);
            for (int i = 0; i < buildings.Length; i++)
            {
                Defines.BuildingType buidingType = (Defines.BuildingType)i;
                if (buidingType == Defines.BuildingType.None)
                {
                    if (GUILayout.Button($"Clear"))
                    {
                        TileObject tile = target as TileObject;
                        if (tile.building != null)
                        {
                            DestroyImmediate(tile.building.gameObject);
                            tile.building = null;
                        }
                    }
                }
                else
                {
                    if (GUILayout.Button($"{buidingType} Build"))
                    {
                        GameObject prefab = GetConstruct(buidingType);
                        

                        if (prefab == null)
                        {
                            Debug.Log("Debug : 프리팹을 찾지못함 스위치 케이스를 확인하시오.");
                            return;
                        }
                        if (go != null)
                        {
                            Debug.Log($"만든적 있음 : 건물은 타일에 하나만");
                        }
                        else if (go == null)
                        {
                            Debug.Log($"만든적 없음");
                            TileObject tile = target as TileObject;

                            GameObject InstanceParent = GameObject.Find(tile.gameObject.transform.parent.name + "AddOn");

                            go = Instantiate(prefab, tile.transform.position, Quaternion.identity);
                            go.transform.SetParent(InstanceParent.transform);
                            
                            tile.building = go.GetComponent<BuildingObject>();
                        }
                        
                    }
                }
            }
            GUILayout.Space(20);
            if (GUILayout.Button($"건물 좌 회전"))
            {
                TileObject tile = target as TileObject;
                if (tile.building != null)
                    tile.building.transform.Rotate(0, -60, 0);
            }
            if (GUILayout.Button($"건물 우 회전"))
            {
                TileObject tile = target as TileObject;
                if (tile.building != null)
                    tile.building.transform.Rotate(0, 60, 0);
            }
        }
    }

    public GameObject GetConstruct(Defines.BuildingType buidingType)
    {
        switch (buidingType)
        {
            //기본 배치되는 성
            case Defines.BuildingType.Castle:
                return Resources.Load<GameObject>("Prefabs/Buildings/Construction/Castle_Red");

            //플레이어가 건설할 목록
            case Defines.BuildingType.House:
                return Resources.Load<GameObject>("Prefabs/Buildings/Construction/House_A_Red");
            case Defines.BuildingType.Tower:
                return Resources.Load<GameObject>("Prefabs/Buildings/Construction/Tower_A_Red");
            case Defines.BuildingType.Wall_Straight:
                return Resources.Load<GameObject>("Prefabs/Buildings/Construction/Wall_Straight");
            case Defines.BuildingType.Wall_Corner_A_Inside:
                return Resources.Load<GameObject>("Prefabs/Buildings/Construction/Wall_Corner_A_Inside");
            case Defines.BuildingType.Wall_Corner_A_Outside:
                return Resources.Load<GameObject>("Prefabs/Buildings/Construction/Wall_Corner_A_Outside");
            case Defines.BuildingType.Wall_Corner_B_Inside:
                return Resources.Load<GameObject>("Prefabs/Buildings/Construction/Wall_Corner_B_Inside");
            case Defines.BuildingType.Wall_Corner_B_Outside:
                return Resources.Load<GameObject>("Prefabs/Buildings/Construction/Wall_Corner_B_Outside");
            case Defines.BuildingType.Wall_Straight_Gate:
                return Resources.Load<GameObject>("Prefabs/Buildings/Construction/Wall_Straight_Gate");
            case Defines.BuildingType.Wall_Corner_A_Gate:
                return Resources.Load<GameObject>("Prefabs/Buildings/Construction/Wall_Corner_A_Gate");

            case Defines.BuildingType.Bridge:
                return Resources.Load<GameObject>("Prefabs/Buildings/Construction/Bridge");
            case Defines.BuildingType.Market:
                return Resources.Load<GameObject>("Prefabs/Buildings/Construction/Market");
            case Defines.BuildingType.Blacksmith:
                return Resources.Load<GameObject>("Prefabs/Buildings/Construction/Blacksmith");

            case Defines.BuildingType.Windmill:
                return Resources.Load<GameObject>("Prefabs/Buildings/Construction/Windmill_Red");
            case Defines.BuildingType.Lumbermill:
                return Resources.Load<GameObject>("Prefabs/Buildings/Construction/Lumbermill_Red");
            case Defines.BuildingType.Quarry:
                return Resources.Load<GameObject>("Prefabs/Buildings/Construction/Quarry_Red");
            case Defines.BuildingType.Watermill:
                return Resources.Load<GameObject>("Prefabs/Buildings/Construction/Watermill_Red");


            //천연 자원생성지역
            case Defines.BuildingType.GrainLand:
                return Resources.Load<GameObject>("Prefabs/Buildings/NaturalObjects/GrainLand");
            case Defines.BuildingType.LoggingArea_A:
                return Resources.Load<GameObject>("Prefabs/Buildings/NaturalObjects/LoggingArea_A");
            case Defines.BuildingType.LoggingArea_B:
                return Resources.Load<GameObject>("Prefabs/Buildings/NaturalObjects/LoggingArea_B");
            case Defines.BuildingType.MiningArea_A:
                return Resources.Load<GameObject>("Prefabs/Buildings/NaturalObjects/MiningArea_A");
            case Defines.BuildingType.MiningArea_B:
                return Resources.Load<GameObject>("Prefabs/Buildings/NaturalObjects/MiningArea_B");
            case Defines.BuildingType.MiningArea_C:
                return Resources.Load<GameObject>("Prefabs/Buildings/NaturalObjects/MiningArea_C");
            case Defines.BuildingType.Well:
                return Resources.Load<GameObject>("Prefabs/Buildings/NaturalObjects/Well");

            //자연환경
            case Defines.BuildingType.Dirt:
                return Resources.Load<GameObject>("Prefabs/Buildings/NaturalObjects/Environment/Dirt");
            case Defines.BuildingType.Mountain_A:
                return Resources.Load<GameObject>("Prefabs/Buildings/NaturalObjects/Environment/Mountain_A");
            case Defines.BuildingType.Mountain_A_Grass:
                return Resources.Load<GameObject>("Prefabs/Buildings/NaturalObjects/Environment/Mountain_A_Grass");
            case Defines.BuildingType.Mountain_A_Grass_Trees:
                return Resources.Load<GameObject>("Prefabs/Buildings/NaturalObjects/Environment/Mountain_A_Grass_Trees");
            case Defines.BuildingType.Mountain_B:
                return Resources.Load<GameObject>("Prefabs/Buildings/NaturalObjects/Environment/Mountain_B");
            case Defines.BuildingType.Mountain_B_Grass:
                return Resources.Load<GameObject>("Prefabs/Buildings/NaturalObjects/Environment/Mountain_B_Grass");
            case Defines.BuildingType.Mountain_B_Grass_Trees:
                return Resources.Load<GameObject>("Prefabs/Buildings/NaturalObjects/Environment/Mountain_B_Grass_Trees");
            case Defines.BuildingType.Mountain_C:
                return Resources.Load<GameObject>("Prefabs/Buildings/NaturalObjects/Environment/Mountain_C");
            case Defines.BuildingType.Mountain_C_Grass:
                return Resources.Load<GameObject>("Prefabs/Buildings/NaturalObjects/Environment/Mountain_C_Grass");
            case Defines.BuildingType.Mountain_C_Grass_Trees:
                return Resources.Load<GameObject>("Prefabs/Buildings/NaturalObjects/Environment/Mountain_C_Grass_Trees");

        }

        return null;
    }
}
