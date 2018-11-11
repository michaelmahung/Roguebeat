using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLock : MonoBehaviour {

    //Simply makes this object move alongside with whatever object is set as the player.

    public Transform player;

	void Start () {
		
	}
	
	void Update ()
    {
        transform.position = player.position;
	}
}
