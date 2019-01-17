using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainTurret : MonoBehaviour {

    public MainController controller;

    public int health;


	// Use this for initialization
	void Start () {

        controller = gameObject.GetComponent<MainController>();

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
