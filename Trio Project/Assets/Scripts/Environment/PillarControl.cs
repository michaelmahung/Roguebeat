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

    public GameObject pTop;
    public GameObject pBody;
    public GameObject floor;
    public AudioClip mySong;
    public AudioSource Player;

    // Use this for initialization
    void Start () {
        Physics.IgnoreCollision(floor.GetComponent<Collider>(), GetComponent<Collider>());
        pBody.GetComponent<MeshRenderer>().material.color = Color.blue;
        pTop.GetComponent<MeshRenderer>().material.color = Color.blue;
	}
	
	// Update is called once per frame
	void Update () {
        
        if(Player.GetComponent<AudioSource>().clip == mySong)
        {
            turnOn = true;
            pBody.GetComponent<MeshRenderer>().material.color = Color.green;
            pTop.GetComponent<MeshRenderer>().material.color = Color.green;
            ChangeSize();
        }
        if(Player.GetComponent<AudioSource>().clip != mySong)
        {
            turnOn = false;
            pBody.GetComponent<MeshRenderer>().material.color = Color.blue;
            pTop.GetComponent<MeshRenderer>().material.color = Color.blue;
            ChangeSize();
        }

        
	}

    void ChangeSize()
    {
        float move = moveSpeed * Time.deltaTime;
        if (turnOn == true) //&& onOnce == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, up.position, move);
            //onOnce = true;

        }
        if (turnOn == false) // && onOnce == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, down.position, move);
            //onOnce = false;

        }
    }
}
