using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    
    public FogOfWar[,] fogs;

    private IntVector2 size;

    private RoomCell currentCell;

    [HideInInspector]
    public Transform Fog;

    public void SetLocation(RoomCell tile)
    {
        if (currentCell != null)
        {
            currentCell.OnPlayerExited();
        }
        currentCell = tile;
        transform.localPosition = tile.transform.localPosition;

        for (int i = tile.coordinates.x - 1, l = 0; l < 3; l++, i++)
        {
            for (int j = tile.coordinates.z - 1, k = 0; k < 3; k++, j++)
            {
                if (i >= 0 && i < size.x && j >= 0 && j < size.z)
                {
                    try
                    {
                        MakeInvisible(new IntVector2(i, j));
                    }
                    catch(System.Exception e)
                    {
                        Debug.Log(e.Message);

                        continue;
                    }
                }

            }
        }
        Debug.Log(tile.transform.localPosition + "," + tile.coordinates.x + "," + tile.coordinates.z);
        currentCell.OnPlayerEntered();
        
    }
    private void Move(Direction dir)
    {
        IntVector2 buffer = currentCell.coordinates;

        if (!Contains(buffer + dir.ToIntVec2()) || currentCell.GetNeighbour(dir).ThisTile is WallTiles)
        {
            Debug.Log("Contains == false " + currentCell.coordinates.x + "," + currentCell.coordinates.z);
            
            return;
        }
        RoomCell nextcell = currentCell.GetNeighbour(dir);
        
        SetLocation(nextcell);
    }
    public void CreateRoomFog(IntVector2 size, FogOfWar FOWprefab)
    {
        
        Fog = new GameObject("Fog").transform;
        this.size = size;
        fogs = new FogOfWar[size.x, size.z];
        
        for (int i = 0; i < size.x; i++)
        {
            for (int j= 0; j <size.z;  j++)
            {
                fogs[i, j] = Instantiate(FOWprefab);
                fogs[i, j].coordinates = new IntVector2(i,j);
                fogs[i,j].transform.localPosition = new Vector3(i - size.x * 0.5f + 0.5f, 0f, j - size.z * 0.5f + 0.5f);
                fogs[i, j].transform.parent = Fog;
                fogs[i, j].name = "Fog" + i +","+ j;
                fogs[i, j].gameObject.layer = 9;
            }
        }
    }
    private void MakeInvisible(IntVector2 coordinates)
    {
        fogs[coordinates.x, coordinates.z].gameObject.SetActive(false);
    }
    private bool Contains(IntVector2 coord)
    {
        return coord.x < size.x && coord.x >= 0 && coord.z < size.z && coord.z >= 0;
    }
    
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
