using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarControl : MonoBehaviour {

    public Transform up;
    public Transform down;
    private Transform CurrentPosition;

    public float moveSpeed;

    public bool turnOn;
    bool onOnce;

    

    // Use this for initialization
    void Start () {
     
	}
	
	// Update is called once per frame
	void Update () {
        float move = moveSpeed * Time.deltaTime;

		if (turnOn == true) //&& onOnce == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, up.position, move);
          
            //onOnce = true;

        }
        if(turnOn == false) // && onOnce == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, down.position, move);
            //onOnce = false;

        }
	}

    void ChangeSize()
    {
       
    }
}
