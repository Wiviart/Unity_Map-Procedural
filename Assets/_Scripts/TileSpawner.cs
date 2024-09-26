using System.Collections.Generic;
using UnityEngine;

public class TileSpawner
{
    private readonly MapTile[,] mapTiles;
    private readonly int mapSizeX, mapSizeY;
    private readonly List<MapTile> mapTilePrefabs;
    private readonly Transform transform;
    private  float size;
    public TileSpawner(
        Transform transform,
        float size,
        MapTile[,] mapTiles,
        int mapSizeX, int mapSizeY,
        List<MapTile> mapTilePrefabs)
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

        MapTile tile = CheckPossibleTiles(x, y);

        MapTile mapTile = Object.Instantiate(tile, pos, Quaternion.identity, transform);
        mapTiles[x, y] = mapTile;
        return true;
    }

    MapTile CheckPossibleTiles(int x, int y)
    {
        TileChecker tileChecker = new(mapTiles, mapSizeX, mapSizeY, mapTilePrefabs);
        List<MapTile> possibleTiles = tileChecker.CheckPossibleTiles(x, y);

        if (possibleTiles == null) return null;

        int index = Random.Range(0, possibleTiles.Count);
        return possibleTiles[index];
    }
}