using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossController : MonoBehaviour, IBossController
{
    public bool IsMainBoss;

    public abstract void PlayerEnteredRoom();

    public abstract void PlayerExitedRoom();
}
