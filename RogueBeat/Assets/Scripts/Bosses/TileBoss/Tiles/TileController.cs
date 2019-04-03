using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileController : MonoBehaviour
{
    public float TimeToActivateTile = 1;

    [SerializeField] TileAttacks[] Phase1Attacks;
    [SerializeField] TileAttacks[] Phase2Attacks;
    [SerializeField] TileAttacks[] Phase3Attacks;
    [SerializeField] TileAttacks PreviousAttack;
    [SerializeField] int tileGridSize = 7;
    [SerializeField] TileSpawner tileSpawner;
    [SerializeField] BossTiles[,] allTiles;
    int randomInt;

    private void Awake()
    {
        tileSpawner = GetComponent<TileSpawner>();
        allTiles = tileSpawner.SpawnTiles(tileGridSize);
    }

    public void SetValues()
    {
        PreviousAttack = TileAttacks.Default;
    }

    public void ActivateTiles(BossStates state)
    {
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
        randomInt = Random.Range(0, phaseArray.Length);

        if (phaseArray[randomInt] != PreviousAttack)
        {
            PreviousAttack = phaseArray[randomInt];
            SelectTileAttack(phaseArray[randomInt]);
            return;
        }

        SelectPhaseAttack(phaseArray);
    }

    void SelectTileAttack(TileAttacks attack)
    {
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
                Debug.Log("Spiral attack go!");
                break;
            case TileAttacks.Grid:
                Debug.Log("Grid attack go!");
                break;
            case TileAttacks.VerticalWave:
                Debug.Log("Vertical Wave go!");
                break;
            case TileAttacks.HorizontalWave:
                Debug.Log("Horizontal Wave go!");
                break;
            default:
                Debug.Log("Something went wrong with attacking");
                break;
        }
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

        return;
    }
}
