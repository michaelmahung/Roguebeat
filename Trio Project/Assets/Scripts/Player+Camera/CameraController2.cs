using UnityEngine;

public class CameraController2 : MonoBehaviour {

    [SerializeField] private float FollowAhead;
    [SerializeField] private float Smoothing;
    [SerializeField] private Vector3 TargetPosition;

    private GameObject focalPoint;
    private float cameraHeight;
    private Vector3 cameraOffset;

    

    void Start()
    {
        try
        {
            //Attempt to find a gameobject tagged as the player.
            focalPoint = GameManager.Instance.PlayerObject;

            if(cameraHeight <= 0f)
            {
                cameraHeight = 40; 
            }
            gameObject.transform.position = focalPoint.transform.position + new Vector3(0, cameraHeight, 0);
            cameraOffset = transform.position - focalPoint.transform.position;
        }
        catch
        {
            //If there is no gameobject found with the tag, throw an error.
            Debug.LogError("No GameObject with PlayerHealth component found, please assign one");
        }
    }


    //I dont remember 100% how this works, but it does work
    void FixedUpdate()
    {
        //Stuff and things.
        if (focalPoint != null)
        {
            if (focalPoint.transform.localRotation.y > 90f)
            {
                TargetPosition = (focalPoint.transform.position + (focalPoint.transform.forward * FollowAhead)) + cameraOffset;
            }
            else
            {
                TargetPosition = (focalPoint.transform.position + (focalPoint.transform.forward * -1 * -FollowAhead)) + cameraOffset;
            }
            transform.position = Vector3.Lerp(transform.position, TargetPosition, Smoothing * Time.deltaTime);
        }
    }

    public void SetFocalPoint(GameObject location)
    {
        if (location != null)
        {
            focalPoint = location;
        }
    }

}
