using System.Collections.Generic;
using UnityEngine;

public class PathFinder
{
    private readonly MapTile[,] mapTiles;
    private readonly int mapSizeX;
    private readonly int mapSizeY;

    public PathFinder(MapTile[,] mapTiles, int mapSizeX, int mapSizeY)
    {
        this.mapTiles = mapTiles;
        this.mapSizeX = mapSizeX;
        this.mapSizeY = mapSizeY;
    }

    public bool IsPathToExit(Vector2Int startPos, Vector2Int exitPos)
    {
        var queue = new Queue<Vector2Int>();
        var visited = new HashSet<Vector2Int>();

        queue.Enqueue(startPos);
        visited.Add(startPos);

        while (queue.Count > 0)
        {
            var currentPos = queue.Dequeue();

            if (currentPos == exitPos) return true; // Reached the exit

            // Check all 4 directions: left, right, up, down
            foreach (var neighbor in GetNeighbors(currentPos))
            {
                if (visited.Contains(neighbor) || !IsValidMove(currentPos, neighbor)) continue;
                queue.Enqueue(neighbor);
                visited.Add(neighbor);
            }
        }

        return false;
    }

    private List<Vector2Int> GetNeighbors(Vector2Int pos)
    {
        return new List<Vector2Int>()
        {
            new Vector2Int(pos.x - 1, pos.y),
            new Vector2Int(pos.x + 1, pos.y),
            new Vector2Int(pos.x, pos.y - 1),
            new Vector2Int(pos.x, pos.y + 1)
        };
    }

    private bool IsValidMove(Vector2Int currentPos, Vector2Int neighborPos)
    {
        // Check bounds of the map and if tiles are connected via directions
        if (neighborPos.x < 0 || neighborPos.x >= mapSizeX || neighborPos.y < 0 || neighborPos.y >= mapSizeY)
            return false;

        // Check if directions between current and neighbor tiles match
        var currentTile = mapTiles[currentPos.x, currentPos.y];
        var neighborTile = mapTiles[neighborPos.x, neighborPos.y];

        if (currentPos.x < neighborPos.x && currentTile.GetDirections().right == 1 &&
            neighborTile.GetDirections().left == 1) return true; // Moving right
        if (currentPos.x > neighborPos.x && currentTile.GetDirections().left == 1 &&
            neighborTile.GetDirections().right == 1) return true; // Moving left
        if (currentPos.y < neighborPos.y && currentTile.GetDirections().up == 1 &&
            neighborTile.GetDirections().down == 1) return true; // Moving up
        if (currentPos.y > neighborPos.y && currentTile.GetDirections().down == 1 &&
            neighborTile.GetDirections().up == 1) return true; // Moving down

        return false;
    }
}