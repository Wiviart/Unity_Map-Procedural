using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSpawner : AMapSpawner
{
    protected override void SpawnTiles()
    {
        SpawnRecursive(0, 0);
    }

    protected override void FunctionAfterValidPath()
    {
        SpawnOtherTiles();
    }

    protected override bool HasValidPath()
    {
        Vector2Int exitPos = GetExitPosition();

        return mapTiles[exitPos.x, exitPos.y] != null;
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
