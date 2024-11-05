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
            CommonButtons();
            BuildingButtons();
            EnvButtons();
            NatualObjectButtons();
        }
    }
    private void CommonButtons()
    {
        GUILayout.Space(20);
        GUILayout.Label("공통 기능");
        if (GUILayout.Button($"Clear"))
        {
            TileObject tile = target as TileObject;
            if (tile.building != null)
            {
                DestroyImmediate(tile.building.gameObject);
                tile.building = null;
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
    private void BuildingButtons()
    {
        string[] buildings = System.Enum.GetNames(typeof(Defines.BuildingType));

        GUILayout.Space(20);
        GUILayout.Label("빌딩 건설");
        for (int i = 0; i < buildings.Length; i++)
        {
            Defines.BuildingType buidingType = (Defines.BuildingType)i;
            if (buidingType == Defines.BuildingType.None)
                continue;

            if (GUILayout.Button($"{buidingType} Build"))
            {
                GameObject prefab = Resources.Load<GameObject>(Utils.BuildingEnumToPrefabPath(buidingType));
                GenerateObject(prefab);
            }
        }

    }
    private void EnvButtons()
    {
        string[] envs = System.Enum.GetNames(typeof(Defines.EnvironmentType));

        GUILayout.Space(20);
        GUILayout.Label("환경 배치");
        for (int i = 0; i < envs.Length; i++)
        {
            Defines.EnvironmentType envType = (Defines.EnvironmentType)i;
            if (envType == Defines.EnvironmentType.None)
                continue;

            if (GUILayout.Button($"{envType} Build"))
            {
                GameObject prefab = Resources.Load<GameObject>(Utils.BuildingEnumToPrefabPath(envType));
                GenerateObject(prefab);
            }
        }
    }
    private void NatualObjectButtons()
    {
        string[] envs = System.Enum.GetNames(typeof(Defines.NaturalObjectType));

        GUILayout.Space(20);
        GUILayout.Label("자원생산물 배치");
        for (int i = 0; i < envs.Length; i++)
        {
            Defines.NaturalObjectType noType = (Defines.NaturalObjectType)i;
            if (noType == Defines.NaturalObjectType.None)
                continue;

            if (GUILayout.Button($"{noType} Build"))
            {
                GameObject prefab = Resources.Load<GameObject>(Utils.BuildingEnumToPrefabPath(noType));
                GenerateObject(prefab);
            }
        }
    }

    private void GenerateObject(GameObject prefab)
    {
        if (prefab == null)
        {
            Debug.Log("Debug : 프리팹을 찾지못함");
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
            GameObject instance = (GameObject)PrefabUtility.InstantiatePrefab(prefab);
            instance.transform.SetParent(InstanceParent.transform);
            instance.transform.position = tile.transform.position;

            tile.building = instance.GetComponent<BuildingObject>();
        }
    }
    
}
