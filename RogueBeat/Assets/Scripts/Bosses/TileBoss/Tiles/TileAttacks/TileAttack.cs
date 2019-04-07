using UnityEngine;

public enum TileAttackStates
{
    Default,
    Idle,
    Active,
    Buffer
}

public abstract class TileAttack : MonoBehaviour
{
    //I need each attack to be able to communicate with the TileController, that way the controller does not have to worry about keeping track of
    //The individual speeds of each attack -- It will simply be told when the attack is finished and react accordingly.
    [SerializeField] internal TileAttackStates currentState;
    [SerializeField] protected float tileActivationTime = 1;
    [SerializeField] protected float bufferTime = 1;
    protected float bufferTimer;
    protected TileController.OnAttackFinished _listener;
    protected BossTiles[,] allTiles;
    protected int tileGridSize;
    protected bool running;

    public abstract void Attack(TileController.OnAttackFinished listener, BossTiles[,] tiles);

    protected abstract void SetValues();

    protected virtual void AddListener(TileController.OnAttackFinished listener)
    {
        _listener = listener;
        _listener += AttackFinished;
        StartAttack();
    }

    protected virtual void RemoveListener(TileController.OnAttackFinished listener)
    {
        if (_listener != null)
        listener -= AttackFinished;
    }

    protected virtual void StartAttack()
    {
        //Do attack
        AttackFinished();
    }

    protected virtual void AttackFinished()
    {
        //Finish attack logic -- reset state etc
        currentState = TileAttackStates.Idle;
        bufferTimer = 0;
        _listener();
        RemoveListener(_listener);
    }

    protected virtual void ActivateTile(BossTiles[,] tileSelection, int x, int y)
    {
        tileSelection[x, y].Activate(tileActivationTime);
    }

    protected virtual void BufferLoop()
    {
        bufferTimer += Time.deltaTime / bufferTime;

        if (bufferTimer < 1)
            return;

        AttackFinished();
    }

}
