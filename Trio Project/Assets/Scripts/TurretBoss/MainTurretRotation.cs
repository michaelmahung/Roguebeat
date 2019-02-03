using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainTurretRotation : MonoBehaviour
{

    public GameObject body;
    public MainController controller;
    public GameObject head;
    public MainTurret check;

    public float a;
    public float b;
    public float c = 0;
    public bool rotatiing;
    public bool aTrue;
    public bool bTrue;
    public float speed = 2;
    public bool atRotation;
    public float randDegree;
    public float currentDegree;
    public float rotationY = 0;
    public float timer = 1f;

    // Use this for initialization
    void Start()
    {
        controller = body.GetComponent<MainController>();
        check = head.GetComponent<MainTurret>();
        currentDegree = 0;
    }

    // Update is called once per frame
    void Update()
    {
        /*if (head.transform.rotation.y <= a)
        {
            print("aTrue should be true");
            //head.transform.rotation = new Vector3(0, -30, 0);
            aTrue = true;
            bTrue = false;
        }
        if (controller.attackPhase == 4)
        {
           
                if (head.transform.rotation.y >= a && aTrue == false)
            {
                //print("i should be rotating to -30");
                head.transform.Rotate(new Vector3(0, -speed * 15, 0) * Time.deltaTime);
                
            }
        }*/
        //randDegree = Random.Range(-30, 30);
        //Quaternion rotation = Quaternion.Lerp(rotationY, Vector3.up);
        if(controller.phase == "Attack" && controller.attackPhase == 4)
        {
            //timer -= Time.deltaTime;
            //if (timer <= 0)
            //{
                rotationY = Mathf.Clamp(/*Random.Range(-45, 45)*/0, -45, 45);

                //if (atRotation == false)
                //{

                transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, -rotationY, transform.localEulerAngles.z);
                //transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Vector3.up);
                //timer = 1f;
            //}
        }
    }
}
