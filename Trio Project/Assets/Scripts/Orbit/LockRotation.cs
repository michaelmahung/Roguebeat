using UnityEngine;

public class LockRotation : MonoBehaviour {

    Quaternion lockedRot;

	void Start ()
    {
        lockedRot = transform.rotation;
	}
	
	// Update is called once per frame
	void LateUpdate ()
    {
        transform.rotation = lockedRot;
	}
}
