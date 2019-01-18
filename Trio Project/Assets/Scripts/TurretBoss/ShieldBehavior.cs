using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBehavior : MonoBehaviour {

    public bool spawnShield;
    public bool shieldDown;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "eProjectile")
        {
            Physics.IgnoreCollision(other.collider, GetComponent<Collider>());
        }

        if(other.gameObject.tag == "PlayerBaseShot")
        {
            if(shieldDown == true)
            {
                print("I let the bullet through");
                Physics.IgnoreCollision(other.collider, GetComponent<Collider>());
            }
            if(shieldDown == false)
            {
                print("I killed the bullet");
                Destroy(gameObject);
            }
        }
    }
}
