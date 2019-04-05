using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileAttackStates
{
    Default,
    Spiral,
    Wave
}

[RequireComponent(typeof(TileSpawner))]
public class TileController : MonoBehaviour
{
    public float TimeToActivateTile = 1;
    public float SpiralAttackSpeed;
    public float WaveAttackSpeed;
    [SerializeField] int tileGridSize = 7;
    [SerializeField] float spiralAttackTime = 5f;
    [SerializeField] TileAttacks[] Phase1Attacks;
    [SerializeField] TileAttacks[] Phase2Attacks;
    [SerializeField] TileAttacks[] Phase3Attacks;
    FireStates currentAttackState;
    TileSpawner tileSpawner;
    TileAttackStates currentTileState;
    TileAttacks previousAttack;
    BossTiles[,] allTiles;
    float spiralFireTime;
    float attackTimer;
    int randomInt;
    int loopCount;
    int spiralMax;
    int spiralMin;
    int spiralCutoff;

    //TODO -- break tile attacks up into their own classes

    private void Awake()
    {
        tileSpawner = GetComponent<TileSpawner>();
        allTiles = tileSpawner.SpawnTiles(tileGridSize);
        spiralFireTime = spiralAttackTime / 3;
    }

    public void SetValues()
    {
        attackTimer = 0;
        spiralMax = tileGridSize - 1;
        spiralMin = 0;
        spiralCutoff = (tileGridSize + 1) / 2;
        currentTileState = TileAttackStates.Default;
        previousAttack = TileAttacks.Default;
    }

    public void ActivateTiles(BossStates state)
    {
        if (currentAttackState == FireStates.Firing)
            return;

        switch (state)
        {
            case BossStates.Phase1:
                SelectPhaseAttack(Phase1Attacks);
                break;
            case BossStates.Phase2:
                SelectPhaseAttack(Phase2Attacks);
                break;
            case BossStates.Phase3:
                SelectPhaseAttack(Phase3Attacks);
                break;
            case BossStates.Default:
                Debug.Log("Boss is not in a valid state");
                break;
            default:
                Debug.Log("Something went wrong");
                break;
        }
    }

    void SelectPhaseAttack(TileAttacks[] phaseArray)
    {
        currentAttackState = FireStates.Firing;
        randomInt = Random.Range(0, phaseArray.Length);

        if (loopCount > 2)
        {
            SelectTileAttack(phaseArray[randomInt]);
            return;
        }

        if (phaseArray[randomInt] != previousAttack)
        {
            previousAttack = phaseArray[randomInt];
            SelectTileAttack(phaseArray[randomInt]);
            return;
        }

        loopCount++;
        SelectPhaseAttack(phaseArray);
    }

    void SelectTileAttack(TileAttacks attack)
    {
        loopCount = 0;

        switch (attack)
        {
            case TileAttacks.Default:
                Debug.Log("Invalid attack selected");
                break;
            case TileAttacks.HorizontalEven:
                HorizontalAttackEven(0);
                break;
            case TileAttacks.HorizontalOdd:
                HorizontalAttackOdd(1);
                break;
            case TileAttacks.VerticalEven:
                VerticalAttackEven(0);
                break;
            case TileAttacks.VerticalOdd:
                VerticalAttackOdd(1);
                break;
            case TileAttacks.Spiral:
                currentTileState = TileAttackStates.Spiral;
                break;
            case TileAttacks.Grid:
                Debug.Log("Grid attack go!");
                currentAttackState = FireStates.Idle;
                break;
            case TileAttacks.VerticalWave:
                currentTileState = TileAttackStates.Wave;
                break;
            case TileAttacks.HorizontalWave:
                currentTileState = TileAttackStates.Wave;
                break;
            default:
                Debug.Log("Something went wrong with attacking");
                break;
        }
    }

    private void Update()
    {
        switch (currentTileState)
        {
            case TileAttackStates.Default:
                break;
            case TileAttackStates.Spiral:
                SpiralUpdateLoop();
                break;
            case TileAttackStates.Wave:
                currentTileState = TileAttackStates.Default;
                WaveAttackUpdate();
                break;
            default:
                break;
        }
    }

    void SpiralUpdateLoop()
    {
        attackTimer += Time.deltaTime;

        if (attackTimer < spiralFireTime)
            return;

        attackTimer = 0;
        SpiralAttack(spiralMax, spiralMin);
    }

    void SpiralAttack(int max, int min)
    {
        int x = min;
        int y = min;

        while(x < max)
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

        while(y > min)
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

        currentAttackState = FireStates.Idle;
        currentTileState = TileAttackStates.Default;
        spiralMax = tileGridSize - 1;
        spiralMin = 0;
    }

    void ActivateTile(BossTiles[,] tileSelection, int x, int y)
    {
        tileSelection[x, y].Activate(TimeToActivateTile);
    }

    void WaveAttackUpdate()
    {
        currentAttackState = FireStates.Idle;
    }

    //TODO -- I can consolidate these into one encompassing function

    void VerticalAttackEven(int index)
    {
        for (int i = 0; i < allTiles.GetLength(0); i++)
        {
            allTiles[i, index].Activate(TimeToActivateTile);
        }

        if ((index + 2) < tileGridSize)
        {
            VerticalAttackEven(index + 2);
        }

        currentAttackState = FireStates.Idle;
        return;
    }

    void VerticalAttackOdd(int index)
    {
        for (int i = 0; i < allTiles.GetLength(0); i++)
        {
            allTiles[i, index].Activate(TimeToActivateTile);
        }

        if ((index + 2) < tileGridSize)
        {
            VerticalAttackOdd(index + 2);
        }

        currentAttackState = FireStates.Idle;
        return;
    }

    void HorizontalAttackEven(int index)
    {
        for (int i = 0; i < allTiles.GetLength(0); i++)
        {
            allTiles[index, i].Activate(TimeToActivateTile);
        }

        if ((index + 2) < tileGridSize)
        {
            HorizontalAttackEven(index + 2);
        }

        currentAttackState = FireStates.Idle;
        return;
    }

    void HorizontalAttackOdd(int index)
    {
        for (int i = 0; i < allTiles.GetLength(0); i++)
        {
            allTiles[index, i].Activate(TimeToActivateTile);
        }

        if ((index + 2) < tileGridSize)
        {
            HorizontalAttackOdd(index + 2);
        }

        currentAttackState = FireStates.Idle;
        return;
    }
}
