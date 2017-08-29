using UnityEngine;

public enum Direction
{
    North,
    East,
    South,
    West,
    NorthEast,
    SouthEast,
    SouthWest,
    NorthWest
}




public static class Directions
{
    public const int Count = 8;
   

    private static IntVector2[] vectors =
    {
        new IntVector2(0,1),
        new IntVector2(1,0),
        new IntVector2(0,-1),
        new IntVector2(-1,0),
        new IntVector2(1,1),
        new IntVector2(1,-1),
        new IntVector2(-1,-1),
        new IntVector2(-1,1)
    };

    private static Direction[] opposites = {
        Direction.South,
        Direction.West,
        Direction.North,
        Direction.East,
        Direction.SouthWest,
        Direction.NorthWest,
        Direction.NorthEast,
        Direction.SouthEast
    };

    public static Direction RandomValue
    {
        get
        {
            return (Direction)Random.Range(0, Count);
        }
    }
    
    public static Direction GetOpposite(this Direction direction)
    {
        return opposites[(int)direction];
    }
    

    public static IntVector2 ToIntVec2(this Direction dir)
    {
        return vectors[(int)dir];
    }
   

    public static Direction AddNumber(this Direction dir, int n)
    {
        return (Direction)((int)(dir + Count - 1) % Count);
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

