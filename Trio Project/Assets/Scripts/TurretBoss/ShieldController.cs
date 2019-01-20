using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldController : MonoBehaviour {
    public bool raiseShields;
    public GameObject shield1;
    public GameObject shield2;
    public GameObject shield3;
    public GameObject shield4;
    public GameObject shield5;
    public GameObject shield6;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(raiseShields == true)
        {
            shield1.GetComponent<ShieldBehavior>().genShield = true;
            shield2.GetComponent<ShieldBehavior>().genShield = true;
            shield3.GetComponent<ShieldBehavior>().genShield = true;
            shield4.GetComponent<ShieldBehavior>().genShield = true;
            shield5.GetComponent<ShieldBehavior>().genShield = true;
            shield6.GetComponent<ShieldBehavior>().genShield = true;
            raiseShields = false;
        }
	}
}
