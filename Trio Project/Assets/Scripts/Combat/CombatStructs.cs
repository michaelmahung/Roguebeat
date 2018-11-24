using System.Collections.Generic;
using UnityEngine;

public class CombatStructs 
{
    [System.Serializable]
    public struct ProjectileInformation
    {
        public GameObject projectile;
        public string projectileName;

        [Tooltip("How many of this projectile should be spawned into it's respectile object pool")]
        [Range(5, 200)]
        public int projectileSpawnAmount;

        [Range(1, 100)]
        public int projectileSpeed;
    }

    [System.Serializable]
    public struct WeaponInformation
    {
        public List<GameObject> fireLocations;
        public string weaponName;
        public AudioClip fireSound;

        public Texture2D icon;

        [Range(0.1f, 10)]
        public float weaponDamage;

        [Range(0.05f, 3)]
        public float fireRate;

        [Tooltip("How long you have to wait after switching to this weapon to fire")]
        [Range(0.01f, 1)]
        public float swapTime;
    }

}
