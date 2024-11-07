using UnityEngine;

[CreateAssetMenu(fileName = "SO_TileData", menuName = "Datas/SO_TileData")]
public class TileSO : InteractableSO
{
    public Defines.TileType tileType;

    [Header("해당 타일로부터 드랍되는 목록")]
    [SerializeField] private GameObject[] dropPrefabs;
}
