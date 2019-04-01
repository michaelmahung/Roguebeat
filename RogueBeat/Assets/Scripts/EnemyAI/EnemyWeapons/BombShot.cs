using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombShot : MonoBehaviour {
public float BombLife = 1.0f;
    float currentLife;
public float BombSpeed = 20.0f;
public GameObject BigBoom;
protected TagManager Tags;


    private void OnEnable()
    {
        currentLife = BombLife;
    }

    private void Awake()
    {

    }
    // Use this for initialization
    void Start () {

		Tags = GameManager.Instance.Tags;
	}
	
	// Update is called once per frame
	void Update ()
	{
		currentLife -= Time.deltaTime;
		  if (currentLife <= 0) {
		callExplosion();
		
		}

		if (currentLife > 0) {
			transform.position += transform.forward * BombSpeed * Time.deltaTime;
		}
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.CompareTag(Tags.PlayerTag) || other.CompareTag(Tags.WallTag)) {
            //print("I got'em!");
			callExplosion ();
		}
	}

	void callExplosion ()
	{
        GameObject explosion = GenericPooler.Instance.GrabPrefab(PooledObject.Explosion);
        explosion.transform.position = transform.position;
        explosion.transform.rotation = transform.rotation;
        explosion.SetActive(true);

        gameObject.SetActive(false);
		/*Instantiate (BigBoom, transform.position, transform.rotation);
			Destroy (gameObject);*/
			}
			}