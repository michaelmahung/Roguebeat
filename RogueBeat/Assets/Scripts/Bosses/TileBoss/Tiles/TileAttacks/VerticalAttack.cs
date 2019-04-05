using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalAttack : TileAttack
{
    public override void Attack(TileController.OnAttackFinished listener, BossTiles[,] tiles)
    {
        throw new System.NotImplementedException();
    }

    protected override void SetValues()
    {
        throw new System.NotImplementedException();
    }

    void VerticalAttackLogic(int index = default(int))
    {
        for (int i = 0; i < allTiles.GetLength(0); i++)
        {
            allTiles[i, index].Activate(tileActivationTime);
        }

        if ((index + 2) < tileGridSize)
        {
            VerticalAttackLogic(index + 2);
        }

        return;
    }
}
