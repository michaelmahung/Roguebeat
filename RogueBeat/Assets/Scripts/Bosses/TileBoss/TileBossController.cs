using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBossController : BossController, IBossController
{
    [SerializeField] int tileGridSize = 11;
    [SerializeField] TileSpawner tileSpawner;
    [SerializeField] BossTiles[,] allTiles;
    [SerializeField] TileBossHealth bossHealth;
    [SerializeField] TileBossWeapon weapon;

    public override void StartBoss()
    {
        Debug.Log("Starting Tile Boss");

        bossHealth.SetValues();
        weapon.SetValues();
    }

    public override void StopBoss()
    {
        Debug.Log("Stopping Tile Boss");
    }

    void Awake()
    {
        tileSpawner = GetComponent<TileSpawner>();
        allTiles = tileSpawner.SpawnTiles(tileGridSize);

        bossHealth = GetComponent<TileBossHealth>();
        weapon = GetComponent<TileBossWeapon>();
    }

    void Start()
    {
        for (int i = 0; i < allTiles.GetLength(0); i ++)
        {
            Debug.Log(allTiles[i, 0].Position.xPos);
        }
    }

    void Update()
    {
        
    }
}
