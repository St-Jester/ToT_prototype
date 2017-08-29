using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class Room : MonoBehaviour
{
    public IntVector2 size;

    [HideInInspector]
    public IntVector2 exit;

    public RoomTiles tilePrefab;
    public WallTiles wallPrefab;
    public Trap trapPrefab;
    public Chest chestPrefab;
    public RoomCell RoomCellPrefab;
    public Chest chestprefab;

    public RoomCell[,] cells;
    
    private float itemProbability;
    
    public RoomCell GetCell(IntVector2 c)
    {
        RoomCell copy;
        try { copy  = cells[c.x, c.z]; }
        catch { return null; }
       
        return copy;
    }

    public IEnumerator Generate()
    {
        yield return null;
        SetExit();
        cells = new RoomCell[size.x, size.z];
        RoomSetup();
        SetItems();
    }

    private void RoomSetup()
    {
        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.z; j++)
            {
                if (new IntVector2(i, j) == exit)
                {
                    CreateRoomTile(exit);//createroomtile
                    GetCell(exit).isAlowed = false;
                }
                else
                    if (i == 0 || j == 0 || i == size.x - 1 || j == size.z - 1)
                {
                    CreateWall(new IntVector2(i, j));
                    
                }
                else
                {
                    CreateRoomTile(new IntVector2(i, j));//createroomtile
                    

                }
            }
        }
        
    }

    private void SetItems()
    {
        itemProbability = 0.25f;

        IntVector2 current;

        for (int i = 0; i < 4; i++)
        {
            if(GetCell(exit).GetNeighbour((Direction)i) == null)
            {
                GetCell(exit).GetNeighbour(((Direction)i).GetOpposite()).isAlowed = false;
                Debug.Log(GetCell(exit).GetNeighbour(((Direction)i).GetOpposite()).coordinates.x+", "+GetCell(exit).GetNeighbour(((Direction)i).GetOpposite()).coordinates.z);
            }
        }
       // char marker = (char)0; 
        for (int i = 1; i < size.x - 1; i++)
        {
            for (int j = 1; j < size.z - 1; j++)
            {
                current = new IntVector2(i, j);
                if (GetCell(new IntVector2(i,j)).isAlowed)
                {
                    if(Random.value < itemProbability)
                    {
                        if (Random.value > 0.5)
                            SetTrap(current);
                        else
                            SetChest(current);
                        
                        for (int k = 0; k < Directions.Count; k++)
                        {
                            if (GetCell(current).GetNeighbour((Direction)k).isAlowed == false)
                            {
                                if ((Direction)k == Direction.West)
                                {
                                    GetCell(current).GetNeighbour(((Direction)k).GetOpposite()).isAlowed = false;
                                    GetCell(current).GetNeighbour(Direction.SouthEast).isAlowed = false;
                                    //marker += (char)1;
                                }
                                if((Direction)k == Direction.SouthWest)
                                {
                                    GetCell(current).GetNeighbour(((Direction)k).GetOpposite()).isAlowed = false;
                                    GetCell(current).GetNeighbour(Direction.East).isAlowed = false;
                                    //marker += (char)1;
                                }
                              //here put south or default
                            }
                        }
                        //if(marker == (char)0)
                        //{
                           
                        //}
                        GetCell(current).isAlowed = false;
                    }
                }
            }
        }
    }

    private bool Contains(IntVector2 coord)
    {
        return coord.x < size.x-1 && coord.x >= 1 && coord.z < size.z-1 && coord.z >= 1;
    }

    private void SetTrap(IntVector2 coordinates)
    {
        Trap trap = Instantiate(trapPrefab) as Trap;
        trap.Init(GetCell(coordinates));
        GetCell(coordinates).ThisTile = trap;
       
    }

    private void SetChest(IntVector2 coordinates)
    {
        Chest chest = Instantiate(chestprefab) as Chest;
        chest.Init(GetCell(coordinates));
        GetCell(coordinates).ThisTile = chest;
    }

    public void SetExit()
    {
        int rand = (int)Directions.RandomValue;
        rand %= 4;
        Debug.Log(rand);
        switch (rand)
        {
            case 0:  exit = new IntVector2(size.x-1, Random.Range(1, size.z-1)); break;
            case 1:  exit = new IntVector2(size.z-1, Random.Range(1, size.x-1));break;
            case 2:  exit = new IntVector2(0, Random.Range(1, size.z-1)); break;
            case 3:  exit = new IntVector2(0, Random.Range(1, size.x-1)); break;
            default: Debug.Log("Failed"); exit = new IntVector2(1, 1);break;
        }
        Debug.Log(exit.x + "," + exit.z);
    }

    private void CreateRoomTile(IntVector2 coordinates)
    {
        RoomCell newcell = CreateTile(coordinates);
        RoomTiles tile = Instantiate(tilePrefab) as RoomTiles;
        
        tile.gameObject.layer = 9;

        tile.Init(newcell);
        newcell.ThisTile = tile;
        newcell.isAlowed = true;
    }

    private void CreateWall(IntVector2 coordinates)
    {
        RoomCell newcell =  CreateTile(coordinates);

        WallTiles wall = Instantiate(wallPrefab) as WallTiles;
           
        wall.gameObject.layer = 9;
        wall.Init(newcell);
        newcell.ThisTile = wall;
        newcell.isAlowed = false;
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

