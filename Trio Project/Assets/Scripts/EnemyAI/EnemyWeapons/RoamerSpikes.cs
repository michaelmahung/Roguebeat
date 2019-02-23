using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoamerSpikes : DamageableEnvironmentItemParent {

    [SerializeField] private int rotationIncreaseAmount = 40;
    [SerializeField] private float speedIncreaseAmount = 0.4f;
    [SerializeField] private float baseDamage = 10;
    [SerializeField] private Roamer myRoamer;

    private float damage
    {
        get { return baseDamage * GameManager.Instance.Difficulty; }
    }

    IDamageable<float> damageable;

	new void Start () {
        base.Start();
        ItemType = myItemType.Default;
        Armor = 0;
        KillPoints = 5;
	}

    public override void Kill()
    {
        myRoamer.IncrementMoveSpeed(speedIncreaseAmount);
        myRoamer.IncrementRotationSpeed(rotationIncreaseAmount);
        base.Kill();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Kill();
            damageable = other.GetComponent<IDamageable<float>>();
            damageable.Damage(damage);
        }
    }
}
