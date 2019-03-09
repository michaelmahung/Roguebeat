﻿using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour 
{
    [Range(5, 100)]
    [SerializeField] private float acceleration = 20;

    [Range(5, 50)]
    [SerializeField] private float topSpeed = 20;

    [Range(1, 15)]
    [SerializeField] private int maxDashDistance = 5;

    [Range(5, 15)]
    [SerializeField] private int minDashSpeed = 10;

    [Range(1, 7)]
    [SerializeField] private int dashCooldown = 2;

    [SerializeField] private PlayerDashUI dashUI;

    private float dashCooldownPercentage
    {
        get
        {
            return dashTimer / dashCooldown;
        }
    }

    private float dashTimer;
    private bool canDash;
    private int dashDistance;
    private Rigidbody rb;
    private Vector3
    dashDirection,
    North = new Vector3(0, 0, 1),
    South = new Vector3(0, 0, -1),
    East = new Vector3(1, 0, 0),
    West = new Vector3(-1, 0, 0);


	void Start () 
    {
        rb = GetComponent<Rigidbody>();
        GameManager.Instance.PlayerRespawned += SetDash;
        SetDash();
	}

    public void PushBackPlayer(float amount)
    {
        rb.AddForce(transform.forward * -1 * amount);
    }

    public void SetDash()
    {
        dashDistance = maxDashDistance;
        canDash = true;
    }
	
    private void FixedUpdate()
    {
        //Physics based movement for the player, Rigidbody.AddForce moves the player regardless of their rotation.

        if (rb.velocity.magnitude > topSpeed)
        {
            rb.velocity = rb.velocity.normalized * topSpeed;
        }

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

        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (rb.velocity.magnitude > minDashSpeed && canDash)
            {
                //Debug.Log(rb.velocity.magnitude + " " + canDash);
                //StartCoroutine("StartDashCooldown");
                canDash = false;
                Dash();
            }
        }
    }

    private void Update()
    {
        if (dashTimer < dashCooldown)
        {
            //Debug.Log(dashTimer);
            dashTimer += Time.deltaTime;
            dashUI.SetPercentage(dashCooldownPercentage);
        } else
        {
            canDash = true;
        }
    }

    void Dash()
    {
        //Get the velocity of the player in a direction
        dashTimer = 0;

        dashDirection = rb.velocity.normalized;
        RaycastHit hit;
        Ray dashRay = new Ray(transform.position, dashDirection);

        //Cast a ray and see if we are hitting a wall, if we are, stop at the wall, otherwise dash.

        if (Physics.Raycast(dashRay, out hit, dashDistance))
        {
            if (hit.collider.tag == "Wall")
            {
                dashDistance = (int)hit.distance;
            }
        }
        GameManager.Instance.CameraShaker.ShakeMe(30, 0.1f);
        rb.MovePosition(transform.position += dashDirection * dashDistance);
        dashDistance = maxDashDistance;
    }

    IEnumerator StartDashCooldown()
    {
        canDash = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
        yield break;
    }
}
