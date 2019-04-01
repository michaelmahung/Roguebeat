using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoom : MonoBehaviour, IRoomBehaviour
{
    [SerializeField] BossController bossController;

    public bool RoomActive { get; set; }

    public void StartBehaviour()
    {
        RoomActive = true;

        if (bossController != null)
        {
            bossController.StartBoss();
        }
    }

    public void StopBehaviour()
    {
        RoomActive = false;

        if (bossController != null)
        {
            bossController.StopBoss();
        }
    }
}
