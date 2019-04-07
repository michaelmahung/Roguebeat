public class HorizontalAttack : TileAttack
{
    bool even;

    public override void Attack(TileController.OnAttackFinished listener, BossTiles[,] tiles)
    {
        allTiles = tiles;
        _listener = listener;
        tileGridSize = tiles.GetLength(0);
        SetValues();
        currentState = TileAttackStates.Active;

        if (even)
        {
            HorizontalAttackLogic(0);
            return;
        }

        HorizontalAttackLogic(1);
    }

    protected override void SetValues()
    {
        even = !even;
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

        currentState = TileAttackStates.Buffer;
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
