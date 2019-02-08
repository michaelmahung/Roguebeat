﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PT_Missile : MonoBehaviour/*, IDamageable<float> */{

    public GameObject BigBoom;
    //public GameObject Player;
    public Transform playerHere;
    public Rigidbody mrBody;
    //public int Life = 1;
    public float MissileSpeed;
    public float turnSpeed;
    

    // Use this for initialization
    void Start () {
        // Player = GameObject.FindGameObjectWithTag("Player");
        playerHere = GameObject.FindGameObjectWithTag("Player").transform;
       // KillPoints = 75;
    }
	
	// Update is called once per frame
	void Update () {

        // Vector3 relativePos = playerHere.position - transform.position;
        //Quaternion rotatation = Quaternion.LookRotation(relativePos, Vector3.up);
        /*if(Life <= 0)
        {
            callExplosion();
        }*/
        transform.position += transform.forward * MissileSpeed * Time.deltaTime;
        var playerPos = Quaternion.LookRotation(playerHere.position - transform.position);
        mrBody.MoveRotation(Quaternion.RotateTowards(transform.rotation, playerPos, turnSpeed));
        
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" || other.tag == "Wall")
        {
           
            callExplosion();
        }
        if (other.tag == "PlayerBaseShot")
        {
          
                //print("Im hit");
                Destroy(other);
                callExplosion();
            
        }
    }

    void callExplosion()
    {
        Instantiate(BigBoom, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    

    /*public void Damage(float hurt)
    {
        Life--;
    }*/
}
