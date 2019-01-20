using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour {

    public Transform player;
    public float speed;
    public int speedMultiplyer;
    public bool snapCalled;
    public string phase;
    public int attackPhase = 1;
    public ShieldController shieldControl;

	// Use this for initialization
	void Start () {
        phase = "Idle";
        Invoke("ShieldsUp", 1);
	}
	
	// Update is called once per frame
	void Update () {
		
       Vector3 relativePos = player.position - transform.position;
       Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
       transform.rotation = Quaternion.Lerp(transform.rotation, rotation, speed * Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.L))
        {
            //transform.LookAt(player);
            //transform.LookAt(player);
            snapCalled = true;
           
        }
        if(snapCalled == true)
        {
            //Vector3 relativePos = player.position - transform.position;
           // Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, (speed * speedMultiplyer) * Time.deltaTime);
            Invoke("SnapToBool", speed/speedMultiplyer);
        }
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

        phase = "Attack";
        //print("i am now attacking");

    }

    void RepairSides()
    {

    }

    void DestroyAllSheilds()
    {

    }

    void ShieldsUp()
    {
        shieldControl.GetComponent<ShieldController>().raiseShields = true;
        Invoke("Attack", 1);
    }

}
