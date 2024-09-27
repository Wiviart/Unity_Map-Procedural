using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class AMapSpawner : MonoBehaviour
{

    [SerializeField] protected MapSO mapSO;
    protected MapTile[,] mapTiles;
    protected TileSpawner tileSpawner;

    void Start()
    {
        mapTiles = new MapTile[mapSO.mapSize.x, mapSO.mapSize.y];
        tileSpawner = new TileSpawner(
            mapSO.mapTilePrefabs, mapTiles, transform,
            mapSO.mapSize.x, mapSO.mapSize.y, mapSO.tileSize);

        SpawnTiles();
        StartCoroutine(CheckValidPath());
    }

    protected abstract void SpawnTiles();

    IEnumerator CheckValidPath()
    {
        bool validPathExists = HasValidPath();

        if (validPathExists)
        {
            FunctionAfterValidPath();
            yield break;
        }

        StartCoroutine(RegenerateMap());
        yield return new WaitForEndOfFrame();
        StartCoroutine(CheckValidPath());
    }

    protected virtual bool HasValidPath()
    {
        Vector2Int playerStart = GetPlayerPosition();
        Vector2Int exitPos = GetExitPosition();

        PathFinder pathFinder = new PathFinder(
            mapTiles, mapSO.mapSize.x, mapSO.mapSize.y);

        return pathFinder.IsPathToExit(playerStart, exitPos);
    }

    protected virtual void FunctionAfterValidPath() { }

    protected virtual Vector2Int GetPlayerPosition()
    {
        return new Vector2Int(0, 0);
    }

    protected virtual Vector2Int GetExitPosition()
    {
        int x = mapSO.mapSize.x - 1;
        int y = mapSO.mapSize.y - 1;
        return new Vector2Int(x, y);
    }

    IEnumerator RegenerateMap()
    {
        Debug.Log(gameObject.name + " regenerating map...");
        DestroyTiles();
        yield return new WaitForEndOfFrame();
        SpawnTiles();
    }

    void DestroyTiles()
    {
        for (int x = 0; x < mapSO.mapSize.x; x++)
        {
            for (int y = 0; y < mapSO.mapSize.y; y++)
            {
                if (!mapTiles[x, y]) continue;
                Destroy(mapTiles[x, y].gameObject);
            }
        }
    }
}
