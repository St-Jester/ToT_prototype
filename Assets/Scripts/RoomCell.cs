using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomCell:MonoBehaviour
{
    //Particles
    //Type
    //Layer
    //Neightbours
    
    public IntVector2 coordinates;
    public BaseTile ThisTile
    {
        get;set;
    }
    private RoomCell[] neighbourCells = new RoomCell[Directions.Count];
    private int existingNeighbours;

    public bool IsPaired(Direction dir)
    {
        return neighbourCells[(int)dir] != null; 
    }
    public void AddNeighbour(RoomCell neighbour, Direction dir)
    {
        neighbourCells[(int)dir] = neighbour;
        existingNeighbours++;
    }
    public RoomCell GetNeighbour(Direction dir)
    {
        return neighbourCells[(int)dir];
    }
}
