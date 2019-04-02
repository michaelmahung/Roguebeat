using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomHeight : MonoBehaviour {

    [SerializeField] private int MaxHeight = 100;
    [SerializeField] private int MinHeight = 40;

    private void OnDestroy()
    {
        
    }

    void Start ()
    {
        //Start this thing from a random height (within bounds)
        LevelSpawning.FinishedSpawningRooms += SetHeight; //By working based off this call, we can make sure that each cube is assigned to the room properly.
	}

    void SetHeight()
    {
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, Random.Range(MinHeight, MaxHeight), gameObject.transform.position.z);
        LevelSpawning.FinishedSpawningRooms -= SetHeight;
    }

}
