using UnityEngine;

public class CameraController2 : MonoBehaviour {

    public float FollowAhead;
    public float Smoothing;
    public Vector3 TargetPosition;
    public GameObject player;

    private float cameraHeight;
    private Vector3 cameraOffset;

    

    void Start()
    {
        try
        {
            //Attempt to find a gameobject tagged as the player.
            player = GameManager.Instance.Player;

            if(cameraHeight <= 0f)
            {
                cameraHeight = 40; 
            }
            gameObject.transform.position = player.transform.position + new Vector3(0, cameraHeight, 0);
            cameraOffset = transform.position - player.transform.position;
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
        if (player != null)
        {
            if (player.transform.localRotation.y > 90f)
            {
                TargetPosition = (player.transform.position + (player.transform.forward * FollowAhead)) + cameraOffset;
            }
            else
            {
                TargetPosition = (player.transform.position + (player.transform.forward * -1 * -FollowAhead)) + cameraOffset;
            }
            transform.position = Vector3.Lerp(transform.position, TargetPosition, Smoothing * Time.deltaTime);
        }
    }

}
