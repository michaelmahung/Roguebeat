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
        try
        {
            //Attempt to find a gameobject tagged as the player.
            player = GameManager.Instance.player;
            cameraOffset = transform.position - player.transform.position;
        }
        catch
        {
            //If there is no gameobject found with the tag, throw an error.
            Debug.LogError("No GameObject with PlayerHealth component found, please assign one");
        }
    }


    //I dont remember 100% how this works, but it does work
    void FixedUpdate()
    {
        //Stuff and things.
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
