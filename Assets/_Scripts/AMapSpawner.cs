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

    protected static Action OnMapRegenerated;
    private void MapRegenerated() => OnMapRegenerated?.Invoke();

    private void Start()
    {
        mapTiles = new MapTile[mapSO.mapSize.x, mapSO.mapSize.y];
        tileSpawner = new TileSpawner(
            mapSO.mapTilePrefabs, mapTiles, transform,
            mapSO.mapSize.x, mapSO.mapSize.y, mapSO.tileSize);

        SpawnTiles();                            // Spawn map tiles
        StartCoroutine(CheckValidPath()); // Check if map is valid
    }

    protected abstract void SpawnTiles();

    private IEnumerator CheckValidPath()
    {
        bool validPathExists = HasValidPath();

        // If map is valid, call action
        if (validPathExists)
        {
            MapRegenerated();
            yield break;
        }

        // If map is invalid, regenerate map
        StartCoroutine(RegenerateMap());
        yield return new WaitForEndOfFrame();
        StartCoroutine(CheckValidPath());
    }

    protected virtual bool HasValidPath()
    {
        var playerStart = GetPlayerPosition();
        var exitPos = GetExitPosition();

        var pathFinder = new PathFinder(
            mapTiles, mapSO.mapSize.x, mapSO.mapSize.y);

        return pathFinder.IsPathToExit(playerStart, exitPos);
    }

    protected virtual Vector2Int GetPlayerPosition() { return playerStart * 10; }
    protected virtual Vector2Int GetExitPosition() { return exitPos * 10; }

    private IEnumerator RegenerateMap()
    {
        Debug.Log(gameObject.name + " regenerating map...");
        DestroyTiles();
        yield return new WaitForEndOfFrame();
        SpawnTiles();
    }

    private void DestroyTiles()
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
