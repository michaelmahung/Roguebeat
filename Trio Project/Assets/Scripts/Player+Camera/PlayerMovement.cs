using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour 
{
    [Range(5, 100)]
    public float Acceleration = 20;

    [Range(5, 50)]
    public float TopSpeed = 20;

    [Range(1, 15)]
    public int MaxDashDistance = 5;

    [Range(5, 15)]
    public int MinDashSpeed = 10;

    [Range(1, 7)]
    public int DashCooldown = 2;

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

    public void SetDash()
    {
        canDash = true;
    }
	
    private void FixedUpdate()
    {
        //Physics based movement for the player, Rigidbody.AddForce moves the player regardless of their rotation.

        if (rb.velocity.magnitude > TopSpeed)
        {
            rb.velocity = rb.velocity.normalized * TopSpeed;
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            rb.AddForce(North * Acceleration);
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            rb.AddForce(West * Acceleration);
        }

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            rb.AddForce(South * Acceleration);
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            rb.AddForce(East * Acceleration);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (rb.velocity.magnitude > MinDashSpeed && canDash)
            {
                Debug.Log(rb.velocity.magnitude + " " + canDash);
                StartCoroutine("StartDashCooldown");
                Dash();
            }
        }
    }

    void Dash()
    {
        //Get the velocity of the player in a direction

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
        GameManager.Instance.cameraShaker.ShakeMe(30, 0.1f);
        rb.MovePosition(transform.position += dashDirection * dashDistance);
        dashDistance = MaxDashDistance;
    }

    IEnumerator StartDashCooldown()
    {
        canDash = false;
        yield return new WaitForSeconds(DashCooldown);
        canDash = true;
        yield break;
    }
}
