using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePoolManager : MonoBehaviour
{
    //Object pooling courtesy of brackeys.


    //Using a dictionary allows us to spawn different projectile types with different tags. 
    //After storing the projectile type, we use a queue, which is essentially a circuar array to keep track of all the objects within the projectile pool.

    public Dictionary<string, Queue<GameObject>> projectileDictionary; //Dictionaries take both a key and a value, in this care the string will be used to retrieve the gameobject.

    //Bootleg instance
    public static ProjectilePoolManager Instance;

    public void Awake()
    {
        Instance = this;
    }


    [System.Serializable]
    public class ProjectilePool
    {
        //We will create a ProjectilePool class that will always contain descriptive variables.
        public string tag;
        public GameObject prefab;
        public int size;
    }

    [SerializeField]
    //Next we will create a list of those pools for the purpose of keeping track of individual projectile types.
    public List<ProjectilePool> ProjectileTypes;



    //Need to change this so projectile will only be loaded in once a weapon is equipped
    void Start()
    {
        Debug.Log("Creating Dictionary...");
        //Initialize our dictionary.
        projectileDictionary = new Dictionary<string, Queue<GameObject>>();
        Debug.Log("Dictionary Created");

        Debug.Log("Attempting to create weapon queues");
        foreach (ProjectilePool uniqueProjectile in ProjectileTypes) //For every unique projectile type in the lsit of projectile types...
        {
            Debug.Log("Generating Queues...");
            Queue<GameObject> objectPool = new Queue<GameObject>(); //We need to create a queue of gameobjects that the prefabs will cycle through.
            GameObject parent = new GameObject(uniqueProjectile.tag + " Holder"); //To make the inspector cleaner, we'll make an empty gameobject to hold all the spawned prefabs.

            Debug.Log("Instantiating and parenting prefabs...");
            for (int i = 0; i < uniqueProjectile.size; i++) //For the total amount of projectiles to be spawned (set in the BaseWeapon class).
            {
                GameObject obj = Instantiate(uniqueProjectile.prefab); //Instantiate an instance of the projectile prefab.
                obj.SetActive(false); //Set the projectile prefab to be inactive in the scene.
                obj.transform.parent = parent.transform; //Make the prefab a child of our empty parent gameobject.
                objectPool.Enqueue(obj); //Add the new prefab to the queue.
            }
            if (projectileDictionary.ContainsKey(uniqueProjectile.tag)) //If there is already an existing key (name) for a unique projectile.
            {
                Debug.LogError("Duplicate Projectile Name: Check WeaponTest - Projectile Name"); //Dont do this to me
                return;
            }
            else
            {
                Debug.Log("Adding " + uniqueProjectile.tag + " to dictionary.");
                projectileDictionary.Add(uniqueProjectile.tag, objectPool); //Otherwise, add the unique projectile to the projectile dictionary.
            }
        }
    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation) //Function to "spawn" the prefab from the queue.
    {
        //Debug.Log("Searching dictionary for key: " + tag);
        if(!projectileDictionary.ContainsKey(tag)) //If the projectile dictionary doesnt have the tag we request.
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist."); //Dont do this to me
            return null; //You get nothing
        }

        GameObject objectToSpawn = projectileDictionary[tag].Dequeue(); //The object we want to spawn is found by comparing the tag attached to the unique projectile.

        //Debug.Log("Setting " + tag + " to be spawned");
        objectToSpawn.SetActive(true); //Activate the object.
        objectToSpawn.transform.position = position; //Set the object position to the one we set when calling the function.
        objectToSpawn.transform.rotation = rotation; //Set the objects rotation to the one we set when calling the function.
        IPooledObject pooledObj = objectToSpawn.GetComponent<IPooledObject>(); //Grab a reference to the IPooledObject interface.

        if (pooledObj != null) //If the interface does exist on the object were spawning
        {
            pooledObj.OnObjectSpawn(); //Call the OnObjectSpawn function attached to the interface
        } else
        {
            Debug.LogError("IPooledObject interface not found on object: " + tag); //Dont do this to me.
        }

        projectileDictionary[tag].Enqueue(objectToSpawn); //If all that works well, move the object to the end of the queue.

        return objectToSpawn; //return the object.
    }
}
