using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using UnityEngine;
using Random = UnityEngine.Random;

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

        yield return new WaitForSeconds(0.1f);

        StartCoroutine(CheckValidPath());
    }

    void SpawnTiles()
    {
        for (int x = 0; x < mapSizeX; x++)
        {
            for (int y = 0; y < mapSizeY; y++)
            {
                SpawnTile(x, y);
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
        DestroyTiles();
        yield return new WaitForSeconds(0.1f);
        SpawnTiles();
    }

    bool SpawnTile(int x, int y)
    {
        if (x < 0 || x >= mapSizeX || y < 0 || y >= mapSizeY) return false;

        Vector3 pos = size * new Vector3(x, y, 0);

        MapTile tile = CheckPossibleTiles(x, y);

        MapTile mapTile = Instantiate(tile, pos, Quaternion.identity, transform);
        mapTiles[x, y] = mapTile;
        return true;
    }

    MapTile CheckPossibleTiles(int x, int y)
    {
        if (x < 0 || x >= mapSizeX || y < 0 || y >= mapSizeY) return null;

        List<MapTile> possibleTiles = new List<MapTile>();

        foreach (var tile in mapTilePrefabs)
        {
            if ((x == 0 && tile.GetDirections().left == 1)
             || (y == 0 && tile.GetDirections().down == 1)
             || (x == mapSizeX - 1 && tile.GetDirections().right == 1)
             || (y == mapSizeY - 1 && tile.GetDirections().up == 1))
                continue;

            if ((x > 0 && mapTiles[x - 1, y] && mapTiles[x - 1, y].GetDirections().right != tile.GetDirections().left)
             || (y > 0 && mapTiles[x, y - 1] && mapTiles[x, y - 1].GetDirections().up != tile.GetDirections().down)
             || (x < mapSizeX - 1 && mapTiles[x + 1, y] && mapTiles[x + 1, y].GetDirections().left != tile.GetDirections().right)
             || (y < mapSizeY - 1 && mapTiles[x, y + 1] && mapTiles[x, y + 1].GetDirections().down != tile.GetDirections().up)
            ) continue;

            possibleTiles.Add(tile);
        }

        if (possibleTiles.Count == 0) return null;
        Debug.Log(possibleTiles.Count);
        int index = Random.Range(0, possibleTiles.Count);
        return possibleTiles[index];
    }
}