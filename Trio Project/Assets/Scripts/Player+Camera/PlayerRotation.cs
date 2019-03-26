using UnityEngine;

public class PlayerRotation : MonoBehaviour 
{
    public Camera mainCam;
    public Quaternion PlayerLookDirection { get; private set; }
    Plane playerPlane;
    [SerializeField] private float rotationSpeed = 7f;

    private void Start()
    {
        if (mainCam == null)
        {
            mainCam = Camera.main;
        }

        playerPlane = new Plane(Vector3.up, transform.position);
    }

    void Update () 
    {
        RotateToMousePosition();
	}

    void RotateToMousePosition()
    {
        //Casts a plane to raycast off of into the world.
        //Shoot a ray into the plane and make the player look at the ray hit position

        //Plane playerPlane = new Plane(Vector3.up, transform.position);
        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction, Color.green, 10f);
        float hitDist = 0.0f;

        if (playerPlane.Raycast(ray, out hitDist))
        {
            //Logic for following mouse cursor location
            Vector3 targetPoint = ray.GetPoint(hitDist);
            Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
            targetRotation.x = 0;
            targetRotation.z = 0;
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime).normalized;
            PlayerLookDirection = transform.rotation;
        }
    }
}
