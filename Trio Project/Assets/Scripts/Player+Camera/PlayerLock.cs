using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLock : MonoBehaviour {

    //Simply makes this object move alongside with whatever object is set as the player.

    private Transform player;

	void Start () 
    {
        try
        {
            //Check to see if there is a gameobject tagged as the player
            player = GameManager.Instance.Player.transform;
        }
        catch
        {
            //If there isnt, flag a warning
            Debug.LogError("No player gameobject found.");
        }
	}
	
	void Update ()
    {
        if (player != null)
        {
            //If a player is found in the scene, set this location to follow the player.
            transform.position = player.position;
        } else
        {
            return;
        }
	}
}
