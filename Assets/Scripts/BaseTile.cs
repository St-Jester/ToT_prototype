using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTile : MonoBehaviour
{
    public RoomCell currentCell;

    public virtual void Init(RoomCell curr)
    {    
        currentCell = curr;
        transform.parent = curr.transform;
        transform.localPosition = Vector3.zero;
    }

    public virtual void OnPlayerExited() { }
    public virtual void OnPlayerEntered() { }
}
