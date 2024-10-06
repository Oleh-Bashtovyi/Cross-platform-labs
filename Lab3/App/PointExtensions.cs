using System.Drawing;

namespace App;

public static class PointExtensions
{
    /// <summary>
    /// Adds two points
    /// </summary>
    /// <param name="point"></param>
    /// <param name="other"></param>
    /// <returns>new Point(point.X + other.X, point.Y + other.Y)</returns>
    public static Point Add(this Point point, Point other)
    {
        return new Point(point.X + other.X, point.Y + other.Y);
    }
}
