using UnityEngine;

// Method 4: Create map when having player and exit transform
// Use recursive method to spawn tiles from player position to exit position
// Use another method to check if the map has a valid path
// After create a valid path, spawn other tiles

public class MapSpawner : AMapSpawner
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform exitTransform;

    protected override void SpawnTiles()
    {
        OnMapRegenerated += SpawnOtherTiles;
        var p = GetPlayerPosition();
        SpawnRecursive(p.x, p.y);
    }

    protected override bool HasValidPath()
    {
        // If exit position is not null, return true
        var exitPos = GetExitPosition();
        return mapTiles[exitPos.x, exitPos.y];
    }

    protected override Vector2Int GetPlayerPosition()
    {
        // Get player position from player transform
        Vector2 p = playerTransform.position;
        int s = mapSO.tileSize;
        return PositionConverter.GetPoint(p, s);
    }

    protected override Vector2Int GetExitPosition()
    {
        // Get exit position from exit transform
        Vector2 p = exitTransform.position;
        int s = mapSO.tileSize;
        return PositionConverter.GetPoint(p, s);
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
}