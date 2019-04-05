using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TileAttack : MonoBehaviour
{
    //I need each attack to be able to communicate with the TileController, that way the controller does not have to worry about keeping track of
    //The individual speeds of each attack -- It will simply be told when the attack is finished and react accordingly.
    [SerializeField] protected float tileActivationTime;
    protected TileController.OnAttackFinished _listener;
    protected BossTiles[,] allTiles;
    protected int tileGridSize;
    protected bool running;

    public abstract void Attack(TileController.OnAttackFinished listener, BossTiles[,] tiles);

    protected virtual void Update()
    {

    }

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
        _listener();
        RemoveListener(_listener);
    }

    protected virtual void ActivateTile(BossTiles[,] tileSelection, int x, int y)
    {
        tileSelection[x, y].Activate(tileActivationTime);
    }

}
