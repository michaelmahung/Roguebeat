using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoom : MonoBehaviour, IRoomBehaviour
{
    [SerializeField] MainController controller;
    public bool RoomActive { get; set; }

    public void StartBehaviour()
    {
        RoomActive = true;

        if (controller != null)
        {
            controller.inRoom = true;
            controller.SetValues();
        }
    }

    public void StopBehaviour()
    {
        RoomActive = false;

        if (controller != null)
        {
            controller.inRoom = false;
            controller.phase = "idle";
            controller.attackPhase = 0;
        }
    }
}
