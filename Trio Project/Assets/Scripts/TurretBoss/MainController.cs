using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI

public class MainController : MonoBehaviour, ITrackRooms {

    public string MyRoomName { get; set; }
    public RoomSetter MyRoom { get; set; }
    public Transform player;

    public int Difficulty;
    public float speed;
    public int speedMultiplyer;
    public bool snapCalled;
    public bool follow;
    public bool callDestroyOnce;
    public string phase;
    public int maxAttackPhase;
    public int attackPhase = 1;

    public ShieldController shieldControl;
    public SideTurret rTurret;
    public SideTurret lTurret;
    public MainTurret head;
    public MainTurretRotation rot;
    public GameObject cenCap;
    public GameObject cenBody;
    public GameObject BossInfo;
    public Material regBody;
    public Material nRed;
    public Material nWhite;
    public Material nBlack;
    public bool inRoom;

	// Use this for initialization
	void Start () {
        phase = "Idle";
       // Invoke("ShieldsUp", 1);
       if (Difficulty <= 1)
        {
            maxAttackPhase = 2;
        }
       if(Difficulty == 2)
        {
            maxAttackPhase = 3;
        }
       if(Difficulty >= 3)
        {
            maxAttackPhase = 4;
        }
        head.maxHealth = 100 * (maxAttackPhase - 1);
        head.health = head.maxHealth;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        RoomSetter.UpdatePlayerRoom += CheckPlayerRoom;
	}
	
	// Update is called once per frame
	void Update () {
        if(inRoom == true  && Difficulty >= 2)
        {
            BossInfo.SetActive(true);
        }
        if(inRoom == false)
        {
            BossInfo.SetActive(false);
        }
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
            //print("I should be red");
            cenBody.GetComponent<MeshRenderer>().material = nRed;
            cenCap.GetComponent<MeshRenderer>().material = nRed;
        }

        if(attackPhase != 4 && ((phase != "Attack" || head.tooClose == false) /*|| head.changeColor == false*/))
        {
            cenBody.GetComponent<MeshRenderer>().material = regBody;
            cenCap.GetComponent<MeshRenderer>().material = regBody;
        }
        if(phase == "Attack" && attackPhase == maxAttackPhase && head.changeColor == true)
        {
            cenBody.GetComponent<MeshRenderer>().material = nRed;
            cenCap.GetComponent<MeshRenderer>().material = nRed;
        }

        if((head.tooClose == true && head.changeColor == true) || (attackPhase == maxAttackPhase && head.changeColor == true))
        {

            if (maxAttackPhase == 4)
            {
                cenBody.GetComponent<MeshRenderer>().material = nWhite;
                cenCap.GetComponent<MeshRenderer>().material = nWhite;
            }
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
            rTurret.maxHealth = 10 * attackPhase;
            lTurret.maxHealth = 10 * attackPhase;
            rTurret.health = rTurret.maxHealth;
            lTurret.health = lTurret.maxHealth;
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
        if (Difficulty >= 2)
        {
            Invoke("ShieldsUp", 1);
        }
        if(Difficulty <= 1)
        {
            attackPhase++;
            Invoke("Attack", 1);
        }
    }

    void ShieldsUp()
    {
        //Debug.Log("Starting Shields Up Phase");
        phase = "ShieldsUp";
        if (maxAttackPhase > 2)
        {
            shieldControl.GetComponent<ShieldController>().raiseShields = true;
        }
        if (rTurret.disabled == false && lTurret.disabled == false)
        {
            Invoke("Attack", 2);
        }
        if (maxAttackPhase == 4)
        {
            if (rTurret.disabled == true && lTurret.disabled == true && attackPhase <= 2)
            {
                Invoke("RepairSides", 2);
            }
        }
        if(maxAttackPhase == 3)
        {
            if (rTurret.disabled == true && lTurret.disabled == true && attackPhase <= 1)
            {
                Invoke("RepairSides", 2);
            }
        }
        if(attackPhase == maxAttackPhase - 1 && rTurret.dead == true && lTurret.dead == true)
        {
            attackPhase++;
            Invoke("Attack", 1);
        }
    }
    void SetValues()
    {
        // Debug.Log("SetValues");
        //main turret info
        head.maxHealth = 100 * (maxAttackPhase - 1);
        head.health = head.maxHealth;
        head.restartHealth = true;
        head.tooClose = false;
        head.attacking = false;
        head.changeColor = false;
        head.trueOnce = false;
        head.p3Start = true;
        rot.head.transform.localEulerAngles = new Vector3(0, 0, 0);

        //Left turret info
        lTurret.health = lTurret.maxHealth;
        lTurret.Tbody.SetActive(true);
        lTurret.cap.SetActive(true);
        lTurret.barrel.SetActive(true);
        lTurret.spawn.SetActive(true);
        if (Difficulty <= 1)
        {
            //lTurret.healthBG.SetActive(true);
        }
        lTurret.disabled = false;
        lTurret.dead = false;
        lTurret.attacking = false;
        lTurret.changeColor = false;
        lTurret.p3Start = true;
        lTurret.transform.tag = "Enemy";

        //Right turret info
        rTurret.health = rTurret.maxHealth;
        rTurret.Tbody.SetActive(true);
        rTurret.cap.SetActive(true);
        rTurret.barrel.SetActive(true);
        rTurret.spawn.SetActive(true);
        if (Difficulty <= 1)
        {
            //rTurret.healthBG.SetActive(true);
        }
        rTurret.disabled = false;
        rTurret.dead = false;
        rTurret.attacking = false;
        rTurret.changeColor = false;
        rTurret.p3Start = true;
        rTurret.transform.tag = "Enemy";

        //Main Controller info
        phase = "Idle";
        attackPhase = 1;
        follow = false;
        snapCalled = false;
        callDestroyOnce = false;
        shieldControl.GetComponent<ShieldController>().destroyShields = true;

        if(Difficulty <= 1)
        {
            Invoke("Attack", .1f);
        }
        if (Difficulty >= 2)
        {
            //print("I was called");
            Invoke("ShieldsUp", 1);
        }

    }

   public void CheckPlayerRoom()
    {
        //Debug.Log(MyRoomName);
        if (GameManager.Instance.PlayerRoomName == MyRoomName)
        {
            // Debug.Log("HE'S HERE!!!!");
            inRoom = true;
            SetValues();
        }
        else
        {
            //Debug.Log("Player not in room");
            inRoom = false;
            phase = "Idle";
            attackPhase = 0;
            //Idle();
        }
    }

}
