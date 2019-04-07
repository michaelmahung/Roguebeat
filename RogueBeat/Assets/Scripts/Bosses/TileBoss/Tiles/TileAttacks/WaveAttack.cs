using UnityEngine;

public abstract class WaveAttack : TileAttack
{
    [SerializeField] protected float waveAttackTime = 10;
    [SerializeField] protected float waveAttackIncrement;
    protected int currentIndex;
    protected bool incrementingDirection;
    protected float attackTimer;

    protected override void SetValues()
    {
        waveAttackIncrement = waveAttackTime / tileGridSize;
        incrementingDirection = !incrementingDirection;
    }

    private void Update()
    {
        switch (currentState)
        {
            case TileAttackStates.Default:
                break;
            case TileAttackStates.Idle:
                break;
            case TileAttackStates.Active:
                WaveUpdateLoop();
                break;
            case TileAttackStates.Buffer:
                BufferLoop();
                break;
            default:
                break;
        }
    }

    protected virtual void WaveUpdateLoop()
    {
        attackTimer += Time.deltaTime;

        if (attackTimer < waveAttackIncrement)
            return;

        attackTimer = 0;

        WaveAttackLogic(currentIndex);
    }

    protected abstract void WaveAttackLogic(int index);
}
