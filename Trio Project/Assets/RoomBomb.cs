using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBomb : MonoBehaviour
{
    public float bombTimer;
	public GameObject expandExplosion;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (bombTimer <= 0)
        {
			Instantiate(expandExplosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        bombTimer -= Time.deltaTime;
    }


    void Explosion()
    {

    }

}
