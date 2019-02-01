using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseRangeScript : MonoBehaviour {

    public MainTurret head;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            head.tooClose = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            head.tooClose = false;
            //head.attacking = false;
        }
    }
}
