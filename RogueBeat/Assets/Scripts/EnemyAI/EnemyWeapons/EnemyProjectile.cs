using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{

    public float Damage;
    protected IDamageable<float> otherDamageable;
    protected string thingHitTag;
    protected float ProjectileLife;
    protected float currentLife;

    protected TagManager Tags;

    virtual protected void Awake()
    {
        Tags = GameManager.Instance.Tags;
        currentLife = ProjectileLife;
    }

    virtual protected void OnEnable()
    {
        currentLife = ProjectileLife;
    }


    virtual protected void OnTriggerEnter(Collider other)
    {
        thingHitTag = other.tag;
        otherDamageable = other.gameObject.GetComponent<IDamageable<float>>();

        if (otherDamageable != null && !other.CompareTag(Tags.EnemyTag) && !other.CompareTag(Tags.ShieldTag) && !other.CompareTag(Tags.Untagged) && !other.CompareTag(Tags.EnemyProjectileTag))
        //if(otherDamageable != null && other.tag != ("Enemy") && other.tag !=("Shield") && other.tag != ("Untagged") && other.tag != ("eProjectile"))
        {
            otherDamageable.Damage(Damage);
            DisableObject();
        }
        else if (thingHitTag == "Wall")
        {
            DisableObject();
        }
    }

    virtual protected void DisableObject()
    {
        thingHitTag = null;
        otherDamageable = null;
        gameObject.SetActive(false);
    }

}
