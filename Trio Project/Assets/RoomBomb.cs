using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBomb : MonoBehaviour
{
    public float bombTimer;
	public GameObject expandExplosion;
    public int DestroyPoints;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        bombTimer -= Time.deltaTime;
        if (bombTimer <= 0)
        {
Explosion();
        }
    }


    void Explosion()
    {
Instantiate(expandExplosion, transform.position, transform.rotation);
            Destroy(gameObject);
    }

    void OnCollisionEnter (Collision collision)
    {
        if(collision.gameObject.CompareTag("Player")){
            Explosion();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("PlayerBaseShot")){
            print ("hitting");
            GameManager.Instance.AddScore(DestroyPoints);
            Explosion();
        }
    }

}
