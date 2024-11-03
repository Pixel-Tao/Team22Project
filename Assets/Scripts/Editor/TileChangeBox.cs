using Defines;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TileObject))]
public class TileChangeBox : Editor
{
    int index;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (!Application.isPlaying)
        {
            string[] buidings = System.Enum.GetNames(typeof(Defines.BuildingType));

            GUILayout.Space(20);
            for (int i = 0; i < buidings.Length; i++)
            {
                Defines.BuildingType buidingType = (Defines.BuildingType)i;
                if (buidingType == Defines.BuildingType.None)
                {
                    if (GUILayout.Button($"Clear"))
                    {
                        Debug.Log($"Change {buidingType}");
                        TileObject tile = target as TileObject;
                        if (tile.building != null)
                        {
                            Destroy(tile.building);
                            tile.building = null;
                        }
                    }
                }
                else
                {
                    if (GUILayout.Button($"{buidingType} Build"))
                    {
                        Debug.Log($"Change {buidingType}");
                        GameObject prefab = GetConstruct(buidingType);
                        if (prefab == null) return;
                        TileObject tile = target as TileObject;
                        GameObject go = Instantiate(prefab, tile.transform.position, Quaternion.identity);
                        go.transform.SetParent(tile.transform);
                        tile.building = go.GetComponent<BuildingObject>();
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
            case Defines.BuildingType.Castle:
                return Resources.Load<GameObject>("Prefabs/Bulidings/Castle_Red");
            case Defines.BuildingType.House:
                return Resources.Load<GameObject>("Prefabs/Bulidings/House_A_Red");
            case Defines.BuildingType.Tower:
                return Resources.Load<GameObject>("Prefabs/Bulidings/Tower_A_Red");
        }

        return null;
    }
}
