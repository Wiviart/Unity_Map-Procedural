using UnityEngine;

public class PositionConverter
{
    public static Vector2Int GetPoint(Vector2 point, int size)
    {
        int x = (int)point.x;
        int y = (int)point.y;

        int tempX = x / size;
        tempX *= size;
        int tempY = y / size;
        tempY *= size;

        int d = size / 2;
        if (x > tempX + d) tempX += size;
        if (y > tempY + d) tempY += size;

        return new Vector2Int(tempX / size, tempY / size);
    }
}