using System;
using System.Collections;
using UnityEngine;

public abstract class AMapSpawner : MonoBehaviour
{
    [SerializeField] protected MapSO mapSO;
    [SerializeField] Vector2Int playerStart = Vector2Int.zero;
    [SerializeField] Vector2Int exitPos = Vector2Int.zero;
    protected MapTile[,] mapTiles;
    protected TileSpawner tileSpawner;

    public static Action OnMapRegenerated;
    void MapRegenerated() => OnMapRegenerated?.Invoke();

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
            MapRegenerated();
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

    protected virtual Vector2Int GetPlayerPosition() { return playerStart * 10; }
    protected virtual Vector2Int GetExitPosition() { return exitPos * 10; }

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
