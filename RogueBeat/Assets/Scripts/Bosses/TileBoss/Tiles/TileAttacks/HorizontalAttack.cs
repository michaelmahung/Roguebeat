using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalAttack : TileAttack
{
    public override void Attack(TileController.OnAttackFinished listener, BossTiles[,] tiles)
    {
        throw new System.NotImplementedException();
    }

    protected override void SetValues()
    {
        throw new System.NotImplementedException();
    }

    void HorizontalAttackLogic(int index)
    {
        for (int i = 0; i < allTiles.GetLength(0); i++)
        {
            allTiles[index, i].Activate(tileActivationTime);
        }

        if ((index + 2) < tileGridSize)
        {
            HorizontalAttackLogic(index + 2);
        }

        return;
    }
}
