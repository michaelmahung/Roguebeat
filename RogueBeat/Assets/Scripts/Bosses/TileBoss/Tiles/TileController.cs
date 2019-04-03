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
                HorizontalAttackEven();
                break;
            case TileAttacks.HorizontalOdd:
                HorizontalAttackOdd();
                break;
            case TileAttacks.VerticalEven:
                VerticalAttackEven();
                break;
            case TileAttacks.VerticalOdd:
                VerticalAttackOdd();
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



    void VerticalAttackEven()
    {
        for (int i = 0; i < allTiles.GetLength(0); i++)
        {
            allTiles[i, 0].Activate(TimeToActivateTile);
        }

        for (int i = 0; i < allTiles.GetLength(0); i++)
        {
            allTiles[i, 2].Activate(TimeToActivateTile);
        }

        for (int i = 0; i < allTiles.GetLength(0); i++)
        {
            allTiles[i, 4].Activate(TimeToActivateTile);
        }

        for (int i = 0; i < allTiles.GetLength(0); i++)
        {
            allTiles[i, 6].Activate(TimeToActivateTile);
        }
    }

    void HorizontalAttackEven()
    {
        for (int i = 0; i < allTiles.GetLength(0); i++)
        {
            allTiles[0, i].Activate(TimeToActivateTile);
        }

        for (int i = 0; i < allTiles.GetLength(0); i++)
        {
            allTiles[2, i].Activate(TimeToActivateTile);
        }

        for (int i = 0; i < allTiles.GetLength(0); i++)
        {
            allTiles[4, i].Activate(TimeToActivateTile);
        }

        for (int i = 0; i < allTiles.GetLength(0); i++)
        {
            allTiles[6, i].Activate(TimeToActivateTile);
        }
    }

    void VerticalAttackOdd()
    {
        for (int i = 0; i < allTiles.GetLength(0); i++)
        {
            allTiles[i, 1].Activate(TimeToActivateTile);
        }

        for (int i = 0; i < allTiles.GetLength(0); i++)
        {
            allTiles[i, 3].Activate(TimeToActivateTile);
        }

        for (int i = 0; i < allTiles.GetLength(0); i++)
        {
            allTiles[i, 5].Activate(TimeToActivateTile);
        }
    }

    void HorizontalAttackOdd()
    {
        for (int i = 0; i < allTiles.GetLength(0); i++)
        {
            allTiles[1, i].Activate(TimeToActivateTile);
        }

        for (int i = 0; i < allTiles.GetLength(0); i++)
        {
            allTiles[3, i].Activate(TimeToActivateTile);
        }

        for (int i = 0; i < allTiles.GetLength(0); i++)
        {
            allTiles[5, i].Activate(TimeToActivateTile);
        }
    }
}
