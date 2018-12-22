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

    //Added a collider array to allow pillars and their children to go through the floor.
    //Also added a floor layer and assigned it in the inspector.
    private Collider[] allColliders;

    // Use this for initialization
    void Start () {
        allColliders = GetComponentsInChildren<Collider>();
        floor = GameObject.FindGameObjectWithTag("Floor");

        //Foreach loop will get all children with collider and ignore collision with floor on them as well as parent.
        foreach (Collider col in allColliders)
        {
            Physics.IgnoreCollision(floor.GetComponent<Collider>(), col);
        }

        Physics.IgnoreCollision(floor.GetComponent<Collider>(), GetComponent<Collider>());
        Player = AudioManager.Instance.AudioPlayer;
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
