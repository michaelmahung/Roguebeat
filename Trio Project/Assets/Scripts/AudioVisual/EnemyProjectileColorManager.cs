using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileColorManager : MonoBehaviour {

    //array to hold all enemy shots
    GameObject[] AlleShots;
    //script to access
    private PlaylistHolder playlistHolder;

    //what beat number to check between 1 and 512
    [SerializeField]
    [Range(1, 512)]
    private int sampleNumber;

    //what decibal range to check for
    [SerializeField]
    [Range(.00000000000000001f, .4f)]
    private float sampleRange;

    //array of what colors to transition through
    public Color[] colors;
    //current color number
    //[SerializeField]
    public int currentIndex = 0;
    //next color number
    //[SerializeField]
    public int nextIndex;
    //how fast should the color change happen
    [SerializeField]
    [Range(.1f, 2.0f)]
    private float changeColorTime;
    //color change is done
    //[SerializeField]
    //private bool isDone = true;
    //timer
    //[SerializeField]
   // private float timer;

    // Use this for initialization
    void Start () {
        //declare PlaylistHolder script
        playlistHolder = GetComponent<PlaylistHolder>();
        //Fill the array of enemy projectiles with all items with eProjectile tag
        AlleShots = GameObject.FindGameObjectsWithTag("eProjectile");

        //for each item in the AlleShots array
        foreach (GameObject go in AlleShots)
        {
            //create an array of the renderers for each of those objects
            MeshRenderer[] renderers = go.GetComponentsInChildren<MeshRenderer>();
            //get access to each meshrenderer
            foreach (MeshRenderer r in renderers)
            {
                //get access to each material
                foreach (Material m in r.materials)
                {
                    //check to make sure it can use colors
                   // if (m.HasProperty("_Color"))
                        //set the color 
                        m.color = colors[currentIndex]; 
                }
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        //ColorChange();
        AlleShots = GameObject.FindGameObjectsWithTag("eProjectile");
        //make the timer count
        //timer = Time.deltaTime;
        //access the samples from the music script and check to see if the sample number 
        //is equal to or greater than the set range and that the last color change is completed
        if (playlistHolder._samples[sampleNumber] >= sampleRange)// && isDone == true)
        {
            //if so, add 1 to both current and next index
            currentIndex = (currentIndex + 1) % colors.Length;
            nextIndex = (currentIndex + 1) % colors.Length;
            //set isdone to false
            //isDone = false;
            //call the function to change the color
            //ColorChange();
        }
        ColorChange();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            print("there are " + AlleShots.Length + " eProjectile tags in play");
        }
    }

    void ColorChange()
    {
        //for each item in the AlleShots array
        foreach (GameObject go in AlleShots)
        {
            //create an array of the renderers for each of those objects
            MeshRenderer[] renderers = go.GetComponentsInChildren<MeshRenderer>();
            //get access to each meshrenderer
             foreach (MeshRenderer r in renderers)
            {
            //get access to each material
            foreach (Material m in r.materials)
            {
            //check to make sure it can use colors
            //if (m.HasProperty("_Color"))
            //Lerp from currentIndex color to nextIndex Color in the specified time
            m.color = Color.Lerp(colors[currentIndex], colors[nextIndex], changeColorTime);
                    //set isDone to true
                    //isDone = true;
                }
            }
        }
        /*if (timer >= changeColorTime && isDone == false)
        {
            //set is done to true
            isDone = true;
            timer = 0.0f;
        }*/
    }
}
