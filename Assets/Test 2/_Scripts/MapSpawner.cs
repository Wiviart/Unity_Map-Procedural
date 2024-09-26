using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSpawner : MonoBehaviour
{
    [SerializeField] private List<MapTile> mapTilePrefabs;
    const float size = 10;
    const int mapSizeX = 10, mapSizeY = 10;
    MapTile[,] mapTiles = new MapTile[mapSizeX, mapSizeY];

    void Start()
    {
        SpawnRecursive(0, 0);
        StartCoroutine(CheckValidPath());
    }

    void SpawnRecursive(int x, int y)
    {
        if (x < 0 || x >= mapSizeX || y < 0 || y >= mapSizeY) return;

        if (mapTiles[x, y]) return;

        TileSpawner tileSpawner = new(transform, size, mapTiles, mapSizeX, mapSizeY, mapTilePrefabs);

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

    IEnumerator CheckValidPath()
    {
        PathFinder pathFinder = new PathFinder(mapTiles, mapSizeX, mapSizeY);
        Vector2Int playerStart = new Vector2Int(0, 0);
        Vector2Int exitPos = new Vector2Int(mapSizeX - 1, mapSizeY - 1);
        bool validPathExists = pathFinder.IsPathToExit(playerStart, exitPos);

        if (validPathExists)
        {
            SpawnOtherTiles();
            yield break;
        }

        StartCoroutine(RegenerateMap());
        yield return new WaitForEndOfFrame();
        StartCoroutine(CheckValidPath());
    }

    void DestroyTiles()
    {
        for (int x = 0; x < mapSizeX; x++)
        {
            for (int y = 0; y < mapSizeY; y++)
            {
                if (!mapTiles[x, y]) continue;
                Destroy(mapTiles[x, y].gameObject);
            }
        }
    }

    IEnumerator RegenerateMap()
    {
        Debug.Log("2 Regenerating map...");

        DestroyTiles();
        yield return new WaitForEndOfFrame();
        SpawnRecursive(0, 0);
    }

    private void SpawnOtherTiles()
    {
        TileSpawner tileSpawner = new(transform, size, mapTiles, mapSizeX, mapSizeY, mapTilePrefabs);

        for (int x = 0; x < mapSizeX; x++)
        {
            for (int y = 0; y < mapSizeY; y++)
            {
                if (mapTiles[x, y]) continue;
                tileSpawner.SpawnTile(x, y);
            }
        }
    }
}
