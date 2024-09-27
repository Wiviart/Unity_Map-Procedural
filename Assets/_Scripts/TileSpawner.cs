using System.Collections.Generic;
using UnityEngine;

public class TileSpawner
{
    private readonly List<MapTile> mapTilePrefabs;
    private readonly MapTile[,] mapTiles;
    private readonly Transform transform;
    private readonly int mapSizeX, mapSizeY;
    private readonly int size;

    public TileSpawner(
        List<MapTile> mapTilePrefabs,
        MapTile[,] mapTiles,
        Transform transform,
        int mapSizeX, int mapSizeY,
        int size
        )
    {
        this.mapTiles = mapTiles;
        this.mapSizeX = mapSizeX;
        this.mapSizeY = mapSizeY;
        this.mapTilePrefabs = mapTilePrefabs;
        this.transform = transform;
        this.size = size;
    }

    public bool SpawnTile(int x, int y)
    {
        if (x < 0 || x >= mapSizeX || y < 0 || y >= mapSizeY) return false;

        Vector3 pos = size * new Vector3(x, y, 0);

        MapTile tile = GetRandomTile(x, y);

        MapTile mapTile = Object.Instantiate(tile, pos, Quaternion.identity, transform);
        mapTiles[x, y] = mapTile;
        return true;
    }

    MapTile GetRandomTile(int x, int y)
    {
        TileChecker tileChecker = new(mapTilePrefabs, mapTiles, mapSizeX, mapSizeY);
        List<MapTile> possibleTiles = tileChecker.CheckPossibleTiles(x, y);

        if (possibleTiles == null) return null;

        int index = Random.Range(0, possibleTiles.Count);
        return possibleTiles[index];
    }
}