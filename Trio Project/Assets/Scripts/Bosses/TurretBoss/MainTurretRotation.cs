using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainTurretRotation : MonoBehaviour
{

    public GameObject body;
    public MainController controller;
    public GameObject head;
    public MainTurret check;
    public Transform player;

    public float degree;
    public bool aTrue;
    public float speed;

    // Use this for initialization
    void Start()
    {
        controller = body.GetComponent<MainController>();
        check = head.GetComponent<MainTurret>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
      
        if(controller.phase == "Attack" && controller.attackPhase == 4)
        {
            if(check.tooClose == false && aTrue == false && head.transform.localRotation.y <= degree)
            {
                //print(head.transform.localRotation.y);
                head.transform.Rotate(Vector3.up * Time.deltaTime * speed);
                if(head.transform.localRotation.y >= degree)
                {
                    //print("I need to switch");
                    aTrue = true;
                }
                
            }
            if(check.tooClose == false && aTrue == true && head.transform.localRotation.y >= -degree)
            {
                //print(head.transform.localRotation.y);
                head.transform.Rotate(Vector3.down * Time.deltaTime * speed);
                if(head.transform.localRotation.y <= - degree)
                {
                    aTrue = false;
                }
            }
            if(check.tooClose == true)
            {
                head.transform.localEulerAngles = new Vector3(0, 0, 0);
            }
        }

    }
}
