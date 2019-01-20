using UnityEngine;

public class EnemyProjectile : MonoBehaviour {

    public float Damage;
    IDamageable<float> otherDamageable;
    string thingHitTag;


   
    private void OnTriggerEnter(Collider other)
    {
        thingHitTag = other.tag;
        otherDamageable = other.gameObject.GetComponent<IDamageable<float>>();

        if (otherDamageable != null && other.tag != "Enemy")
        {
            otherDamageable.Damage(Damage);
            Destroy(this.gameObject);
        } else if (thingHitTag == "Wall")
        {
            Destroy(this.gameObject);
        }
    }
}
