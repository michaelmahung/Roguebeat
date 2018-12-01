using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beat : MonoBehaviour {
    //public GameObject _cubePrefab;
    GameObject[] eShots = new GameObject[512];
    public float maxScale;
    public bool isBroke = false;
    //private int i;
    private PlaylistHolder playlistHolder;
    //public GameObject eShot;
    //GameObject[] eFire = new GameObject[512];

	// Use this for initialization
	void Start () {

        playlistHolder = GetComponent<PlaylistHolder>();
        //GameObject[] eShots;
        eShots = GameObject.FindGameObjectsWithTag("eProjectile");

        //eShot = GameObject.FindWithTag("eProjectile");
        //var eShot = GameObject.FindWithTag("eProjectile");
        
        /*for (int i = 0; i < 512; i++)
        {
            GameObject _instanceCube = (GameObject)Instantiate(_cubePrefab);
            _instanceCube.transform.position = this.transform.position;
            _instanceCube.transform.parent = this.transform;
            _instanceCube.name = "Cube" + i;
            this.transform.eulerAngles = new Vector3(0, -.703125f * i, 0);
            _instanceCube.transform.position = Vector3.forward * 100;
            sCube [i] = _instanceCube;
        }*/
    }
	
	// Update is called once per frame
	void Update () {
        eShots = GameObject.FindGameObjectsWithTag("eProjectile");
        //Beats();
        //int number = i;
        //if(Beats.i )
        //var eShot = GameObject.FindWithTag("eProjectile");
        /*for (int i = 0; i < 512; i++)
        {
            if (sCube != null)
            {
                sCube[i].transform.localScale = new Vector3(10, (AudioTracker._samples[i] * maxScale) + 2, 10);
            }
        }*/
        for (int i = 0; i<eShots.Length; i++)
        {
            if (i > eShots.Length)
            {
                i = 1;
            }
            if (eShots!= null)
            {
                eShots[i].transform.localScale = new Vector3((playlistHolder._samples[i] * maxScale) + 1, (playlistHolder._samples[i] * maxScale) +1, (playlistHolder._samples[i] * maxScale)+1);
                // print("there are" + i + "shots on the field");
                
            }
            
        }
        
        {

        }
        
	}

    /*void Beats()

    {
        for (int i = 0; i < eShots.Length; i++)
        {
           if (i > eShots.Length)
            {
                isBroke = true;
                break;
                
            }
            if (eShots != null)
            {
                eShots[i].transform.localScale = new Vector3((playlistHolder._samples[i] * maxScale) + 1, (playlistHolder._samples[i] * maxScale) + 1, (playlistHolder._samples[i] * maxScale) + 1);
                // print("there are" + i + "shots on the field");

            }

        }
        if(isBroke == true)
        {
            isBroke = false;
            Beats();
        }
    }*/
}
