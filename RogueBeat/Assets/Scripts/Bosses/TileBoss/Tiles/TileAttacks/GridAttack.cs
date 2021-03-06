﻿public class GridAttack : TileAttack
{
    bool gridSwitch;
    int totalLoops;

    public override void Attack(TileController.OnAttackFinished listener, BossTiles[,] tiles)
    {
        allTiles = tiles;
        _listener = listener;
        tileGridSize = tiles.GetLength(0);
        SetValues();
        currentState = TileAttackStates.Active;

        GridAttackLogic(0);
    }

    protected override void SetValues()
    {
        totalLoops = 0;
    }

    void GridAttackLogic(int index)
    {
        if (index > tileGridSize - 1)
        {
            currentState = TileAttackStates.Buffer;
            return;
        }

        int i = 0;

        if (gridSwitch)
        {
            i = 1;
        }

        while (i < tileGridSize)
        {
            ActivateTile(allTiles, i, index);
            i += 2;
        }

        gridSwitch = !gridSwitch;
        totalLoops++;
        GridAttackLogic(index + 1);
        return;
    }

    void Update()
    {
        switch (currentState)
        {
            case TileAttackStates.Default:
                break;
            case TileAttackStates.Idle:
                break;
            case TileAttackStates.Active:
                break;
            case TileAttackStates.Buffer:
                BufferLoop();
                break;
            default:
                break;
        }
    }
}
