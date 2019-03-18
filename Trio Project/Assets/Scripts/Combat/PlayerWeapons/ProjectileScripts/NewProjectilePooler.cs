using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ProjectileTypes
{
    BaseProjectile,
    LaserProjectile,
    FlamethrowerProjectile
}

[System.Serializable]
public struct ProjectileProperties
{
    public GameObject Prefab;
    public BaseProjectile PrefabBehaviour;
}

[System.Serializable]
public struct ProjectilePoolInfo
{
    public string Name;
    public ProjectileProperties Projectile;
    public ProjectileTypes PrefabKey;
    public int PoolSize;
    public bool ExpandPool;
    public List<ProjectileProperties> SpawnedPrefabs;
}

public class NewProjectilePooler : MonoBehaviour
{
    public ProjectilePoolInfo[] ProjectilePool;

    private static NewProjectilePooler _instance;
    public static NewProjectilePooler Instance { get { return _instance; } }

    private void Awake()
    {
        _instance = this;
        GeneratePools();
    }

    void GeneratePools()
    {
        for (int i = 0; i < ProjectilePool.Length; i++)
        {
            PopulatePool(ProjectilePool[i]);
            System.GC.Collect();
        }

        System.GC.Collect();
    }

    void PopulatePool(ProjectilePoolInfo info)
    {
        for (int i = 0; i < info.PoolSize; i++)
        {
            ProjectileProperties projectile = new ProjectileProperties();
            projectile.Prefab = Instantiate(info.Projectile.Prefab);
            projectile.PrefabBehaviour = projectile.Prefab.GetComponent<BaseProjectile>();
            info.SpawnedPrefabs.Add(projectile);
            projectile.Prefab.SetActive(false);
        }
    }

    public ProjectileProperties GrabProjectile(ProjectileTypes key)
    {
        for (int i = 0; i < ProjectilePool.Length; i++)
        {
            if (ProjectilePool[i].PrefabKey == key)
            {
                return FindAvailablePrefab(ProjectilePool[i]);
            }
        }

        Debug.Log("No Pool Found with ID of: " + key);
        return ProjectilePool[0].Projectile;
    }

    ProjectileProperties FindAvailablePrefab(ProjectilePoolInfo info)
    {
        for (int i = 0; i < info.SpawnedPrefabs.Count; i++)
        {
            if (!info.SpawnedPrefabs[i].Prefab.activeInHierarchy)
            {
                if (info.SpawnedPrefabs[i].Prefab != null)
                {
                    return info.SpawnedPrefabs[i];
                }
            }
        }

        if (info.ExpandPool)
        {
            ProjectileProperties projectile = new ProjectileProperties();
            projectile.Prefab = Instantiate(info.Projectile.Prefab);
            projectile.PrefabBehaviour = projectile.Prefab.GetComponent<BaseProjectile>();
            info.SpawnedPrefabs.Add(projectile);
            return projectile;
        }

        return info.Projectile;
    }
}
