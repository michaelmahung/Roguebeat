using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController2 : MonoBehaviour {

    public GameObject player;
    public float followAhead;
    public float smoothing;

    private Vector3 targetPosition;
    private Vector3 cameraOffset;

    void Start()
    {
        cameraOffset = transform.position - player.transform.position;
    }

    void FixedUpdate()
    {
        if (player.transform.localRotation.y > 90f)
        {
            targetPosition = (player.transform.position + (player.transform.forward * followAhead)) + cameraOffset;
        }
        else
        {
            targetPosition = (player.transform.position + (player.transform.forward * -1 * -followAhead)) + cameraOffset;
        }
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing * Time.deltaTime);
    }

}
