using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beat : MonoBehaviour {
    GameObject[] eShots = new GameObject[512];
    public float maxScale;
    public bool isBroke = false;
    private PlaylistHolder playlistHolder;

	// Use this for initialization
	void Start () {

        playlistHolder = GetComponent<PlaylistHolder>();
    }
	
	// Update is called once per frame
	void Update () {
        eShots = GameObject.FindGameObjectsWithTag("eProjectile");
        for (int i = 0; i<eShots.Length; i++)
        {
            if (i > eShots.Length)
            {
                i = 1;
            }
            if (eShots!= null)
            {
                eShots[i].transform.localScale = new Vector3 ((playlistHolder._samples[i] * maxScale) + 1, (playlistHolder._samples[i] * maxScale) +1, (playlistHolder._samples[i] * maxScale)+1);
                // print("there are" + i + "shots on the field");
                
            }
            
        }
        
        {

        }
        
	}
}
