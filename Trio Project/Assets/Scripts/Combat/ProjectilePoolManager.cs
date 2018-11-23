using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is messy and needs to be restructured, works for now though.
/// </summary>

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
        projectileDictionary = new Dictionary<string, Queue<GameObject>>();
    }


    [System.Serializable]
    public class ProjectileVariables
    {
        //We will create a ProjectilePool class that will always contain descriptive variables.
        //We will use these variables to create unique projectile types from our BaseWeapon class.
        public string tag;
        public GameObject prefab;
        public int size;
    }

    [SerializeField]
    //Next we will create a list of those pools for the purpose of keeping track of individual projectile types.
    public List<ProjectileVariables> ProjectileTypes;


    //New function will take in variables to create ProjectileVariables later storage
    //One benefit of this function is that new projectiles can be stores when theyre activated instead of when the game starts.
    //Will reduce overhead if certain weapons are never used.
    public void AddProjectileToDictionary(string _tag, GameObject _prefab, int _size)
    {
        ProjectileVariables uniqueProjectile = new ProjectileVariables();
        uniqueProjectile.tag = _tag;
        uniqueProjectile.prefab = _prefab;
        uniqueProjectile.size = _size;

        Queue<GameObject> objectPool = new Queue<GameObject>(); //We need to create a queue of gameobjects that the prefabs will cycle through.
        GameObject parent = new GameObject(uniqueProjectile.tag + " Holder"); //To make the inspector cleaner, we'll make an empty gameobject to hold all the spawned prefabs.

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
            //Debug.Log("Adding " + uniqueProjectile.tag + " to dictionary.");
            projectileDictionary.Add(uniqueProjectile.tag, objectPool); //Otherwise, add the unique projectile to the projectile dictionary.
        }
    }


    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation, float damage, float speed) //Function to "spawn" the prefab from the queue.
    {
        //Debug.Log("Searching dictionary for key: " + tag);
        if(!projectileDictionary.ContainsKey(tag)) //If the projectile dictionary doesnt have the tag we request.
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist."); //Dont do this to me
            return null; //You get nothing
        }

        GameObject objectToSpawn = projectileDictionary[tag].Dequeue(); //The object we want to spawn is found by comparing the tag attached to the unique projectile.

        BaseProjectile baseProjectile = objectToSpawn.GetComponent<BaseProjectile>();
        //BaseProjectile baseChild = baseProjectile.GetComponentInChildren<BaseProjectile>();

        if (baseProjectile != null)
        {
            baseProjectile.projectileDamage = damage;
            baseProjectile.projectileSpeed = speed;
            //baseChild.projectileDamage = damage;
            //baseChild.projectileSpeed = speed;
        }else
        {
            Debug.LogError("BaseProjectile class not found on projectile");
        }

        objectToSpawn.transform.position = position; //Set the object position to the one we set when calling the function.
        objectToSpawn.transform.rotation = rotation; //Set the objects rotation to the one we set when calling the function.
        objectToSpawn.SetActive(true); //Activate the object.
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
