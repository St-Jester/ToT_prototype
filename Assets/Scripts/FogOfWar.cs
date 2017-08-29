using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogOfWar : RoomCell
{
    public void ChangeLayer(int layerNumber)
    {
        gameObject.layer = layerNumber;
    }
    //topmost layer
}
