using UnityEngine;

public class EnemyProjectile : MonoBehaviour {

    public float Damage;
    protected IDamageable<float> otherDamageable;
    protected string thingHitTag;


   
    private void OnTriggerEnter(Collider other)
    {
        thingHitTag = other.tag;
        otherDamageable = other.gameObject.GetComponent<IDamageable<float>>();

        if (otherDamageable != null && other.tag != "Enemy" && other.tag != "Shield" && other.tag != "Untagged" && other.tag != "eProjectile")
        {
            otherDamageable.Damage(Damage);
            Destroy(this.gameObject);
        } else if (thingHitTag == "Wall")
        {
            Destroy(this.gameObject);
        }
        
    }
}
