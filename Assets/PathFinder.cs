using System.Collections.Generic;
using UnityEngine;

public class PathFinder
{
    private MapTile[,] mapTiles;
    int mapSizeX, mapSizeY;
    public PathFinder(MapTile[,] mapTiles, int mapSizeX, int mapSizeY)
    {
        this.mapTiles = mapTiles;
        this.mapSizeX = mapSizeX;
        this.mapSizeY = mapSizeY;
    }
    public bool IsPathToExit(Vector2Int startPos, Vector2Int exitPos)
    {
        Queue<Vector2Int> queue = new Queue<Vector2Int>();
        HashSet<Vector2Int> visited = new HashSet<Vector2Int>();

        queue.Enqueue(startPos);
        visited.Add(startPos);

        while (queue.Count > 0)
        {
            Vector2Int currentPos = queue.Dequeue();

            // If we reached the exit, return true
            if (currentPos == exitPos) return true;

            // Check all 4 directions: left, right, up, down
            foreach (var neighbor in GetNeighbors(currentPos))
            {
                if (!visited.Contains(neighbor) && IsValidMove(currentPos, neighbor))
                {
                    queue.Enqueue(neighbor);
                    visited.Add(neighbor);
                }
            }
        }

        // If we exhaust the queue without reaching the exit, return false
        return false;
    }

    List<Vector2Int> GetNeighbors(Vector2Int pos)
    {
        return new List<Vector2Int>()
        {
            new Vector2Int(pos.x - 1, pos.y),
            new Vector2Int(pos.x + 1, pos.y),
            new Vector2Int(pos.x, pos.y - 1),
            new Vector2Int(pos.x, pos.y + 1)
        };
    }

    bool IsValidMove(Vector2Int currentPos, Vector2Int neighborPos)
    {
        // Check bounds of the map and if tiles are connected via directions
        if (neighborPos.x < 0 || neighborPos.x >= mapSizeX || neighborPos.y < 0 || neighborPos.y >= mapSizeY)
            return false;

        // Check if directions between current and neighbor tiles match
        MapTile currentTile = mapTiles[currentPos.x, currentPos.y];
        MapTile neighborTile = mapTiles[neighborPos.x, neighborPos.y];

        if (currentPos.x < neighborPos.x && currentTile.GetDirections().right == 1 && neighborTile.GetDirections().left == 1) return true; // Moving right
        if (currentPos.x > neighborPos.x && currentTile.GetDirections().left == 1 && neighborTile.GetDirections().right == 1) return true; // Moving left
        if (currentPos.y < neighborPos.y && currentTile.GetDirections().up == 1 && neighborTile.GetDirections().down == 1) return true; // Moving up
        if (currentPos.y > neighborPos.y && currentTile.GetDirections().down == 1 && neighborTile.GetDirections().up == 1) return true; // Moving down

        return false;
    }
}