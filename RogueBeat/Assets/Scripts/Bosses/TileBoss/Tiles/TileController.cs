using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TileSpawner))]
public class TileController : MonoBehaviour
{
    public delegate void OnAttackFinished();
    public OnAttackFinished AttackFinished;
    [SerializeField] int tileGridSize = 7;
    [SerializeField] TileAttack[] Phase1Attacks;
    [SerializeField] TileAttack[] Phase2Attacks;
    [SerializeField] TileAttack[] Phase3Attacks;
    FireStates currentAttackState;
    TileSpawner tileSpawner;
    TileAttack previousAttack;
    TileAttack[] tileAttacks;
    BossTiles[,] allTiles;
    int randomInt;
    int loopCount;

    private void Awake()
    {
        AttackFinished += FinishedAttacking;
        tileSpawner = GetComponent<TileSpawner>();
        allTiles = tileSpawner.SpawnTiles(tileGridSize);
    }

    public void SetValues()
    {
        previousAttack = null;
    }

    public void ActivateTiles(BossPhases state)
    {
        if (currentAttackState == FireStates.Firing)
            return;

        switch (state)
        {
            case BossPhases.Phase1:
                SelectPhaseAttack(Phase1Attacks);
                break;
            case BossPhases.Phase2:
                SelectPhaseAttack(Phase2Attacks);
                break;
            case BossPhases.Phase3:
                SelectPhaseAttack(Phase3Attacks);
                break;
            case BossPhases.Default:
                Debug.Log("Boss is not in a valid state");
                break;
            default:
                Debug.Log("Something went wrong");
                break;
        }
    }

    void SelectPhaseAttack(TileAttack[] phaseArray)
    {
        currentAttackState = FireStates.Firing;
        randomInt = Random.Range(0, phaseArray.Length);

        if (loopCount < 2)
        {
            if (previousAttack != phaseArray[randomInt])
            {
                phaseArray[randomInt].Attack(AttackFinished, allTiles);
                return;
            }

            loopCount++;
            SelectPhaseAttack(phaseArray);
            return;
        }

        phaseArray[randomInt].Attack(AttackFinished, allTiles);
    }

    void FinishedAttacking()
    {
        currentAttackState = FireStates.Idle;
    }
}
