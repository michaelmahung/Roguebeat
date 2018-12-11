using System.Collections.Generic;
using UnityEngine;

public class ProjectilePoolManager : MonoBehaviour
{
    [System.Serializable]
    public class ProjectileVariables
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    [SerializeField]
    public List<ProjectileVariables> ProjectileTypes;

    public Dictionary<string, Queue<GameObject>> projectileDictionary; 
    public static ProjectilePoolManager Instance;

    public void Awake()
    {
        Instance = this;
        projectileDictionary = new Dictionary<string, Queue<GameObject>>();
    }


    public void AddProjectileToDictionary(string _tag, GameObject _prefab, int _size)
    {
        ProjectileVariables uniqueProjectile = new ProjectileVariables();
        uniqueProjectile.tag = _tag;
        uniqueProjectile.prefab = _prefab;
        uniqueProjectile.size = _size;

        Queue<GameObject> objectPool = new Queue<GameObject>();
        GameObject parent = new GameObject(uniqueProjectile.tag + " Holder");

        for (int i = 0; i < uniqueProjectile.size; i++)
        {
            GameObject obj = Instantiate(uniqueProjectile.prefab);
            obj.SetActive(false);
            obj.transform.parent = parent.transform;
            objectPool.Enqueue(obj);
        }
        if (projectileDictionary.ContainsKey(uniqueProjectile.tag))
        {
            Debug.LogError("Duplicate Projectile Name: Check WeaponTest - Projectile Name");
            return;
        }
        else
        {
            projectileDictionary.Add(uniqueProjectile.tag, objectPool);
        }
    }

    //This is bad, but it works for now, ill change if needed.
    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation, float damage, float speed, float life)
    {
        if(!projectileDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
            return null;
        }

        GameObject objectToSpawn = projectileDictionary[tag].Dequeue();

        BaseProjectile baseProjectile = objectToSpawn.GetComponent<BaseProjectile>();

        if (baseProjectile != null)
        {
            baseProjectile.projectileDamage = damage;
            baseProjectile.projectileSpeed = speed;
            baseProjectile.activeTime = life;
        }else
        {
            Debug.LogError("BaseProjectile class not found on projectile");
        }

        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;
        objectToSpawn.SetActive(true);
        IPooledObject pooledObj = objectToSpawn.GetComponent<IPooledObject>();

        if (pooledObj != null)
        {
            pooledObj.OnObjectSpawn();
        }
        else
        {
            Debug.LogError("IPooledObject interface not found on object: " + tag);
        }
        projectileDictionary[tag].Enqueue(objectToSpawn);
        return objectToSpawn;
    }
}
