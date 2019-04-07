public class VerticalWave : WaveAttack
{
    public override void Attack(TileController.OnAttackFinished listener, BossTiles[,] tiles)
    {
        allTiles = tiles;
        _listener = listener;
        tileGridSize = tiles.GetLength(0);
        SetValues();
        currentState = TileAttackStates.Active;
    }

    protected override void SetValues()
    {
        base.SetValues();

        if (incrementingDirection)
        {
            currentIndex = 0;
            return;
        }

        currentIndex = tileGridSize - 1;
    }

    protected override void WaveAttackLogic(int index)
    {
        if (index > tileGridSize - 1 || index < 0)
        {
            currentState = TileAttackStates.Buffer;
            return;
        }

        int i = 0;

        while (i < tileGridSize)
        {
            ActivateTile(allTiles, index, i);
            i++;
        }

        if (incrementingDirection)
        {
            currentIndex++;
            return;
        }

        currentIndex--;
        return;
    }
}
