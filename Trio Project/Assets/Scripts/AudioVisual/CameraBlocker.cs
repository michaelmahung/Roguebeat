using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBlocker : MonoBehaviour
{
    [SerializeField] bool blockCam;
    [SerializeField] Camera myCam;

    private void Awake()
    {
        if (blockCam)
        myCam.cullingMask = 0;
        //LevelSpawning.FinishedSpawningRooms += UnBlockCamera;
    }

    public void UnBlockCamera()
    {
        if (blockCam)
        myCam.cullingMask = -1;
    }
}
