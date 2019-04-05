using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiralAttack : TileAttack
{
    [SerializeField] float spiralAttackTime = 4;
    float spiralFireTime;
    int spiralMin;
    int spiralMax;
    int spiralCutoff;
    float attackTimer;

    public override void Attack(TileController.OnAttackFinished listener, BossTiles[,] tiles)
    {
        allTiles = tiles;
        _listener = listener;
        tileGridSize = tiles.GetLength(0);
        SetValues();
    }

    protected override void SetValues()
    {
        attackTimer = 0;
        spiralMax = tileGridSize - 1;
        spiralMin = 0;
        spiralCutoff = (tileGridSize + 1) / 2;
        spiralFireTime = spiralAttackTime / 3;
    }

    protected override void Update()
    {
        if (!running)
            return;

        SpiralUpdateLoop();
    }

    void SpiralUpdateLoop()
    {
        attackTimer += Time.deltaTime;

        if (attackTimer < spiralFireTime)
            return;

        attackTimer = 0;
        SpiralAttackLogic(spiralMax, spiralMin);
    }

    void SpiralAttackLogic(int max, int min)
    {
        int x = min;
        int y = min;

        while (x < max)
        {
            ActivateTile(allTiles, x, y);
            x++;
        }

        while (y < max)
        {
            ActivateTile(allTiles, x, y);
            y++;
        }

        while (x > min)
        {
            ActivateTile(allTiles, x, y);
            x--;
        }

        while (y > min)
        {
            ActivateTile(allTiles, x, y);
            y--;
        }

        if (spiralMax > spiralCutoff)
        {
            spiralMin++;
            spiralMax--;
            return;
        }


        spiralMax = tileGridSize - 1;
        spiralMin = 0;
        running = false;
        AttackFinished();
    }
}
