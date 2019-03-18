using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PooledObject
{
    Trooper, Bruiser, Boomer, BabyRoamer,
    BombShot, BoomerBomb, EnemyFire1, EnemyFireShotgun,
    Explosion, MissileExplosion, PTMissile, TrooperShot,
    RoamerMine, BabyRoamerMine, RoamerExplosion, RoomBomb
}

[System.Serializable]
public struct PoolInfo
{
    public string Name;
    public GameObject Prefab;
    public PooledObject PrefabKey;
    public int PoolSize;
    public bool ExpandPool; 
    public List<GameObject> SpawnedPrefabs;
}

public class GenericPooler : MonoBehaviour
{
    public PoolInfo[] ObjectPool;
    private static GenericPooler _instance;
    public static GenericPooler Instance { get { return _instance; } }

    private void Awake()
    {
        _instance = this;
        //LevelSpawning.FinishedSpawningRooms += GeneratePools;
        GeneratePools();
    }

    void GeneratePools()
    {
        for (int i = 0; i < ObjectPool.Length; i++)
        {
            PopulatePool(ObjectPool[i]);
            System.GC.Collect();
        }

        System.GC.Collect();
    }

    void PopulatePool(PoolInfo info)
    {
        for (int i = 0; i <= info.PoolSize; i++)
        {
            GameObject go = Instantiate(info.Prefab);
            info.SpawnedPrefabs.Add(go);
            go.SetActive(false);
        }
    }

    public GameObject GrabPrefab(PooledObject objectKey)
    {
        for (int i = 0; i < ObjectPool.Length; i++)
        {
            if (ObjectPool[i].PrefabKey == objectKey)
            {
                return FindAvailablePrefab(ObjectPool[i]);
            }
        }

        Debug.Log("No Pool Found with ID of: " + objectKey);
        return null; 
    }

    GameObject FindAvailablePrefab(PoolInfo info)
    {
        for (int i = 0; i < info.SpawnedPrefabs.Count; i ++)
        {
            if (!info.SpawnedPrefabs[i].activeInHierarchy)
            {
                if (info.SpawnedPrefabs[i] != null)
                {
                    return info.SpawnedPrefabs[i];
                }
            }
        }

        if (info.ExpandPool)
        {
            GameObject go = Instantiate(info.Prefab);
            info.SpawnedPrefabs.Add(go);
            return go;
        }

        return null;
    }

}
