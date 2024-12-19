using UnityEngine;

// Method 3: Create map when having player and exit position
// Use recursive method to spawn tiles from player position to exit position
// Use another method to check if the map has a valid path
// After create a valid path, spawn other tiles

public class MapSpawner : AMapSpawner
{
    protected override void SpawnTiles()
    {
        OnMapRegenerated += SpawnOtherTiles;
        SpawnRecursive(0, 0);
    }

    protected override bool HasValidPath()
    {
        // If exit position is not null, return true
        var exitPos = GetExitPosition();
        return mapTiles[exitPos.x, exitPos.y];
    }

    private void SpawnRecursive(int x, int y)
    {
        if (x < 0 || x >= mapSO.mapSize.x
         || y < 0 || y >= mapSO.mapSize.y) return;

        if (mapTiles[x, y]) return;

        if (!tileSpawner.SpawnTile(x, y)) return;

        if (mapTiles[x, y].GetDirections().left == 1)
            SpawnRecursive(x - 1, y);

        if (mapTiles[x, y].GetDirections().right == 1)
            SpawnRecursive(x + 1, y);

        if (mapTiles[x, y].GetDirections().down == 1)
            SpawnRecursive(x, y - 1);

        if (mapTiles[x, y].GetDirections().up == 1)
            SpawnRecursive(x, y + 1);
    }

    private void SpawnOtherTiles()
    {
        for (int x = 0; x < mapSO.mapSize.x; x++)
        {
            for (int y = 0; y < mapSO.mapSize.y; y++)
            {
                if (mapTiles[x, y]) continue;
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
