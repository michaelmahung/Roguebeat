using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour {

    public Transform player;
    public float speed;
    public int speedMultiplyer;
    public bool snapCalled;
    public bool follow;
    public bool callDestroyOnce;
    public string phase;
    public int attackPhase = 1;
    public ShieldController shieldControl;
    public SideTurret rTurret;
    public SideTurret lTurret;
    public MainTurret head;
    public GameObject cenCap;
    public GameObject cenBody;

	// Use this for initialization
	void Start () {
        phase = "Idle";
        Invoke("ShieldsUp", 1);
	}
	
	// Update is called once per frame
	void Update () {
        
            Vector3 relativePos = player.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
        if (follow == true)
        {
            if (head.tooClose == false)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, rotation, speed * Time.deltaTime);
            }
            if(head.tooClose == true)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, rotation,( speed * speedMultiplyer)* Time.deltaTime);
            }
        }
        if(snapCalled == true)
        {
            //Vector3 relativePos = player.position - transform.position;
           // Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, (speed * speedMultiplyer) * Time.deltaTime);
            follow = true;
            Invoke("SnapToBool", speed/speedMultiplyer);
            
        }
        if(rTurret.disabled == true && lTurret.disabled == true)
        {
            if (callDestroyOnce == false)
            {
                follow = false;
                DestroyAllSheilds();
                callDestroyOnce = true;
            }
        }
        if (attackPhase == 4 || (phase == "Attack" && head.tooClose == true))
        {

            cenBody.GetComponent<Renderer>().material.color = Color.red;
            cenCap.GetComponent<Renderer>().material.color = Color.red;
        }

        if(attackPhase != 4 && (phase != "Attack" || head.tooClose == false))
        {
            cenBody.GetComponent<Renderer>().material.color = Color.blue;
            cenCap.GetComponent<Renderer>().material.color = Color.blue;
        }

        /*if(phase == "Attack" && head.tooClose == true)
        {
            cenBody.GetComponent<Renderer>().material.color = Color.red;
            cenCap.GetComponent<Renderer>().material.color = Color.red;
        }*/
    }

    void SnapToBool()
    {
        //yield return new WaitForSeconds(1);
        snapCalled = false;
    }

    void Idle()
    {

    }

    void Stunned()
    {

    }

    void Attack()
    {
        snapCalled = true;
        phase = "Attack";
        //print("i am now attacking");

    }

    void RepairSides()
    {
        phase = "RepairSides";
        attackPhase++;
        if (attackPhase < 4)
        {
            rTurret.health = rTurret.maxHealth * attackPhase;
            lTurret.health = lTurret.maxHealth * attackPhase;
            rTurret.disabled = false;
            lTurret.disabled = false;
        }
        
        callDestroyOnce = false;
        if(attackPhase == 4)
        {
            cenBody.GetComponent<Renderer>().material.color = Color.red;
            cenCap.GetComponent<Renderer>().material.color = Color.red;
        }
        Invoke("Attack", 1);
    }

    void DestroyAllSheilds()
    {
        phase = "DestoyAllShields";
        shieldControl.GetComponent<ShieldController>().destroyShields = true;
        Invoke("ShieldsUp", 1);
    }

    void ShieldsUp()
    {
        phase = "ShieldsUp";
        shieldControl.GetComponent<ShieldController>().raiseShields = true;
        if (rTurret.disabled == false && lTurret.disabled == false)
        {
            Invoke("Attack", 2);
        }
        if(rTurret.disabled == true && lTurret.disabled == true && attackPhase <= 2)
        {
            Invoke("RepairSides", 2);
        }
        if(attackPhase == 3 && rTurret.dead == true && lTurret.dead == true)
        {
            attackPhase++;
            Invoke("Attack", 1);
        }
    }

}
