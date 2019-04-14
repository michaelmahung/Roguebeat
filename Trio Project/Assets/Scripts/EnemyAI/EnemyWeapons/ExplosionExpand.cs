using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionExpand : EnemyProjectile {

public float initialSize;
public float maxSize;
public float expandSpeed = 1.0f;
public float Explosion;
public bool Changing;
public bool Unchanged;
public GameObject missile;

    Vector3 startingScale;
private Vector3 targetScale;

    protected override void OnEnable()
    {
        transform.localScale = startingScale;
        Explosion = 0;
        Changing = false;
        Unchanged = true;
        initialSize = gameObject.transform.localScale.x;
        base.OnEnable();
    }

    // Use this for initialization
    override protected void Awake () {
		Tags = GameManager.Instance.Tags;
        Damage = 10 * GameManager.Instance.Difficulty;
	Changing = false;
	Unchanged = true;
	initialSize = gameObject.transform.localScale.x;
        startingScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Changing == false && Unchanged == true){
			Explosion += Time.deltaTime * 0.9f;
			float Grow = Mathf.Lerp (initialSize, initialSize * 6.0f, Explosion);
			gameObject.transform.localScale = new Vector3 (Grow, Grow, Grow);
			maxSize = gameObject.transform.localScale.x;
			if (Explosion >= 1.0f) {
				Explosion = 1.0f;
				Unchanged = false;
				Changing = true;

			}
		}

		if (Changing == true && Unchanged == false) {
			Explosion -= Time.deltaTime * 0.9f;
			float small = Mathf.Lerp (maxSize / 1.5f, maxSize, Explosion);
			gameObject.transform.localScale = new Vector3 (small, small, small);
			if (Explosion <= 0.0f) {
				Explosion = 0.0f;
				Changing = false;
                DisableObject();

			}
		}
	}

    new protected void OnTriggerEnter(Collider other)
    {
        thingHitTag = other.tag;
        otherDamageable = other.gameObject.GetComponent<IDamageable<float>>();

        if (otherDamageable != null && !other.CompareTag(Tags.EnemyTag) && !other.CompareTag(Tags.ShieldTag) && !other.CompareTag(Tags.Untagged))
        {
            if (other == missile)
            {
                otherDamageable.Damage(Damage);

            }
            otherDamageable.Damage(Damage);
            if (!other.CompareTag(Tags.PlayerTag))
            {
                DisableObject();
            }
        }
        else if (thingHitTag == "Wall")
        {
            //Destroy(this.gameObject);
        }
        if(other.name == "RotBase")
        {
            this.GetComponent<Collider>().enabled = false;
            //Destroy(this.gameObject);
        }

    }
}

