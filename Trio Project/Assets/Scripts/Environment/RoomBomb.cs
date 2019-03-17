using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBomb : MonoBehaviour
{
    public float bombTimer;
    public GameObject expandExplosion;
    public int DestroyPoints;
    public float Force = 20000;
    float currentTimer;
    Rigidbody rb;



    private void OnEnable()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        currentTimer = bombTimer;
        rb.AddRelativeForce(transform.up * Force);
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        currentTimer -= Time.deltaTime;
        if (currentTimer <= 0)
        {
            Explosion();
        }
    }


    void Explosion()
    {
        GameObject explosion = GenericPooler.Instance.GrabPrefab(PooledObject.Explosion);
        explosion.transform.position = transform.position;
        explosion.transform.rotation = transform.rotation;
        explosion.SetActive(true);

        gameObject.SetActive(false);
        //Instantiate(expandExplosion, transform.position, transform.rotation);
        //Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Explosion();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerBaseShot"))
        {
            GameManager.Instance.AddScore(DestroyPoints);
            Explosion();
        }
    }

    void DisableObject()
    {
        GameObject go = GenericPooler.Instance.GrabPrefab(PooledObject.Explosion);
        go.transform.position = transform.position;
        go.transform.rotation = transform.rotation;

        go.SetActive(true);

        gameObject.SetActive(false);
    }

}
