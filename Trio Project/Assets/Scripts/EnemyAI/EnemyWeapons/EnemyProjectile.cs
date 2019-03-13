using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{

    public float Damage;
    protected IDamageable<float> otherDamageable;
    protected string thingHitTag;

    protected TagManager Tags;

    void Awake()
    {
        Tags = GameManager.Instance.Tags;
    }

    private void OnTriggerEnter(Collider other)
    {
        thingHitTag = other.tag;
        otherDamageable = other.gameObject.GetComponent<IDamageable<float>>();

        if (otherDamageable != null && !other.CompareTag(Tags.EnemyTag) && !other.CompareTag(Tags.ShieldTag) && !other.CompareTag(Tags.Untagged) && !other.CompareTag(Tags.EnemyProjectileTag))
        //if(otherDamageable != null && other.tag != ("Enemy") && other.tag !=("Shield") && other.tag != ("Untagged") && other.tag != ("eProjectile"))
        {
            otherDamageable.Damage(Damage);
            Destroy(this.gameObject);
        }
        else if (thingHitTag == "Wall")
        {
            Destroy(this.gameObject);
        }

    }
}
