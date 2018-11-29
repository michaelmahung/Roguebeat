using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileColorManager : MonoBehaviour {

    //GameObject[] eShots = new GameObject[512];
    GameObject[] AlleShots;
    private PlaylistHolder playlistHolder;
    //private float sampleBeat;
    [SerializeField]
    [Range(1, 512)]
    private int sampleNumber;
    [SerializeField]
    [Range(.00000000000000001f, .4f)]
    private float sampleRange;
    public Color[] colors;
    [SerializeField]
    private int currentIndex = 0;
    [SerializeField]
    private int nextIndex;
    [SerializeField]
    [Range(.1f, 2.0f)]
    private float changeColorTime;
    //private float lastChange = 0.0f;
    //private float timer = 0.0f;

    // Use this for initialization
    void Start () {
        playlistHolder = GetComponent<PlaylistHolder>();
        //eShots = GameObject.FindGameObjectsWithTag("eProjectile");
        /*GameObject[]*/ AlleShots = GameObject.FindGameObjectsWithTag("eProjectile");
        //Renderer rend = eShots.GetComponent<Renderer>();
        //sampleBeat = playlistHolder._samples[sampleNumber];
        foreach (GameObject go in AlleShots)
        {
            MeshRenderer[] renderers = go.GetComponentsInChildren<MeshRenderer>();
            foreach (MeshRenderer r in renderers)
            {
                foreach (Material m in r.materials)
                {
                    if (m.HasProperty("_Color"))
                        m.color = Color.red;
                }
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
		if(playlistHolder._samples[sampleNumber] >= sampleRange)
        {
            currentIndex = (currentIndex + 1) % colors.Length;
            nextIndex = (currentIndex + 1) % colors.Length;
        }
        foreach (GameObject go in AlleShots)
        {
            MeshRenderer[] renderers = go.GetComponentsInChildren<MeshRenderer>();
            foreach (MeshRenderer r in renderers)
            {
                foreach (Material m in r.materials)
                {
                    if (m.HasProperty("_Color"))
                        m.color = Color.Lerp(colors[currentIndex], colors[nextIndex], Time.deltaTime/ changeColorTime);
                }
            }
        }
        //GetComponent<Renderer>().material.color = Color.Lerp(colors[currentIndex], colors[nextIndex], changeColorTime);
    }
}
