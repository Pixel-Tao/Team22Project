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
        return Resources.Load<GameObject>(Utils.BuildingEnumToPrefabPath(buidingType));
    }
}
