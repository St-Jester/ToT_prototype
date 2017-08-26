using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : InteractableItems
{
    public float damage;

    public bool activated;

    //public override void Init(RoomCell curr)
    //{
    //    base.Init(curr);
        
    //}
    public override void OnPlayerEntered()
    {
        Debug.Log("Got himself into a trap. received " + damage + "of damage");
    }
    public override void OnPlayerExited()
    {
        Debug.Log("Succesfully survived the trap");
    }
}
