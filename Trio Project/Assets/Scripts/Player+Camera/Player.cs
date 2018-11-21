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
    [Range(1, 15)]
    public int maxDashDistance = 5;
    private int dashDistance;
    private Rigidbody rb;
    private Vector3
    dashDirection,
    North = new Vector3(0, 0, 1),
    South = new Vector3(0, 0, -1),
    East = new Vector3(1, 0, 0),
    West = new Vector3 (-1, 0, 0);

    public void Kill()
    {
        //Since the player uses the killable interface, we need to assign a kill function
        Destroy(gameObject);
    }

    public void Damage(float damage)
    {
        //Since the player uses the damageable interface, we need to assign a damage function
        currentHealth -= damage;
    }

    void Start()
    {
        //If theres a player in the scene but no GameManager for some reason, throw a warning
        if (GameManager.Instance == null)
        {
            Debug.LogWarning("Player will have limited functionality without a GameManager script in the scene.");
        }
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
    }

    private void FixedUpdate()
    {
        //Physics based movement for the player, Rigidbody.AddForce moves the player regardless of their rotation.
            
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            rb.AddForce(North * acceleration);
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            rb.AddForce(West * acceleration);
        }

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            rb.AddForce(South * acceleration);
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            rb.AddForce(East * acceleration);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            dashDirection = rb.velocity.normalized;
            RaycastHit hit;
            Ray dashRay = new Ray(transform.position, dashDirection);

            if (Physics.Raycast(dashRay, out hit, dashDistance))
            {
                if (hit.collider.tag == "Wall")
                {
                    //Debug.Log("Hit a wall");
                    dashDistance = (int)hit.distance;
                }
            }
            rb.MovePosition(transform.position += dashDirection * dashDistance);
            dashDistance = maxDashDistance;
        }

        if (rb.velocity.magnitude > topSpeed)
        {
            //rb.velocity.magnitude is the current speed of the player
            //All this does is say, "If the player is moving past my topspeed, slow it down".
            rb.velocity = rb.velocity.normalized * topSpeed; 
        }
    }

}
