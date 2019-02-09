using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour, ITrackRooms {

    public string MyRoomName { get; set; }
    public RoomSetter MyRoom { get; set; }
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
       // Invoke("ShieldsUp", 1);
        player = GameObject.FindGameObjectWithTag("Player").transform;
        RoomSetter.UpdatePlayerRoom += CheckPlayerRoom;
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
            snapCalled = false;
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
        if ((attackPhase == 4 || (phase == "Attack" && head.tooClose == true)) && head.changeColor == false)
        {

            cenBody.GetComponent<Renderer>().material.color = Color.red;
            cenCap.GetComponent<Renderer>().material.color = Color.red;
        }

        if(attackPhase != 4 && (phase != "Attack" || head.tooClose == false))
        {
            cenBody.GetComponent<Renderer>().material.color = Color.blue;
            cenCap.GetComponent<Renderer>().material.color = Color.blue;
        }

        if((head.tooClose == true && head.changeColor == true) || (attackPhase == 4 && head.changeColor == true))
        {
            cenBody.GetComponent<Renderer>().material.color = Color.white;
            cenCap.GetComponent<Renderer>().material.color = Color.white;
        }

        /*if(phase == "Attack" && head.tooClose == true)
        {
            cenBody.GetComponent<Renderer>().material.color = Color.red;
            cenCap.GetComponent<Renderer>().material.color = Color.red;
        }*/
    }

    void SnapToBool()
    {
       // Debug.Log("SnapToBool");
        //yield return new WaitForSeconds(1);
        snapCalled = false;
    }

    void Idle()
    {
       // Debug.Log("Idle");
    }

    void Stunned()
    {

    }

    void Attack()
    {
        //Debug.Log("Attack");
        snapCalled = true;
        phase = "Attack";
        //print("i am now attacking");

    }

    void RepairSides()
    {
        //Debug.Log("RepairSides");
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
        //Debug.Log("DestroyAllShields");
        phase = "DestoyAllShields";
        shieldControl.GetComponent<ShieldController>().destroyShields = true;
        Invoke("ShieldsUp", 1);
    }

    void ShieldsUp()
    {
        //Debug.Log("Starting Shields Up Phase");
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
    void SetValues()
    {
       // Debug.Log("SetValues");
        //main turret info
        head.health = 300;
        head.tooClose = false;
        head.attacking = false;
        head.changeColor = false;
        head.trueOnce = false;

        //Left turret info
        lTurret.health = lTurret.maxHealth;
        lTurret.Tbody.SetActive(true);
        lTurret.cap.SetActive(true);
        lTurret.barrel.SetActive(true);
        lTurret.spawn.SetActive(true);
        lTurret.disabled = false;
        lTurret.dead = false;
        lTurret.attacking = false;
        lTurret.changeColor = false;
        lTurret.transform.tag = "Enemy";

        //Right turret info
        rTurret.health = rTurret.maxHealth;
        rTurret.Tbody.SetActive(true);
        rTurret.cap.SetActive(true);
        rTurret.barrel.SetActive(true);
        rTurret.spawn.SetActive(true);
        rTurret.disabled = false;
        rTurret.dead = false;
        rTurret.attacking = false;
        rTurret.changeColor = false;
        rTurret.transform.tag = "Enemy";

        //Main Controller info
        phase = "Idle";
        attackPhase = 1;
        follow = false;
        snapCalled = false;
        callDestroyOnce = false;
        shieldControl.GetComponent<ShieldController>().destroyShields = true;

        Invoke("ShieldsUp", 1);

    }

   public void CheckPlayerRoom()
    {
        //Debug.Log(MyRoomName);
        if (GameManager.Instance.PlayerRoom == MyRoomName)
        {
           // Debug.Log("HE'S HERE!!!!");
            SetValues();
        }
        else
        {
            //Debug.Log("Player not in room");
            phase = "Idle";
            attackPhase = 0;
            //Idle();
        }
    }

}
