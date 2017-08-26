using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
public class RoomCell:MonoBehaviour
{
    //Particles
    //Type
    //Layer
    //Neightbours
    [HideInInspector]
    public IntVector2 coordinates;

    [HideInInspector]
    public BaseTile ThisTile {get;set;}

    [HideInInspector]
    public bool isAlowed;

    private RoomCell[] neighbourCells = new RoomCell[Directions.Count];

    private int existingNeighbours;


    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

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

    public Direction RandomUninitializedDirection
    {
        get
        {
            int skips = Random.Range(0, Directions.Count - existingNeighbours);
            for (int i = 0; i < Directions.Count; i++)
            {
                if (neighbourCells[i] == null)
                {
                    if (skips == 0)
                    {
                        return (Direction)i;
                    }
                    skips -= 1;
                }
            }
            throw new InvalidOperationException("MazeCell has no uninitialized directions left.");
        }
    }

    public bool IsFullyInitialised
    {
        get
        {
            return existingNeighbours == Directions.Count;
        }
    }

    public void OnPlayerEntered()
    {
        ThisTile.OnPlayerEntered();
    }

    public void OnPlayerExited()
    {
        ThisTile.OnPlayerExited();
        
    }

}
