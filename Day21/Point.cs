
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day21;
public record struct Point(int X, int Y)
{
    public static Point operator +(Point a, Point b) => new Point(a.X + b.X, a.Y + b.Y);

    public static Point operator -(Point a, Point b) => new Point(a.X - b.X, a.Y - b.Y);

    public static Point operator *(Point point, int multiple) => new Point(point.X * multiple, point.Y * multiple);

    public Point Normalize() => new Point(X != 0 ? X / Math.Abs(X) : 0, Y != 0 ? Y / Math.Abs(Y) : 0);

    public static implicit operator Point((int X, int Y) tuple) => new Point(tuple.X, tuple.Y);

    public int ManhattanDistance(Point b) => Math.Abs(X - b.X) + Math.Abs(Y - b.Y);
}

public record struct Point3d(int X, int Y, int Z)
{
    public static Point3d operator +(Point3d a, Point3d b) => new Point3d(a.X + b.X, a.Y + b.Y, a.Z + b.Z);

    public static Point3d operator -(Point3d a, Point3d b) => new Point3d(a.X - b.X, a.Y - b.Y, a.Z - b.Z);

    public Point3d Normalize() => new Point3d(X != 0 ? X / Math.Abs(X) : 0, Y != 0 ? Y / Math.Abs(Y) : 0, Z != 0 ? Z / Math.Abs(Z) : 0);

    public static implicit operator Point3d((int X, int Y, int Z) tuple) => new Point3d(tuple.X, tuple.Y, tuple.Z);

    public int ManhattanDistance(Point3d b) => Math.Abs(X - b.X) + Math.Abs(Y - b.Y) + Math.Abs(Z - b.Z);
}

public static class Directions
{
    public static Point[] WithoutDiagonals { get; } =
    [
        (0, 1),
        (1, 0),
        (0, -1),
        (-1, 0),
    ];

    public static Point[] DiagonalsOnly { get; } =
    [
        (1, 1),
        (-1, 1),
        (1, -1),
        (-1, -1)
    ];

    public static Point[] WithDiagonals { get; } =
    [
        (0, 1),
        (1, 0),
        (0, -1),
        (-1, 0),
        (1, 1),
        (-1, 1),
        (1, -1),
        (-1, -1)
    ];

    public static Point3d[] WithoutDiagonals3d { get; } =
    [
        (1, 0, 0),
        (0, 1, 0),
        (0, 0, 1),
        (-1, 0, 0),
        (0, -1, 0),
        (0, 0, -1),
    ];
}