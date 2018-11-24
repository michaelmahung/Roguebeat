using UnityEngine;

public class PlayerMovement : MonoBehaviour 
{
    [Range(5, 100)]
    public float acceleration = 20;

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
    West = new Vector3(-1, 0, 0);


	void Start () 
    {
        rb = GetComponent<Rigidbody>();
	}
	
    private void FixedUpdate()
    {
        //Physics based movement for the player, Rigidbody.AddForce moves the player regardless of their rotation.
        if (rb.velocity.magnitude > topSpeed)
        {
            //rb.velocity.magnitude is the current speed of the player
            //All this does is say, "If the player is moving past my topspeed, slow it down".
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

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Dash();
        }
    }

    void Dash()
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
}
