using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseRangeScript : MonoBehaviour {

    public MainTurret head;
    public PlayerHealth pHealth;
    public GameObject player;

	// Use this for initialization
	void Start () {
        pHealth = GameManager.Instance.PlayerObject.GetComponent<PlayerHealth>();
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
        /*if(other.gameObject.tag == null)
        {
            head.tooClose = false;
        }*/
        /*if (other.name == "MissileExplosion (Clone)")
        {
            print("supress blast");
            Destroy(other);
        }*/
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            head.tooClose = false;
            head.p3Start = true;
            //head.attacking = false;
        }
        
    }

    private void OnCollisionEnter(Collision other)
    {
        
    }
}
