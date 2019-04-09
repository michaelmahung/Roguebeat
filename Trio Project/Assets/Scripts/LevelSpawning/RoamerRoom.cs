using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoamerRoom : MonoBehaviour, IRoomBehaviour
{
    [SerializeField] private Roamer myRoamer;
    public bool RoomActive { get; set; }

    public void StartBehaviour()
    {
        //Debug.Log("Activating Roamer");
        RoomActive = true;

        if (myRoamer != null)
            myRoamer.ChasePlayer();
    }

    public void StopBehaviour()
    {
        //Debug.Log("Deactivating Roamer");
        RoomActive = false;

        if (myRoamer != null)
            myRoamer.StopChasing();
    }
}
