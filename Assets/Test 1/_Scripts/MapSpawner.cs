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
        SpawnTiles();
        StartCoroutine(CheckValidPath());
    }

    IEnumerator CheckValidPath()
    {
        PathFinder pathFinder = new PathFinder(mapTiles, mapSizeX, mapSizeY);
        Vector2Int playerStart = new Vector2Int(0, 0);
        Vector2Int exitPos = new Vector2Int(mapSizeX - 1, mapSizeY - 1);
        bool validPathExists = pathFinder.IsPathToExit(playerStart, exitPos);

        if (validPathExists) yield break;

        StartCoroutine(RegenerateMap());
        yield return new WaitForEndOfFrame();
        StartCoroutine(CheckValidPath());
    }

    void SpawnTiles()
    {
        TileSpawner tileSpawner = new(transform, size, mapTiles, mapSizeX, mapSizeY, mapTilePrefabs);

        for (int x = 0; x < mapSizeX; x++)
        {
            for (int y = 0; y < mapSizeY; y++)
            {
                tileSpawner.SpawnTile(x, y);
            }
        }
    }

    void DestroyTiles()
    {
        for (int x = 0; x < mapSizeX; x++)
        {
            for (int y = 0; y < mapSizeY; y++)
            {
                Destroy(mapTiles[x, y].gameObject);
            }
        }
    }

    IEnumerator RegenerateMap()
    {
        Debug.Log("1 Regenerating map...");
        DestroyTiles();
        yield return new WaitForEndOfFrame();
        SpawnTiles();
    }
}