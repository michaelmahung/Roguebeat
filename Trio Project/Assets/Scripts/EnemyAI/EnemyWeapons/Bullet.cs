using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : EnemyProjectile {

public float BulletLife = 6.0f;
public float BulletSpeed = 50.0f;

    /*Josh's shit
    private EnemyProjectileColorManager eColor;
    private int colorIndex;
    private Color[] getColor;
    private Color startColor = Color.red;*/
 
	// Use this for initialization
	void Start () {
        Damage = 1 * GameManager.Instance.Difficulty;
        //also Josh's shit
       /* eColor = GetComponent<EnemyProjectileColorManager>();
        colorIndex = eColor.currentIndex;
        getColor = eColor.colors;
        Renderer rend = GetComponent<Renderer>();
        rend.material.color = Color.Lerp(startColor, getColor[colorIndex], Time.deltaTime);*/
	}
	
	// Update is called once per frame
	void Update ()
	{
		BulletLife -= Time.deltaTime;
		if (BulletLife <= 0) {
			Destroy (gameObject);
		}
		if (BulletLife > 0) {
			transform.position += transform.forward * BulletSpeed * Time.deltaTime;
		}
	}
}