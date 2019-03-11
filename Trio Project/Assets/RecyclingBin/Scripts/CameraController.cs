using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    //Controls the camera's position and smooths movement when following the player.

    public Transform player;
    public float smooth = 0.3f;

    public float height = 20f;

    private Vector3 velocity = Vector3.zero;

    void Update()
    {
        Vector3 pos = new Vector3();
        pos.x = player.position.x;
        pos.z = player.position.z;
        pos.y = player.position.y + height;
        transform.position = Vector3.SmoothDamp(transform.position, pos, ref velocity, smooth);
    }
}
