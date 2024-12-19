using UnityEngine;

// Method 1: Create map when having player and exit position
// If valid path doesn't exist, spawn a new map

public class MapSpawner : AMapSpawner
{
    protected override void SpawnTiles()
    {
        for (int x = 0; x < mapSO.mapSize.x; x++)
        {
            for (int y = 0; y < mapSO.mapSize.y; y++)
            {
                tileSpawner.SpawnTile(x, y);
            }
        }
    }

    protected override Vector2Int GetExitPosition()
    {
        int x = mapSO.mapSize.x - 1;
        int y = mapSO.mapSize.y - 1;
        return new Vector2Int(x, y);
    }
}