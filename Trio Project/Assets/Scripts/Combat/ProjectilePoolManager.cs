using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePoolManager : MonoBehaviour
{
    [System.Serializable]
    public class ProjectilePool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    [SerializeField]
    public List<ProjectilePool> Pools;
    public Dictionary<string, Queue<GameObject>> projectileDictionary;


    public static ProjectilePoolManager Instance;

    public void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        Debug.Log("Starting PPM");
        projectileDictionary = new Dictionary<string, Queue<GameObject>>();
        Debug.Log("Dictionary Created");

        foreach (ProjectilePool pool in Pools)
        {
            Debug.Log("Assigning Pools");
            Queue<GameObject> objectPool = new Queue<GameObject>();
            GameObject parent = new GameObject(pool.tag + " Holder");

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                obj.transform.parent = parent.transform;
                objectPool.Enqueue(obj);
            }
            Debug.Log("Adding to Dictionary");
            projectileDictionary.Add(pool.tag, objectPool);
        }
    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if(!projectileDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
            return null;
        }

        GameObject objectToSpawn = projectileDictionary[tag].Dequeue();


        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;
        //Debug.Log(position);
        IPooledObject pooledObj = objectToSpawn.GetComponent<IPooledObject>();

        if (pooledObj != null)
        {
            pooledObj.OnObjectSpawn();
        }

        projectileDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }
}
