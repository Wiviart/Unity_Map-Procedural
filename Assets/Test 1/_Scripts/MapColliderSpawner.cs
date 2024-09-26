using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using UnityEngine;
using Random = UnityEngine.Random;

public class MapColliderSpawner : MonoBehaviour
{
    [SerializeField] private MapCollider mapColliderPrefab;
    const float size = 10;
    const int mapSizeX = 10, mapSizeY = 10;

    MapCollider[,] mapColliders;

    void Start()
    {
        SpawnCollider();
    }

    private void SpawnCollider()
    {
        mapColliders = new MapCollider[mapSizeX, mapSizeY];
        for (int i = 0; i < mapSizeX; i++)
        {
            for (int j = 0; j < mapSizeY; j++)
            {
                Vector3 pos = size * new Vector3(i, j, 0);
                MapCollider mapCollider = Instantiate(mapColliderPrefab, pos, Quaternion.identity, transform);
                mapCollider.Init(size * 1.25f);
                mapColliders[i, j] = mapCollider;
            }
        }
    }
}
