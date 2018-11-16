using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable<float>, IKillable
//Take on the damage and killable interfaces for the player
{

    public float attackDamage = 10;
    public float attackSpeed = 1;
    [Range(5, 100)]
    public float acceleration = 20;
    public float currentHealth = 10;
    public float maxHealth = 10;
    [Range(5, 50)]
    public float topSpeed = 20;
    private Rigidbody rb;

    public void Kill()
    {
        Destroy(gameObject);
    }

    public void Damage(float damage)
    {
        currentHealth -= damage;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Plane playerPlane = new Plane(Vector3.up, transform.position);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float hitDist = 0.0f;

        if (playerPlane.Raycast(ray, out hitDist))
        {
            //Logic for following mouse cursor location
            Vector3 targetPoint = ray.GetPoint(hitDist);
            Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
            targetRotation.x = 0;
            targetRotation.z = 0;
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 7f * Time.deltaTime);
        }

        /*if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            transform.position += new Vector3(0, 0, 1) * acceleration * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(-1, 0, 0) * acceleration * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += new Vector3(1, 0, 0) * acceleration * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            transform.position += new Vector3(0, 0, -1) * acceleration * Time.deltaTime;
        }*/
       
    }

    private void FixedUpdate()
    {
        //Physics based movement for the player, Rigidbody.AddForce moves the player regardless of their rotation.

        //Debug.Log(rb.velocity.magnitude);

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
            rb.AddForce(new Vector3(0, 0, 1) * acceleration);
            }

            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
            rb.AddForce(new Vector3(-1, 0, 0) * acceleration);
            }

            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
            rb.AddForce(new Vector3(1, 0, 0) * acceleration);
            }

            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                rb.AddForce(new Vector3(0, 0, -1) * acceleration);
            }  

        if (rb.velocity.magnitude > topSpeed)
        {
            //rb.velocity.magnitude is the current speed of the player
            //All this does is say, "If the player is moving past my topspeed, slow it down".
            rb.velocity = rb.velocity.normalized * topSpeed; 
        }
    }

}
