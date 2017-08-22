using UnityEngine;

public enum Direction
{
    North,
    East,
    South,
    West
}


public static class Directions
{
    public const int Count = 4;

    public static Direction RandomValue
    {
        get
        {
            return (Direction)Random.Range(0, Count);
        }
    }
    private static IntVector2[] vectors =
    {
        new IntVector2(0,1),
        new IntVector2(1,0),
        new IntVector2(0,-1),
        new IntVector2(-1,0)
    };
    private static Direction[] opposites = {
        Direction.South,
        Direction.West,
        Direction.North,
        Direction.East
    };

    public static Direction GetOpposite(this Direction direction)
    {
        return opposites[(int)direction];
    }
    public static IntVector2 ToIntVec2(this Direction dir)
    {
        return vectors[(int)dir];
    }

    public static Vector3 ToVector3(this Direction dir)
    {
        switch((int)dir)
        {
            case 0:
                return new Vector3(0, 0, 1);
            case 1:
                return new Vector3(1, 0, 0);
            case 2:
                return new Vector3(0, 0, -1);
            case 3:
                return new Vector3(-1, 0, 0);
            default:
                return Vector3.zero;
        }
    }
}

