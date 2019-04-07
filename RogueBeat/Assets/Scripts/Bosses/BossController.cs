using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossController : MonoBehaviour, IBossController
{
    public bool IsMainBoss;
    [SerializeField] internal BossPhases currentPhase;

    public virtual void ChangePhase(BossPhases phase)
    {
        currentPhase = phase;
    }

    public abstract void PlayerEnteredRoom();

    public abstract void PlayerExitedRoom();
}
