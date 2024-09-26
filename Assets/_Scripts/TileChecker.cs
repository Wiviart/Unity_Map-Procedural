using System.Collections.Generic;

public class TileChecker
{
    private readonly MapTile[,] mapTiles;
    private readonly int mapSizeX, mapSizeY;
    private readonly List<MapTile> mapTilePrefabs;

    public TileChecker(
        MapTile[,] mapTiles,
        int mapSizeX, int mapSizeY,
        List<MapTile> mapTilePrefabs)
    {
        this.mapTiles = mapTiles;
        this.mapSizeX = mapSizeX;
        this.mapSizeY = mapSizeY;
        this.mapTilePrefabs = mapTilePrefabs;
    }

    public List<MapTile> CheckPossibleTiles(int x, int y)
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

        return possibleTiles;
    }
}