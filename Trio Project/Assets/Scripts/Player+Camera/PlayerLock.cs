using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLock : MonoBehaviour {

    //Simply makes this object move alongside with whatever object is set as the player.

    public Transform player;
    bool playerInScene;

	void Start () 
    {
        try
        {
            //Check to see if there is a gameobject tagged as the player
            player = GameManager.Instance.player.transform;
            playerInScene = true;
        }
        catch
        {
            //If there isnt, flag a warning
            Debug.LogError("No PlayerHealth component found, please assign one.");
            playerInScene = false;
        }
	}
	
	void Update ()
    {
        if (playerInScene)
        {
            //If a player is found in the scene, set this location to follow the player.
            transform.position = player.position;
        } else
        {
            return;
        }
	}
}
