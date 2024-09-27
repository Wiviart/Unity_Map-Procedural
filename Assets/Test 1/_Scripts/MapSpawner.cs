using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}