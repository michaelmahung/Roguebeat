using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldController : MonoBehaviour {
    public bool raiseShields;
    public bool destroyShields;
    public int speed; 
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
        transform.Rotate(new Vector3(0, Time.deltaTime * speed, 0));
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
        if(destroyShields == true)
        {
            shield1.GetComponent<ShieldBehavior>().shieldDown = true;
            shield1.GetComponent<ShieldBehavior>().shieldDown = true;
            shield2.GetComponent<ShieldBehavior>().shieldDown = true;
            shield3.GetComponent<ShieldBehavior>().shieldDown = true;
            shield4.GetComponent<ShieldBehavior>().shieldDown = true;
            shield5.GetComponent<ShieldBehavior>().shieldDown = true;
            shield6.GetComponent<ShieldBehavior>().shieldDown = true;
            destroyShields = false;
        }
	}
}
