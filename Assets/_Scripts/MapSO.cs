using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MapSO", menuName = "Scriptable Objects/MapSO", order = 1)]
public class MapSO : ScriptableObject
{
    [Header("Map Settings")]
    public Vector2Int mapSize = new Vector2Int(10, 10);
    public int tileSize = 10;

    [Header("Map Tiles")]
    public List<MapTile> mapTilePrefabs;
}