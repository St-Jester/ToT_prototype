using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Room : MonoBehaviour
{

    public IntVector2 size;
    public IntVector2 exit = new IntVector2(0, 1);
    public RoomTiles tilePrefab;
    public WallTiles wallPrefab;
    public RoomCell[,] cells;
    public RoomCell RoomCellPrefab;
    
    //void SetWorkingSpace()//insides of the room
    //{
    //    size.x-=2;
    //    size.z-=2;
    //}
    public RoomCell GetCell(IntVector2 c)
    {
        RoomCell copy;
        try { copy  = cells[c.x, c.z]; }
        catch { return null; }
       
        return copy;
    }
    public IEnumerator Generate()
    {
        cells = new RoomCell[size.x, size.z];
        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.z; j++)
            {
                yield return null;
                if (i == 0 || j == 0 || i == size.x - 1 || j == size.z - 1)
                {
                    CreateWall(new IntVector2(i, j));
                    //CreateTile(new IntVector2(i, j));//createwall
                }
                else {
                    CreateRoomTile(new IntVector2(i, j));//createroomtile
                }
            }
        }
    }

    private void CreateRoomTile(IntVector2 coordinates)
    {
        RoomCell newcell = CreateTile(coordinates);
        RoomTiles tile = Instantiate(tilePrefab) as RoomTiles;
        
            tile.gameObject.layer = 9;

        tile.Init(newcell);
        newcell.ThisTile = tile;
    }

    private void CreateWall(IntVector2 coordinates)
    {
        RoomCell newcell =  CreateTile(coordinates);

        WallTiles wall = Instantiate(wallPrefab) as WallTiles;
        
            
        wall.gameObject.layer = 9;

        wall.Init(newcell);
        newcell.ThisTile = wall;
    }

    RoomCell CreateTile(IntVector2 coordinates)
    {
        RoomCell newCell = Instantiate(RoomCellPrefab) as RoomCell;
        
        cells[coordinates.x, coordinates.z] = newCell;

        newCell.coordinates = coordinates;
        newCell.name = "Tile" + coordinates.x + "," + coordinates.z;
        newCell.transform.parent = transform;
        newCell.transform.localPosition = new Vector3(coordinates.x - size.x * 0.5f + 0.5f, -0.01f, coordinates.z - size.z * 0.5f + 0.5f);
        
        SetNeighbourhood(newCell);
        return newCell;
    }

    public void SetNeighbourhood(RoomCell currentCell)
    {
        Direction dirs;
        for (int i = 0; i < Directions.Count; i++)
        {
            dirs = (Direction)i;
            IntVector2 coordinates = currentCell.coordinates + dirs.ToIntVec2();
            if(GetCell(coordinates) != null && !currentCell.IsPaired(dirs)) 
            {
                currentCell.AddNeighbour(GetCell(coordinates), dirs);
                currentCell.GetNeighbour(dirs).AddNeighbour(currentCell,dirs.GetOpposite());
            }
        }
    }
}

