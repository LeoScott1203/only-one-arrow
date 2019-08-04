// Quick and dirty is the name of the game here when 4 hours are left till the deadline
// This is only compile time but what can you do
using UnityEngine;

public static class MapBoundaries
{
    static float minX = -16f;
    static float maxX = 16f;

    static float minY = -10f;
    static float maxY = 10f;

    public static bool InBounds(Vector2 vector)
    {
        return vector.x >= minX && vector.x <= maxX && vector.y >= minY && vector.y <= maxY;
    }
}