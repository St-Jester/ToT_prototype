using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    
    public FogOfWar[,] fogs;
    private IntVector2 size;
    private RoomCell currentCell;
    public void SetPlayerInstance(RoomCell cell)
    {
        currentCell = cell;
    }
    public void SetLocation(RoomCell tile)
    {
        RoomCell buffer = tile;
        currentCell = tile;
        transform.localPosition = new Vector3(
            tile.transform.localPosition.x,
            0f,
            tile.transform.localPosition.z);

        Debug.Log(transform.localPosition);
        Debug.Log(tile.coordinates.x+","+tile.coordinates.z);
        tile = buffer;
    }
    private void Move(Direction dir)
    {
        
        IntVector2 buffer = currentCell.coordinates;
        if (!Contains(buffer + dir.ToIntVec2()))
        {
            Debug.Log("Contains == false " + currentCell.coordinates.x + "," + currentCell.coordinates.z);
            return;
        }
        RoomCell nextcell = currentCell.GetNeighbour(dir);
        Debug.Log(nextcell.GetType());
        for (int i = nextcell.coordinates.x - 1,l = 0; l < 3; l++,i++)
        {
            for (int j = nextcell.coordinates.z - 1, k = 0; k < 3; k++,j++)
            {
                if (i >= 0 || i < size.x || j >= 0 || j < size.z)
                {
                    try
                    {
                        MakeInvisible(new IntVector2(i, j));
                    }
                    catch
                    {
                        continue;
                    }
                }
                  
            }
        }
        
        SetLocation(nextcell);
    }
    public void CreateRoomFog(IntVector2 size, FogOfWar FOWprefab)
    {
        this.size = size;
        fogs = new FogOfWar[size.x, size.z];
        for (int i = 0; i < size.x; i++)
        {
            for (int j= 0; j <size.z;  j++)
            {
                fogs[i, j] = Instantiate(FOWprefab);
                fogs[i, j].coordinates = new IntVector2(i,j);
                fogs[i,j].transform.localPosition = new Vector3(i - size.x * 0.5f + 0.5f, 0.2f, j - size.z * 0.5f + 0.5f);
                
                fogs[i, j].name = "Fog" + i +","+ j;
                fogs[i, j].gameObject.layer = 9;
            }
        }
    }
    private void MakeInvisible(IntVector2 coordinates)
    {
        fogs[coordinates.x, coordinates.z].ChangeLayer(8);
    }
    private bool Contains(IntVector2 coord)
    {
        return coord.x < size.x && coord.x >= 0 && coord.z < size.z && coord.z >= 0;
    }
    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Move(Direction.North);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Move(Direction.East);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Move(Direction.South);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Move(Direction.West);
        }
    }
}
